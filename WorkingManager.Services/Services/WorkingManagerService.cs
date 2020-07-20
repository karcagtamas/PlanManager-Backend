using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.WM;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Entities.WM;
using ManagerAPI.Models.Models.WM;
using ManagerAPI.Services.Services.Interfaces;
using WorkingManager.Services.Services.Interfaces;

namespace WorkingManager.Services.Services
{
    /// <summary>
    /// Working Manager Service
    /// </summary>
    public class WorkingManagerService : IWorkingManagerService
    {
        // Actions
        private const string GetWorkingDayTypesAction = "get working day types";
        private const string UpdateWorkingFieldAction = "update working field";
        private const string RemoveWorkingFieldAction = "remove working field";
        private const string AddWorkingFieldAction = "add working field";
        private const string UpdateWorkingDayAction = "update working day";
        private const string CreateWorkingDayAction = "create working day";
        private const string GetWorkingDayAction = "get working day";
        private const string GetWorkingFieldAction = "get working field";

        // Thing
        private const string FieldIdThing = "field id";
        private const string DayIdThing = "day id";
        private const string DayThing = "day";

        // Message
        private const string WorkingFieldDoesNotExistMessage = "Working field does not exist";
        private const string WorkingFieldIdsDoNotMatchMessage = "Working field Ids do not match";
        private const string WorkingDayDoesNotExistMessage = "Working day does not exist";
        private const string WorkingDayIdsDoNotMatchMessage = "Working day Ids do not match";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public WorkingManagerService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _loggerService = loggerService;
        }
        
        /// <summary>
        /// Get Working day
        /// </summary>
        /// <param name="day">Day</param>
        /// <returns>Working day</returns>
        public WorkingDayListDto GetWorkingDay(DateTime day)
        {
            User user = this._utilsService.GetCurrentUser();
            var workingDay = user.WorkingDays.FirstOrDefault(x => x.Day == day);
            
            if (workingDay == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(WorkingManagerService), DayThing, WorkingDayDoesNotExistMessage);
            }

            var dto = _mapper.Map<WorkingDayListDto>(workingDay);
            this._loggerService.LogInformation(user, nameof(WorkingManagerService), GetWorkingDayAction, dto.Id);
            return dto;
        }

        /// <summary>
        /// Create working day
        /// </summary>
        /// <param name="model">Working day init model</param>
        public void CreateWorkingDay(WorkingDayInitModel model)
        {
            User user = this._utilsService.GetCurrentUser();
            WorkingDay created = new WorkingDay
            {
                Day = model.Date,
                StartHour = 8,
                EndHour = 16,
                StartMin = 0,
                EndMin = 0,
                TypeId = 1,
                UserId = user.Id
            };

            _context.WorkingDays.Add(created);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(WorkingManagerService), CreateWorkingDayAction, created.Id);
        }

        /// <summary>
        /// Update working day
        /// </summary>
        /// <param name="workingDayId">Wokring day's Id</param>
        /// <param name="model">Working day model</param>
        public void UpdateWorkingDay(int workingDayId, WorkingDayModel model)
        {
            User user = this._utilsService.GetCurrentUser();

            WorkingDay updated = _context.WorkingDays.Find(workingDayId);
            if (updated == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(WorkingManagerService), DayIdThing, WorkingDayDoesNotExistMessage);
            }

            _mapper.Map(model, updated);

            _context.WorkingDays.Update(updated);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(WorkingManagerService), UpdateWorkingDayAction, updated.Id);
        }

        /// <summary>
        /// Add working field to working day
        /// </summary>
        /// <param name="workingDayId">Working day's Id</param>
        /// <param name="model">Working field model</param>
        public void AddWorkingField(int workingDayId, WokringFieldModel model)
        {
            User user = this._utilsService.GetCurrentUser();
            WorkingDay workingDay = _context.WorkingDays.Find(workingDayId);

            if (workingDay == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(WorkingManagerService), DayIdThing, WorkingDayDoesNotExistMessage);
            }

            WorkingField workingField = _mapper.Map<WorkingField>(model);
            workingDay.WorkingFields.Add(workingField);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(WorkingManagerService), AddWorkingFieldAction, workingField.Id);
        }

        /// <summary>
        /// Remove working field
        /// </summary>
        /// <param name="workingFieldId">Working field's Id</param>
        public void DeleteWorkingField(int workingFieldId)
        {
            User user = this._utilsService.GetCurrentUser();
            WorkingField workingField = _context.WorkingFields.Find(workingFieldId);

            if (workingField == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(WorkingManagerService), FieldIdThing, WorkingFieldDoesNotExistMessage);
            }

            _context.WorkingFields.Remove(workingField);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(WorkingManagerService), RemoveWorkingFieldAction, workingField.Id);
        }

        /// <summary>
        /// Update working field
        /// </summary>
        /// <param name="workingFieldId">Working field's Id</param>
        /// <param name="model">Working field</param>
        public void UpdateWorkingField(int workingFieldId, WokringFieldModel model)
        {
            User user = this._utilsService.GetCurrentUser();

            WorkingField updated = _context.WorkingFields.Find(workingFieldId);
            if (updated == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(WorkingManagerService), FieldIdThing, WorkingFieldDoesNotExistMessage);
            }

            _mapper.Map(model, updated);
            _context.WorkingFields.Update(updated);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(WorkingManagerService), UpdateWorkingFieldAction, updated.Id);
        }

        /// <summary>
        /// Get working day types
        /// </summary>
        /// <returns>List of working day types</returns>
        public List<WorkingDayTypeDto> GetWorkingDayTypes()
        {
            User user = this._utilsService.GetCurrentUser();
            var list = _mapper.Map<List<WorkingDayTypeDto>>(_context.WorkingDayTypes.ToList());

            this._loggerService.LogInformation(user, nameof(WorkingManagerService), GetWorkingDayTypesAction, list.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Get working field by the given Id
        /// </summary>
        /// <param name="id">Id of the field</param>
        /// <returns>Data object of the field</returns>
        public WorkingFieldDto GetWorkingField(int id)
        {
            var user = this._utilsService.GetCurrentUser();
            var field = _context.WorkingFields.Find(id);

            if (field == null) {
                throw this._loggerService.LogInvalidThings(user, nameof(WorkingManagerService), FieldIdThing, WorkingFieldDoesNotExistMessage);
            }

            this._loggerService.LogInformation(user, nameof(WorkingManagerService), GetWorkingFieldAction, field.Id);

            return this._mapper.Map<WorkingFieldDto>(field);
        }
    }
}
