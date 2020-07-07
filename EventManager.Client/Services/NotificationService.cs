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
        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/notification";
        private readonly IHelperService _helperService;
        private readonly HttpClient _httpClient;

        public NotificationService (IHttpService httpService, IHelperService helperService, HttpClient httpClient) {
            this._httpService = httpService;
            this._helperService = helperService;
            this._httpClient = httpClient;
        }

        public async Task<int?> GetCountOfUnReadNotifications () {
            var settings = new HttpSettings($"{this._url}/unreads/count");

            return await _httpService.getInt(settings);
        }

        public async Task<List<NotificationDto>> GetMyNotifications () {
            var settings = new HttpSettings($"{this._url}");

            return await this._httpService.get<List<NotificationDto>> (settings);
        }

        public async Task<bool> SetUnReadsToRead (int[] ids) {
            var settings = new HttpSettings ($"{this._url}", null, null, "Notification refreshing");

            var body = new HttpBody<int[]>(this._helperService, ids);

            return await this._httpService.update<int[]>(settings, body);
        }
    }
}