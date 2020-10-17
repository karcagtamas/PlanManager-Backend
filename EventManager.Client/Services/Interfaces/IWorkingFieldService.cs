using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using System;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingFieldService : IHttpCall<WorkingFieldListDto, WorkingFieldDto, WorkingFieldModel>
    {
        Task<WorkingWeekStatDto> GetWeekStat(DateTime week);
        Task<WorkingMonthStatDto> GetMonthStat(int year, int month);
    }
}