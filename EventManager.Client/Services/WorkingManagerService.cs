using EventManager.Client.Models;
using EventManager.Client.Models.WM;
using EventManager.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class WorkingManagerService : IWorkingManagerService
    {
        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/working-manager";
        private readonly IHelperService _helperService;

        public WorkingManagerService(IHttpService httpService, IHelperService helperService)
        {
            this._httpService = httpService;
            this._helperService = helperService;
        }

        public async Task<bool> AddWorkingField(int workingDayId, WorkingFieldDto workingField)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingDayId, -1);
            pathParams.Add<string>("field", -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Wokring field adding");

            var body = new HttpBody<WorkingFieldDto>(this._helperService, workingField);

            return await this._httpService.create<WorkingFieldDto>(settings, body);
        }

        public async Task<bool> CreateWorkingDay(WorkingDayDto workingDay)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Working day adding");

            var body = new HttpBody<WorkingDayDto>(this._helperService, workingDay);

            return await this._httpService.create<WorkingDayDto>(settings, body);
        }

        public async Task<bool> DeleteWorkingField(int workingFieldId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingFieldId, -1);

            var settings = new HttpSettings($"{this._url}/field", null, pathParams, "Wokring field deleting");

            return await this._httpService.delete(settings);
        }

        public async Task<WorkingDayListDto> GetWorkingDay(DateTime day)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(this._helperService.DateToNumberDayString(day), -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams);

            return await this._httpService.get<WorkingDayListDto>(settings);
        }

        public async Task<List<WorkingDayTypeDto>> GetWorkingDayTypes()
        {
            var settings = new HttpSettings($"{this._url}/types");

            return await this._httpService.get<List<WorkingDayTypeDto>>(settings);
        }

        public async Task<bool> UpdateWorkingDay(int workingDayId, WorkingDayDto workingDay)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingDayId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Wokring day updating");

            var body = new HttpBody<WorkingDayDto>(this._helperService, workingDay);

            return await this._httpService.update<WorkingDayDto>(settings, body);
        }

        public async Task<bool> UpdateWorkingField(int workingFieldId, WorkingFieldDto workingField)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingFieldId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Wokring field updating");

            var body = new HttpBody<WorkingFieldDto>(this._helperService, workingField);

            return await this._httpService.update<WorkingFieldDto>(settings, body);
        }
    }
}
