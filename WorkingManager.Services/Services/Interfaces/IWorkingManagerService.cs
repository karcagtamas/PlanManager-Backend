using System;
using System.Collections.Generic;
using ManagerAPI.Models.DTOs.WM;

namespace WorkingManager.Services.Services.Interfaces
{
    public interface IWorkingManagerService
    {
        WorkingDayListDto GetWorkingDay(DateTime day);
        void CreateWorkingDay(WorkingDayDto workingDay);
        void UpdateWorkingDay(int workingDayId, WorkingDayDto workingDay);
        void AddWorkingField(int workingDayId, WorkingFieldDto workingField);
        void DeleteWorkingField(int workingFieldId);
        void UpdateWorkingField(int workingFieldId, WorkingFieldDto workingField);
        List<WorkingDayTypeDto> GetWorkingDayTypes();
    }
}
