using EventManager.Client.Models;
using EventManager.Client.Models.Notifications;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/notification";

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseModel<int>> GetCountOfUnReadNotifications()
        {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<int>>($"{_url}/unreads/count");

            return result;
        }

        public async Task<ApiResponseModel<List<NotificationDto>>> GetMyNotifications()
        {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<List<NotificationDto>>>($"{_url}");

            return result;
        }

        public async Task<ApiResponseModel<object>> SetUnReadsToRead(int[] ids)
        {
            var result = await _httpClient.PutJsonAsync<ApiResponseModel<object>>($"{_url}", ids);

            return result;
        }
    }
}
