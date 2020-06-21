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

        public async Task<ApiResponseModel<List<NotificationDto>>> GetMyNotifications()
        {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<List<NotificationDto>>>($"{_url}");

            return result;
        }
    }
}
