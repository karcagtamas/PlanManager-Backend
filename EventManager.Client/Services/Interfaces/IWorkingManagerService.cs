using EventManager.Client.Models.WM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingManagerService
    {
        Task<WorkingDayListDto> GetWorkingDay(DateTime day);
        Task<bool> CreateWorkingDay(WorkingDayInitModel model);
        Task<bool> UpdateWorkingDay(int workingDayId, WorkingDayModel model);
        Task<bool> AddWorkingField(int workingDayId, WorkingFieldModel model);
        Task<bool> DeleteWorkingField(int workingFieldId);
        Task<bool> UpdateWorkingField(int workingFieldId, WorkingFieldModel model);
        Task<List<WorkingDayTypeDto>> GetWorkingDayTypes();
    }
}
