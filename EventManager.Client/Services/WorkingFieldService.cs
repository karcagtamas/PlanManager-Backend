using System;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services
{
    public class WorkingFieldService : HttpCall<WorkingFieldListDto, WorkingFieldDto, WorkingFieldModel>, IWorkingFieldService
    {
        private IHelperService _helper;
        public WorkingFieldService(IHttpService httpService, IHelperService helperService, IHelperService helper) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/working-field", "Working field")
        {
            _helper = helper;
        }

        public async Task<WorkingWeekStatDto> GetWeekStat(DateTime week)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(this._helper.DateToNumberDayString(week), -1);

            var settings = new HttpSettings($"{this.Url}/week-stat", null, pathParams);

            return await this.Http.Get<WorkingWeekStatDto>(settings);
        }

        public async Task<WorkingMonthStatDto> GetMonthStat(int year, int month)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(year, -1);
            pathParams.Add<int>(month, -1);

            var settings = new HttpSettings($"{this.Url}/month-stat", null, pathParams);

            return await this.Http.Get<WorkingMonthStatDto>(settings);
        }
    }
}