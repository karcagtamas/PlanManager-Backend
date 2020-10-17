using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Helpers;
using ManagerAPI.Shared.Models.WM;
using System;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class WorkingDayService : HttpCall<WorkingDayListDto, WorkingDayDto, WorkingDayModel>, IWorkingDayService
    {
        public WorkingDayService(IHttpService http) : base(http, $"{ApplicationSettings.BaseApiUrl}/working-day", "Working day")
        {
        }

        public async Task<WorkingDayListDto> Get(DateTime day)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(DateHelper.DateToNumberDayString(day), -1);

            var settings = new HttpSettings($"{this.Url}/day", null, pathParams);

            return await this.Http.Get<WorkingDayListDto>(settings);
        }

        public async Task<WorkingDayStatDto> Stat(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            pathParams.Add<string>("stat", -1);

            var settings = new HttpSettings($"{this.Url}", null, pathParams);

            return await this.Http.Get<WorkingDayStatDto>(settings);
        }
    }
}