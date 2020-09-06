using System;
using System.Collections.Generic;
using System.Linq;
using CsomorGenerator.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;

namespace CsomorGenerator.Services
{
    public class GeneratorService : IGeneratorService
    {
        public GeneratorSettings GenerateSimple(GeneratorSettings settings)
        {
            if (PreCheckSimple(settings))
            {
                   this.SetWorkHours(ref settings);
                   var limit = 500;
                   var stop = true;
                   
                   
                   settings.Works.ForEach(work =>
                   {
                       work.Tables.ForEach(table =>
                       {
                           if (table.IsAvailable)
                           {
                               var count = 0;
                               Person person = null;
                               do
                               {
                                   Random rand = new Random();
                                   int index = rand.Next(settings.Persons.Count);
                                   person = settings.Persons[index];
                                   count++;

                                   if (count >= 100)
                                   {
                                       var newIndex = rand.Next(settings.Persons.Count);
                                       var newPerson = settings.Persons[newIndex];
                                       var newDate =  table.Date.AddHours(-rand.Next(this.GetLength(settings.Start, table.Date)));

                                       var addedWork = settings.Works.FirstOrDefault(x =>
                                           x.Id == newPerson.Tables
                                               .FirstOrDefault(y => this.CompareDates(newDate, y.Date))?.WorkId);

                                       var newTable =
                                           newPerson.Tables.FirstOrDefault(x => this.CompareDates(table.Date, x.Date));
                                       if (newTable != null && newTable.IsAvailable
                                           && string.IsNullOrEmpty(newTable.WorkId) && this.WorkerIsValid(person, newDate, addedWork?.Id, settings.MaxWorkHour))
                                       {
                                           if (addedWork != null)
                                           {
                                               person.Hours--;
                                               var pTable = person.Tables.FirstOrDefault(x => this.CompareDates(newDate, x.Date));
                                               if (pTable == null)
                                               {
                                                   throw new ArgumentException("Table is missing");
                                               }

                                               pTable.WorkId = addedWork.Id;
                                                   

                                               newPerson.Hours++;
                                               var newPTable = person.Tables.FirstOrDefault(x => this.CompareDates(newDate, x.Date));
                                               if (newPTable == null)
                                               {
                                                   throw new ArgumentException("Table is missing");
                                               }

                                               newPTable.WorkId = null;

                                               var workTable = settings.Works.FirstOrDefault(x => x.Id == addedWork.Id)
                                                   ?.Tables
                                                   .FirstOrDefault(x => this.CompareDates(newDate, x.Date));

                                               if (workTable == null)
                                               {
                                                   throw new ArgumentException("Table is missing");
                                               }

                                               workTable.PersonId = person.Id;
                                           }

                                           person = newPerson;
                                       }
                                   }

                               } while (this.WorkerIsValid(person, table.Date, work.Id, settings.MaxWorkHour) && count < limit);

                               if (count < limit)
                               {
                                   table.PersonId = person.Id;
                                   var pTable = person.Tables.FirstOrDefault(x => this.CompareDates(table.Date, x.Date));

                                   if (pTable == null)
                                   {
                                       throw new ArgumentException("Table is missing");
                                   }

                                   pTable.WorkId = work.Id;
                                   person.Hours--;
                               }
                               else
                               {
                                   throw new ArgumentException("Generating was not successful");
                               }
                           }
                       });
                   });
            }

            return null;
        }

        private bool PreCheckSimple(GeneratorSettings settings)
        {
            return CheckPersons(settings.Persons, settings.Works) && CheckWorks(settings.Works) && CheckHours(settings) && CheckSum(settings);
        }

        private bool CheckPersons(List<Person> persons, List<Work> works)
        {
            persons.ForEach(x => CheckPerson(x, works.Count));

            return true;
        }

        private void CheckPerson(Person person, int works)
        {
            if (!person.Tables.Any(x => x.IsAvailable))
            {
                throw new ArgumentException($"Person ({person.Name}) cannot be unavailable all the time.");
            }

            if (person.IgnoredWorks.Count == works)
            {
                throw new ArgumentException($"Person ({person.Name}) must has least one not ignored Work");
            }
        }

        private bool CheckWorks(List<Work> works)
        {
            works.ForEach(CheckWork);

            return true;
        }

        private void CheckWork(Work work)
        {
            if (!work.Tables.Any(x => x.IsAvailable))
            {
                throw new ArgumentException($"Work ({work.Name}) cannot be unavailable all the time.");
            }
        }

        private bool CheckHours(GeneratorSettings settings)
        {
            var start = settings.Start;

            while (start < settings.Finish)
            {
                var works = settings.Works.Count(x =>
                    x.Tables.FirstOrDefault(y => CompareDates(start, y.Date))?.IsAvailable ?? false);
                var persons = settings.Persons.Count(x =>
                    x.Tables.FirstOrDefault(y => CompareDates(start, y.Date))?.IsAvailable ?? false);

                if (works > persons)
                {
                    throw new ArgumentException($"There are not enough person in hour {start.Month}-{start.Day}-{start.Hour}.");
                }
                
                start = start.AddHours(1);
            }

            return true;
        }

        private bool CheckSum(GeneratorSettings settings)
        {
            var personSum = settings.Persons.Sum(x => x.Tables.Count(y => y.IsAvailable));
            var workSum = settings.Works.Sum(x => x.Tables.Count(y => y.IsAvailable));

            if (workSum > (personSum * 0.9) - this.GetLength(settings.Start, settings.Finish) / (settings.MaxWorkHour + settings.MinRestHour) * settings.MinRestHour)
            {
                throw new ArgumentException("Does not have enough person.");
            }
            
            return true;
        }

        private bool WorkerIsValid(Person person, DateTime date, string workId, int maxWorkHour)
        {
            if (person.Hours == 0)
            {
                return false;
            }

            var table = person.Tables.FirstOrDefault(x => this.CompareDates(date, x.Date));

            if (table == null)
            {
                throw new ArgumentException("Wrong person table");
            }
            
            if (table.IsAvailable && string.IsNullOrEmpty(table.WorkId))
            {
                return false;
            }

            if (person.IgnoredWorks.Contains(workId))
            {
                return false;
            }

            if (!CheckPast(person, date, maxWorkHour))
            {
                return false;
            }
            
            return true;
        }

        private bool CheckPast(Person person, DateTime date, int maxWorkHour)
        {
            for (int i = 1; i <= maxWorkHour; i++)
            {
                if (string.IsNullOrEmpty(person.Tables.FirstOrDefault(x => this.CompareDates(date.AddHours(-i), x.Date))
                    ?.WorkId))
                {
                    return true;
                }
            }
            
            return false;
        }

        private bool CompareDates(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day &&
                   date1.Hour == date2.Hour;
        }

        private int GetLength(DateTime start, DateTime end)
        {
            var length = 0;
            var date = start;
            while (date < end)
            {
                length++;
                date = date.AddHours(1);
            }

            return length;
        }

        private void SetWorkHours(ref GeneratorSettings settings)
        {
            var hours = settings.Works.Sum(x => x.Tables.Count(y => y.IsAvailable));

            while (hours > 0)
            {
                for (int i = 0; i < settings.Persons.Count && hours > 0; i++)
                {
                    var availableHours = settings.Persons[i].Tables.Count(x => x.IsAvailable);
                    if (settings.Persons[i].Hours < availableHours)
                    {
                        settings.Persons[i].Hours++;
                        hours--;
                    }
                }
            }
        }
    }
}