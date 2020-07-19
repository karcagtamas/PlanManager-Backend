using System;
using System.Collections.Generic;
using ManagerAPI.Models.DTOs.WM;
using ManagerAPI.Models.Models.WM;

namespace WorkingManager.Services.Services.Interfaces
{
    public interface IWorkingManagerService
    {
        WorkingDayListDto GetWorkingDay(DateTime day);
        void CreateWorkingDay(WorkingDayInitModel model);
        void UpdateWorkingDay(int workingDayId, WorkingDayModel model);
        void AddWorkingField(int workingDayId, WokringFieldModel model);
        void DeleteWorkingField(int workingFieldId);
        void UpdateWorkingField(int workingFieldId, WokringFieldModel model);
        List<WorkingDayTypeDto> GetWorkingDayTypes();
    }
}
