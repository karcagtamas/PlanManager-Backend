using System;
using System.Collections.Generic;
using ManagerAPI.Models.DTOs;

namespace WorkingManager.Services.Services
{
    public interface IWorkingManagerService
    {
        WorkingDayListDto GetWorkingDay(string userId, DateTime day);

        void CreateWorkingDay(string userId, WorkingDayDto workingDay);

        void UpdateWorkingDay(string userId, int workingDayId, WorkingDayDto workingDay);

        void AddWorkingField(string userId, int workingDayId, WorkingFieldDto workingField);

        void DeleteWorkingField(string userId, int workingFieldId);
        

        void UpdateWorkingField(string userId, int workingFieldId, WorkingFieldDto workingField);

        List<WorkingDayTypeDto> GetWorkingDayTypes();
    }
}
