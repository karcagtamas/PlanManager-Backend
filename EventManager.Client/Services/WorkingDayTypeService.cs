using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;

namespace EventManager.Client.Services
{
    public class WorkingDayTypeService : IWorkingDayTypeService
    {
        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/working-day-type";
        private readonly IHelperService _helperService;

        public WorkingDayTypeService(IHttpService httpService, IHelperService helperService)
        {
            this._httpService = httpService;
            this._helperService = helperService;
        }

        public async Task<List<WorkingDayTypeListDto>> GetWorkingDayTypes()
        {
            var settings = new HttpSettings($"{this._url}");

            return await this._httpService.get<List<WorkingDayTypeListDto>>(settings);
        }
    }
}