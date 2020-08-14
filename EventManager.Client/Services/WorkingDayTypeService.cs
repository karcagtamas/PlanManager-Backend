using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services
{
    public class WorkingDayTypeService : HttpCall<WorkingDayTypeListDto, WorkingDayTypeDto, WorkingDayTypeModel>, IWorkingDayTypeService
    {

        public WorkingDayTypeService(IHttpService httpService) : base (httpService, $"{ApplicationSettings.BaseApiUrl}/working-day-type", "Working day type")
        {
        }
    }
}