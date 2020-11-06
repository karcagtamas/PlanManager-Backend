using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using System;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingDayService : IHttpCall<WorkingDayListDto, WorkingDayDto, WorkingDayModel>
    {
        Task<WorkingDayListDto> Get(DateTime day);
        Task<WorkingDayStatDto> Stat(int id);
    }
}