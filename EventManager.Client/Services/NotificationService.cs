using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Notifications;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Services {
    public class NotificationService : INotificationService {
        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/notification";
        private readonly IHelperService _helperService;

        public NotificationService (HttpClient httpClient, IHelperService helperService) {
            _httpClient = httpClient;
            _helperService = helperService;
        }

        public async Task<int?> GetCountOfUnReadNotifications () {
            var response = await _httpClient.GetAsync ($"{_url}/unreads/count");

            if (response.IsSuccessStatusCode) {
                int count = -1;
                int.TryParse (await response.Content.ReadAsStringAsync (), out count);
                return count == -1 ? null : (int?) count;
            } else {
                return null;
            }
        }

        public async Task<List<NotificationDto>> GetMyNotifications () {
            var response = await _httpClient.GetAsync ($"{_url}");

            if (response.IsSuccessStatusCode) {
                using (var sr = await response.Content.ReadAsStreamAsync ()) {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<NotificationDto>> (sr, _helperService.GetSerializerOptions ());
                }
            } else {
                return null;
            }
        }

        public async Task<bool> SetUnReadsToRead (int[] ids) {
            var response = await _httpClient.PutAsync ($"{_url}", _helperService.CreateContent (ids));

            await _helperService.AddToaster (response, "Notification refreshing");

            return response.IsSuccessStatusCode;
        }
    }
}