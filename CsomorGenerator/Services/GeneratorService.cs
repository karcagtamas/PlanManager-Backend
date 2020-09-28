using AutoMapper;
using CsomorGenerator.Services.Interfaces;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.CSM;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsomorGenerator.Services
{
    /// <summary>
    /// Csomor Generator
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utils;
        private readonly ILoggerService _logger;
        private readonly int GenerateLimit = 500;

        public GeneratorService(DatabaseContext context, IMapper mapper, IUtilsService utils, ILoggerService logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._utils = utils;
            this._logger = logger;
        }

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

                settings.Works.ForEach(work =>
                {
                    settings.Persons = this.GenerateToWork(ref work, settings.Persons, settings.MaxWorkHour);
                });
            }

            return null;
        }

        /// <summary>
        /// Generate csomor for works
        /// </summary>
        /// <param name="work">Work</param>
        /// <param name="persons">Persons</param>
        /// <param name="maxHour">Maximum work hour</param>
        /// <returns>List of modified persons</returns>
        private List<Person> GenerateToWork(ref Work work, List<Person> persons, int maxHour)
        {
            for (int i = 0; i < work.Tables.Count; i++)
            {
                var table = work.Tables[i];
                GenerateToDate(ref table, ref persons, work.Id, maxHour, 0);
            }
            return persons;
        }

        /// <summary>
        /// Generate csomor for table
        /// </summary>
        /// <param name="table">Work table</param>
        /// <param name="persons">Person list</param>
        /// <param name="workId">Work Id</param>
        /// <param name="maxHour">Maximum work hour</param>
        /// <param name="counter">Counter against infinite loop</param>
        private void GenerateToDate(ref WorkTable table, ref List<Person> persons, string workId, int maxHour, int counter = 0)
        {
            var person = GetValidRandomPerson(persons);

            if (this.WorkerIsValid(person, table.Date, workId, maxHour))
            {
                table.PersonId = person.Id;
                DateTime date = table.Date;
                var pTable = person.Tables.FirstOrDefault(x => this.CompareDates(date, x.Date));

                if (pTable == null)
                {
                    throw new ArgumentException("Table is missing");
                }

                pTable.WorkId = workId;
                person.Hours--;
            }
            else
            {
                if (counter < this.GenerateLimit)
                {
                    if (counter > 100)
                    {
                        // TODO: Worker switch
                    }
                    else
                    {
                        this.GenerateToDate(ref table, ref persons, workId, maxHour, counter++);
                    }
                }
            }
        }

        /// <summary>
        /// Get valid random person.
        /// Filtered to active and available persons
        /// </summary>
        /// <param name="persons">Person list</param>
        /// <returns>Randomized person</returns>
        private Person GetValidRandomPerson(List<Person> persons)
        {
            Random r = new Random();
            var validList = persons.Where(x => !x.IsIgnored && x.Hours != 0).ToList();

            return validList[r.Next(validList.Count)];
        }

        /// <summary>
        /// Precheck simple generation
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
            if (!this.CheckPast(person, date, maxWorkHour))
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
            // TODO: add min rest
            // TODO: check future too (because of change)
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

        /// <summary>
        /// Create Csomor from settings model
        /// </summary>
        /// <param name="model">Settings model</param>
        public int Create(GeneratorSettingsModel model)
        {
            var user = this._utils.GetCurrentUser();

            var csomor = this._mapper.Map<Csomor>(model);
            csomor.OwnerId = user.Id;

            model.Persons.ForEach(x =>
            {
                var person = csomor.Persons.FirstOrDefault(y => y.Id == x.Id);

                person.IgnoredWorks = new List<IgnoredWork>();

                x.IgnoredWorks.ForEach(y =>
                {
                    var ignored = new IgnoredWork
                    {
                        PersonId = x.Id,
                        WorkId = y
                    };

                    person.IgnoredWorks.Add(ignored);
                });
            });

            csomor.Persons = this._mapper.Map<List<CsomorPerson>>(model.Persons);
            csomor.Works = this._mapper.Map<List<CsomorWork>>(model.Works);

            this._context.Csomors.Add(csomor);
            this._context.SaveChanges();

            this._logger.LogInformation(user, "Generator Service", "create csomor", csomor.Id);

            return csomor.Id;
        }

        /// <summary>
        /// Update csomor with the given Id
        /// </summary>
        /// <param name="id">Csomor Id</param>
        /// <param name="model">Settings model</param>
        public void Update(int id, GeneratorSettingsModel model)
        {
            var user = this._utils.GetCurrentUser();

            var csomor = this._context.Csomors.Find(id);

            if (csomor == null)
            {
                throw this._logger.LogInvalidThings(user, "Generator Service", "csomor", "Csomor does not exist");
            }

            this._mapper.Map(model, csomor);

            for (var i = 0; i < csomor.Persons.Count; i++)
            {
                this._context.CsomorPersonTables.RemoveRange(csomor.Persons.ToList()[i].Tables);
                this._context.SaveChanges();
                this._context.CsomorPersons.Remove(csomor.Persons.ToList()[i]);
                this._context.SaveChanges();
            }
            for (var i = 0; i < csomor.Works.Count; i++)
            {
                this._context.CsomorWorkTables.RemoveRange(csomor.Works.ToList()[i].Tables);
                this._context.SaveChanges();
                this._context.CsomorWorks.Remove(csomor.Works.ToList()[i]);
                this._context.SaveChanges();
            }
            this._context.SaveChanges();

            csomor.Persons = this._mapper.Map<List<CsomorPerson>>(model.Persons);
            csomor.Works = this._mapper.Map<List<CsomorWork>>(model.Works);


            this._context.Csomors.Update(csomor);
            this._context.SaveChanges();

            this._logger.LogInformation(user, "Generator Service", "update csomor", csomor.Id);
        }

        /// <summary>
        /// Remove Csomor with the given Id
        /// </summary>
        /// <param name="id">Csomor Id</param>
        public void Delete(int id)
        {
            var user = this._utils.GetCurrentUser();

            var csomor = this._context.Csomors.Find(id);

            if (csomor == null)
            {
                throw this._logger.LogInvalidThings(user, "Generator Service", "csomor", "Csomor does not exist");
            }

            this._context.Csomors.Remove(csomor);
            this._context.SaveChanges();

            this._logger.LogInformation(user, "Generator Service", "delete csomor", id);
        }

        /// <summary>
        /// Get csomor settings
        /// </summary>
        /// <param name="id">Csomor Id</param>
        /// <returns>Generator settings</returns>
        public GeneratorSettings Get(int id)
        {
            var user = this._utils.GetCurrentUser();

            var csomor = this._context.Csomors.Find(id);

            if (csomor == null)
            {
                throw this._logger.LogInvalidThings(user, "Generator Service", "csomor", "Csomor does not exist");
            }

            this._logger.LogInformation(user, "Generator Service", "get csomor", id);

            return this._mapper.Map<GeneratorSettings>(csomor);
        }

        /// <summary>
        /// Get public csomors
        /// </summary>
        /// <returns></returns>
        public List<CsomorListDTO> GetPublicList()
        {
            var list = this._mapper.Map<List<CsomorListDTO>>(this._context.Csomors.Where(x => x.IsPublic).OrderBy(x => x.Id));

            try
            {
                var user = this._utils.GetCurrentUser();

                this._logger.LogInformation(user, "Generator Service", "get csomor", list.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this._logger.LogAnonymousInformation("Generator Service", "get csomor", list.Select(x => x.Id).ToList());
            }

            return list;
        }

        public List<CsomorListDTO> GetOwnedList()
        {
            var user = this._utils.GetCurrentUser();

            var list = this._mapper.Map<List<CsomorListDTO>>(user.OwnedCsomors.OrderBy(x => x.Id));

            this._logger.LogInformation(user, "Generator Service", "get csomor", list.Select(x => x.Id).ToList());

            return list;
        }

        public List<CsomorListDTO> GetSharedList()
        {
            var user = this._utils.GetCurrentUser();

            var list = this._mapper.Map<List<CsomorListDTO>>(user.SharedCsomors.Select(x => x.Csomor).OrderBy(x => x.Id));

            this._logger.LogInformation(user, "Generator Service", "get csomor", list.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Update share list
        /// </summary>
        /// <param name="id">Csomor Id</param>
        /// <param name="models">Access models</param>
        public void Share(int id, List<CsomorAccessModel> models)
        {
            var user = this._utils.GetCurrentUser();

            var csomor = this._context.Csomors.Find(id);

            var shareList = this._context.SharedCsomors.ToList();

            models.ForEach(x =>
            {
                if (shareList.Select(y => y.UserId).ToList().Contains(x.Id))
                {
                    var element = shareList.FirstOrDefault(y => y.UserId == x.Id);
                    if (element.HasWriteAccess != x.HasWriteAccess)
                    {
                        element.HasWriteAccess = x.HasWriteAccess;

                        this._context.SharedCsomors.Update(element);
                    }
                }
                else
                {
                    var element = new UserCsomor
                    {
                        UserId = x.Id,
                        HasWriteAccess = x.HasWriteAccess,
                        CsomorId = id
                    };

                    this._context.SharedCsomors.Add(element);
                }
            });

            shareList.ForEach(x =>
            {
                if (!models.Select(y => y.Id).Contains(x.UserId))
                {
                    this._context.SharedCsomors.Remove(x);
                }
            });

            this._context.SaveChanges();

            this._logger.LogInformation(user, "Generator Service", "update shared", models.Select(x => x.Id).ToList());
        }

        /// <summary>
        /// Change public status
        /// </summary>
        /// <param name="id">Csomor Id</param>
        /// <param name="status">New status</param>
        public void ChangePublicStatus(int id, bool status)
        {
            var user = this._utils.GetCurrentUser();
            var csomor = this._context.Csomors.Find(id);

            if (csomor == null)
            {
                throw this._logger.LogInvalidThings(user, "Generator Service", "csomor", "Csomor does not exist");
            }

            csomor.IsPublic = status;
            this._context.Update(csomor);
            this._context.SaveChanges();

            this._logger.LogInformation(user, "Generator Service", status ? "publish" : "unpublish" + " csomor", id);
        }
    }
}