using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Events;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Services {
    public class EventService : IEventService {
        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/event";

        public EventService (HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task<ApiResponseModel<List<MyEventListDto>>> GetMyList () {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<List<MyEventListDto>>> ($"{_url}/my");

            return result;
        }

        public async Task<ApiResponseModel<EventDto>> Get (int id) {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<EventDto>> ($"{_url}/{id}");

            return result;
        }

        public async Task<ApiResponseModel<Object>> CreateEvent (EventModel model) {
            var result = await _httpClient.PostJsonAsync<ApiResponseModel<Object>> ($"{_url}", model);

            return result;
        }

        public async Task<ApiResponseModel<object>> SetEventAsGt (int id) {
            var result = await _httpClient.PostJsonAsync<ApiResponseModel<Object>> ($"{_url}/gt/{id}", null);
            return result;
        }

        public async Task<ApiResponseModel<object>> SetEventAsSport (int id) {
            var result = await _httpClient.PostJsonAsync<ApiResponseModel<Object>> ($"{_url}/sport/{id}", null);
            return result;
        }

        public async Task<ApiResponseModel<object>> UpdateMasterEvent (MasterEventUpdateDto masterUpdate) {
            var result =
                await _httpClient.PutJsonAsync<ApiResponseModel<Object>> ($"{_url}/{masterUpdate.Id}", masterUpdate);
            return result;
        }

        public async Task<ApiResponseModel<object>> UpdateSportEvent (SportEventUpdateDto sportUpdate) {
            var result =
                await _httpClient.PutJsonAsync<ApiResponseModel<Object>> ($"{_url}/sport/{sportUpdate.Id}", sportUpdate);
            return result;
        }

        public async Task<ApiResponseModel<object>> UpdateGtEvent (GtEventUpdateDto gtUpdate) {
            var result =
                await _httpClient.PutJsonAsync<ApiResponseModel<Object>> ($"{_url}/gt/{gtUpdate.Id}", gtUpdate);
            return result;
        }
    }
}