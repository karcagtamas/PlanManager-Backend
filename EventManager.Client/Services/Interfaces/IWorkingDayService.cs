using System;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingDayService
    {
        Task<WorkingDayListDto> GetWorkingDay(DateTime day);
        Task<bool> CreateWorkingDay(WorkingDayModel model);
        Task<bool> UpdateWorkingDay(int workingDayId, WorkingDayModel model);
    }
}