using System;
using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using Microsoft.Extensions.Logging;

namespace WorkingManager.Services.Services
{
    public class WorkingManagerService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<WorkingManagerService> _logger;
        private readonly IMapper _mapper;

        public WorkingManagerService(DatabaseContext context, ILogger<WorkingManagerService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        /*
        public WorkingDayListDTO GetWorkingDay(string userId, DateTime day)
        {
            User user = _context.ApplicationUsers.Find(userId);
            _logger.LogInformation($"Get WorkingDay for user {user.UserName} ({user.Id})");
            WorkingDay wd = user.WorkingDays.FirstOrDefault(x => x.Day == day);
            WorkingDayListDTO workDayDto = _mapper.Map<WorkingDayListDTO>(wd);
            if (workDayDto == null)
            {
                return null;
            }

            if (wd != null)
                workDayDto.WorkingFields = wd.WorkingFields.Select(x => _mapper.Map<WorkingFieldListDTO>(x)).ToList();
            return workDayDto;
        }

        public void CreateWorkingDay(string userId, WorkingDayDTO workingDay)
        {
            User user = _context.ApplicationUsers.Find(userId);
            if (workingDay == null)
            {
                throw new Exception($"Invalid data for creation by user {user.UserName} ({user.Id})");
            }
            try
            {
                WorkingDay created = _mapper.Map<WorkingDay>(workingDay);
                created.UserId = userId;
                _context.WorkingDays.Add(created);
                _context.SaveChanges();
                _logger.LogInformation($"Successfully created WorkingDay for user {user.UserName} ({user.Id})");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateWorkingDay(string userId, int workingDayId, WorkingDayDTO workingDay)
        {
            User user = _context.ApplicationUsers.Find(userId);
            if (workingDay == null)
            {
                throw new Exception($"Invalid data for update by user {user.UserName} ({user.Id})");
            }
            if (workingDay.Id != workingDayId)
            {
                throw new Exception($"Invalid Id for update by user {user.UserName} ({user.Id}");
            }
            WorkingDay updated = _context.WorkingDays.Find(workingDayId);
            if (updated == null)
            {
                throw new Exception($"Invalid Id for update by user {user.UserName} ({user.Id}");
            }
            try
            {
                updated.Day = workingDay.Day;
                updated.StartHour = workingDay.StartHour;
                updated.StartMin = workingDay.StartMin;
                updated.EndHour = workingDay.EndHour;
                updated.EndMin = workingDay.EndMin;
                updated.TypeId = workingDay.Type;
                _context.WorkingDays.Update(updated);
                _context.SaveChanges();
                _logger.LogInformation($"Successfully updated WorkingDay ({workingDay.Id}) by user {user.UserName} ({user.Id})");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddWorkingField(string userId, int workingDayId, WorkingFieldDTO WorkingField)
        {
            User user = _context.ApplicationUsers.Find(userId);
            WorkingDay workingDay = _context.WorkingDays.Find(workingDayId);
            if (workingDay == null)
            {
                throw new Exception("WorkingDay does not exist");
            }
            if (WorkingField == null)
            {
                throw new Exception("Invalid WorkingField");
            }
            try
            {
                WorkingField workingField = _mapper.Map<WorkingField>(WorkingField);
                workingDay.WorkingFields.Add(workingField);
                _context.SaveChanges();
                _logger.LogInformation($"Successfully created WorkingField by user {user.UserName} ({user.Id})");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void DeleteWorkingField(string userId, int workingFieldId)
        {
            User user = _context.ApplicationUsers.Find(userId);
            WorkingField workingField = _context.WorkingFields.Find(workingFieldId);
            if (workingField == null)
            {
                throw new Exception("WorkingField does not exist");
            }
            try
            {
                _context.WorkingFields.Remove(workingField);
                _context.SaveChanges();
                _logger.LogInformation($"Successfully deleted WorkingField by user {user.UserName} ({user.Id})");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateWorkingField(string userId, int workingFieldId, WorkingFieldDTO workingField)
        {
            User user = _context.ApplicationUsers.Find(userId);
            if (workingField == null)
            {
                throw new Exception("Invalid update form");
            }
            if (workingFieldId != workingField.Id)
            {
                throw new Exception("Invalid input Ids");
            }
            try
            {
                WorkingField updated = _context.WorkingFields.Find(workingFieldId);
                updated.Length = workingField.Length;
                updated.Title = workingField.Title;
                updated.Description = workingField.Description;
                _context.WorkingFields.Update(updated);
                _context.SaveChanges();
                _logger.LogInformation($"Successfully updated WorkingField ({updated.Id}) by user {user.UserName} ({user.Id})");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<WorkingDayTypeDTO> GetWorkingDayTypes()
        {
            try
            {
                return _context.WorkingDayTypes.Select(x => _mapper.Map<WorkingDayTypeDTO>(x)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        */
    }
}
