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

        public Task<bool> AddWorkingField(int workingDayId, WorkingFieldDto workingField)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateWorkingDay(WorkingDayDto workingDay)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Working day adding");

            var body = new HttpBody<WorkingDayDto>(this._helperService, workingDay);

            return await this._httpService.create<WorkingDayDto>(settings, body);
        }

        public Task<bool> DeleteWorkingField(int workingFieldId)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkingDayListDto> GetWorkingDay(DateTime day)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(this._helperService.DateToNumberDayString(day), -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams);

            return await this._httpService.get<WorkingDayListDto>(settings);
        }

        public Task<List<WorkingDayTypeDto>> GetWorkingDayTypes()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWorkingDay(int workingDayId, WorkingDayDto workingDay)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWorkingField(int workingFieldId, WorkingFieldDto workingField)
        {
            throw new NotImplementedException();
        }
    }
}
