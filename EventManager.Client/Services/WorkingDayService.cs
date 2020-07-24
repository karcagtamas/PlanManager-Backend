using System;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services
{
    public class WorkingDayService : IWorkingDayService
    {
        private readonly IHttpService _http;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/working-day";
        private readonly IHelperService _helper;

        public WorkingDayService(IHttpService http, IHelperService helper)
        {
            this._http = http;
            this._helper = helper;
        }

        public async Task<WorkingDayListDto> GetWorkingDay(DateTime day)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(this._helper.DateToNumberDayString(day), -1);

            var settings = new HttpSettings($"{this._url}/day", null, pathParams);

            return await this._http.get<WorkingDayListDto>(settings);
        }

        public async Task<bool> CreateWorkingDay(WorkingDayModel model)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Working day adding");

            var body = new HttpBody<WorkingDayModel>(this._helper, model);

            return await this._http.create<WorkingDayModel>(settings, body);
        }

        public async Task<bool> UpdateWorkingDay(int workingDayId, WorkingDayModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingDayId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Working day updating");

            var body = new HttpBody<WorkingDayModel>(this._helper, model);

            return await this._http.update<WorkingDayModel>(settings, body);
        }
    }
}