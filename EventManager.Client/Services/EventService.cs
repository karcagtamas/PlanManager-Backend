using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.EM;
using ManagerAPI.Shared.Models.EM;

namespace EventManager.Client.Services
{
    public class EventService : IEventService
    {
        private readonly IHttpService _httpService;
        private readonly IHelperService _helperService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/event";

        public EventService(IHttpService httpService, IHelperService helperService)
        {
            _httpService = httpService;
            _helperService = helperService;
        }
        public async Task<List<MyEventListDto>> GetMyList()
        {
            var settings = new HttpSettings($"{this._url}/my");

            return await this._httpService.Get<List<MyEventListDto>>(settings);
        }

        public async Task<EventDto> Get(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams);

            return await this._httpService.Get<EventDto>(settings);
        }

        public async Task<bool> CreateEvent(EventModel model)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Event creating");

            var body = new HttpBody<EventModel>(model);

            return await this._httpService.Create<EventModel>(settings, body);
        }

        public async Task<bool> SetEventAsGt(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);

            var settings = new HttpSettings($"{this._url}/gt", null, pathParams, "Master event extending to Gt event");

            var body = new HttpBody<object>(null);

            return await this._httpService.Create<object>(settings, body);
        }

        public async Task<bool> SetEventAsSport(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);

            var settings = new HttpSettings($"{this._url}/sport", null, pathParams, "Master event extending to Sport event");

            var body = new HttpBody<object>(null);

            return await this._httpService.Create<object>(settings, body);
        }

        public async Task<bool> UpdateMasterEvent(MasterEventUpdateDto masterUpdate)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(masterUpdate.Id, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Master event updating");

            var body = new HttpBody<MasterEventUpdateDto>(masterUpdate);

            return await this._httpService.Update<MasterEventUpdateDto>(settings, body);
        }

        public async Task<bool> UpdateSportEvent(SportEventUpdateDto sportUpdate)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(sportUpdate.Id, -1);

            var settings = new HttpSettings($"{this._url}/sport", null, pathParams, "Sport event updating");

            var body = new HttpBody<SportEventUpdateDto>(sportUpdate);

            return await this._httpService.Update<SportEventUpdateDto>(settings, body);
        }

        public async Task<bool> UpdateGtEvent(GtEventUpdateDto gtUpdate)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(gtUpdate.Id, -1);

            var settings = new HttpSettings($"{this._url}/gt", null, pathParams, "Gt event updating");

            var body = new HttpBody<GtEventUpdateDto>(gtUpdate);

            return await this._httpService.Update<GtEventUpdateDto>(settings, body);
        }
    }
}