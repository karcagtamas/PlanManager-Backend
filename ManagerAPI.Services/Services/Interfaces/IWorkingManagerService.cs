using System;
using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IWorkingManagerService
    {
        WorkingDayListDto GetWorkingDay(DateTime day);
        void CreateWorkingDay(WorkingDayInitModel model);
        void UpdateWorkingDay(int workingDayId, WorkingDayModel model);
        void AddWorkingField(int workingDayId, WorkingFieldModel model);
        void DeleteWorkingField(int workingFieldId);
        void UpdateWorkingField(int workingFieldId, WorkingFieldModel model);
        List<WorkingDayTypeDto> GetWorkingDayTypes();
        WorkingFieldDto GetWorkingField(int id);
        WorkingDayStatDto GetWorkingDayStat(int id);
    }
}
