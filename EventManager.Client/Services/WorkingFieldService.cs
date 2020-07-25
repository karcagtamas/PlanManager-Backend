using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services
{
    public class WorkingFieldService : IWorkingFieldService
    {
        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/working-field";
        private readonly IHelperService _helperService;

        public WorkingFieldService(IHttpService httpService, IHelperService helperService)
        {
            this._httpService = httpService;
            this._helperService = helperService;
        }
        
        public async Task<bool> AddWorkingField(WorkingFieldModel model)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Working field adding");

            var body = new HttpBody<WorkingFieldModel>(this._helperService, model);

            return await this._httpService.create<WorkingFieldModel>(settings, body);
        }

        public async Task<bool> DeleteWorkingField(int workingFieldId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingFieldId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Working field deleting");

            return await this._httpService.delete(settings);
        }
        
        public async Task<WorkingFieldDto> GetWorkingField(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams);

            return await this._httpService.get<WorkingFieldDto>(settings);
        }

        public async Task<bool> UpdateWorkingField(int workingFieldId, WorkingFieldModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(workingFieldId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Working field updating");

            var body = new HttpBody<WorkingFieldModel>(this._helperService, model);

            return await this._httpService.update<WorkingFieldModel>(settings, body);
        }
    }
}