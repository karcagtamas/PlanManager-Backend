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
        Task<bool> CreateWorkingDay(WorkingDayDto workingDay);
        Task<bool> UpdateWorkingDay(int workingDayId, WorkingDayDto workingDay);
        Task<bool> AddWorkingField(int workingDayId, WorkingFieldDto workingField);
        Task<bool> DeleteWorkingField(int workingFieldId);
        Task<bool> UpdateWorkingField(int workingFieldId, WorkingFieldDto workingField);
        Task<List<WorkingDayTypeDto>> GetWorkingDayTypes();
    }
}
