using System;
using System.Threading.Tasks;
using EventManager.Client.Models.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingDayService : IHttpCall<WorkingDayListDto, WorkingDayDto, WorkingDayModel>
    {
        Task<WorkingDayListDto> Get(DateTime day);
    }
}