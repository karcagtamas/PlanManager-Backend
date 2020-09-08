using System;
using System.Collections.Generic;
using System.Linq;
using CsomorGenerator.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;

namespace CsomorGenerator.Services
{
    /// <summary>
    /// Csomor Generator
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        /// <summary>
        /// Simple generator
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public GeneratorSettings GenerateSimple(GeneratorSettings settings)
        {
            // Pre check
            if (PreCheckSimple(settings))
            {
                   this.SetWorkHours(ref settings);
                   var limit = 500;
                   var stop = true;
                   
                   
                   settings.Works.ForEach(work =>
                   {
                       work.Tables.ForEach(table =>
                       {
                           if (table.IsActive)
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

        /// <summary>
        /// Pre check simple generation
        /// </summary>
        /// <param name="settings">Generator settings</param>
        /// <returns>Is startable or not</returns>
        private bool PreCheckSimple(GeneratorSettings settings)
        {
            return CheckPersons(settings.Persons, settings.Works) && CheckWorks(settings.Works) && CheckHours(settings) && CheckSum(settings);
        }

        /// <summary>
        /// Check persons valid status
        /// </summary>
        /// <param name="persons">Persons</param>
        /// <param name="works">Works</param>
        /// <returns>Valid or not</returns>
        private bool CheckPersons(List<Person> persons, List<Work> works)
        {
            persons.ForEach(x => CheckPerson(x, works.Count));

            return true;
        }

        /// <summary>
        /// Check person
        /// </summary>
        /// <param name="person">Person</param>
        /// <param name="works">Work</param>    
        private void CheckPerson(Person person, int works)
        {
            // Check all hour is unavailable
            if (!person.Tables.Any(x => x.IsAvailable))
            {
                throw new ArgumentException($"Person ({person.Name}) cannot be unavailable all the time.");
            }

            // All works is ignored
            if (person.IgnoredWorks.Count == works)
            {
                throw new ArgumentException($"Person ({person.Name}) must has least one not ignored Work");
            }
        }

        /// <summary>
        /// Check work valid status.
        /// </summary>
        /// <param name="works">Work</param>
        /// <returns>Valid or not</returns>
        private bool CheckWorks(List<Work> works)
        {
            works.ForEach(CheckWork);

            return true;
        }

        /// <summary>
        /// Check empty works.
        /// All work is empty or not.
        /// </summary>
        /// <param name="work">Work</param>
        private void CheckWork(Work work)
        {
            if (!work.Tables.Any(x => x.IsActive))
            {
                throw new ArgumentException($"Work ({work.Name}) cannot be unavailable all the time.");
            }
        }

        /// <summary>
        /// Check hours.
        /// All hours is correct.
        /// </summary>
        /// <param name="settings">Generator settings</param>
        /// <returns>Valid or not</returns>
        private bool CheckHours(GeneratorSettings settings)
        {
            // Start
            var start = settings.Start;

            while (start < settings.Finish)
            {
                // Count of works
                var works = settings.Works.Count(x =>
                    x.Tables.FirstOrDefault(y => CompareDates(start, y.Date))?.IsActive ?? false);
                
                // Count of persons
                var persons = settings.Persons.Count(x =>
                    x.Tables.FirstOrDefault(y => CompareDates(start, y.Date))?.IsAvailable ?? false);

                // Work number has to be less thank works
                if (works > persons)
                {
                    throw new ArgumentException($"There are not enough person in hour {start.Month}-{start.Day}-{start.Hour}.");
                }
                
                start = start.AddHours(1);
            }

            return true;
        }

        /// <summary>
        /// Check hour sums
        /// </summary>
        /// <param name="settings">Generator settings</param>
        /// <returns>Valid or not</returns>
        private bool CheckSum(GeneratorSettings settings)
        {
            // Sums
            var personSum = settings.Persons.Sum(x => x.Tables.Count(y => y.IsAvailable));
            var workSum = settings.Works.Sum(x => x.Tables.Count(y => y.IsActive));

            // Work sum has to be less than person sum's 90% and minus the res hour sum
            if (workSum > (personSum * 0.9) - this.GetLength(settings.Start, settings.Finish) / (settings.MaxWorkHour + settings.MinRestHour) * settings.MinRestHour)
            {
                throw new ArgumentException("Does not have enough person.");
            }
            
            return true;
        }

        /// <summary>
        /// Worker is valid in the current date for the given work or not.
        /// </summary>
        /// <param name="person">Person</param>
        /// <param name="date">Current date</param>
        /// <param name="workId">Work Id</param>
        /// <param name="maxWorkHour">Maximum work hour setting</param>
        /// <returns>Valid or not</returns>
        private bool WorkerIsValid(Person person, DateTime date, string workId, int maxWorkHour)
        {
            // Person not has more available hour
            if (person.Hours == 0)
            {
                return false;
            }
            
            var table = person.Tables.FirstOrDefault(x => this.CompareDates(date, x.Date));

            // Worker does not have hour table.
            if (table == null)
            {
                throw new ArgumentException("Wrong person table");
            }
            
            // Person is available and not has any work
            if (table.IsAvailable && string.IsNullOrEmpty(table.WorkId))
            {
                return false;
            }

            // The work is not ignored
            if (person.IgnoredWorks.Contains(workId))
            {
                return false;
            }

            // Past is acceptable for the settings
            if (!CheckPast(person, date, maxWorkHour))
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Check past.
        /// All last max work hour has work or not.
        /// </summary>
        /// <param name="person">Checked person</param>
        /// <param name="date">Current date</param>
        /// <param name="maxWorkHour">Maximum work hour setting</param>
        /// <returns>Valid or not</returns>
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

        /// <summary>
        /// Compare two dates. Equals or not.
        /// </summary>
        /// <param name="date1">Date 1</param>
        /// <param name="date2">Date 2</param>
        /// <returns>Equals or not</returns>
        private bool CompareDates(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day &&
                   date1.Hour == date2.Hour;
        }

        /// <summary>
        /// Get difference between two date in hour
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="end">Finish date</param>
        /// <returns>Length</returns>
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

        /// <summary>
        /// Calculate work hours to the persons
        /// </summary>
        /// <param name="settings">Generator settings</param>
        private void SetWorkHours(ref GeneratorSettings settings)
        {
            // All hour
            var hours = settings.Works.Sum(x => x.Tables.Count(y => y.IsActive));

            // Add hours to the persons
            while (hours > 0)
            {
                for (int i = 0; i < settings.Persons.Count && hours > 0; i++)
                {
                    // Max hours
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