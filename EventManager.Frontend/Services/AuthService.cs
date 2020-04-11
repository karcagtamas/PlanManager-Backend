using System;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Frontend.Data;
using EventManager.Frontend.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace EventManager.Frontend.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public AuthService(HttpClient httpClient, IOptions<ApplicationSettings> applicationSettings)
        {
            _httpClient = httpClient;
            var appSettings = applicationSettings.Value;
            _url = $"{appSettings.ApiUrl}/auth";
        }
        
        public async Task<ApiResponse<string>> SignIn(LoginModel model)
        {
            return await _httpClient.PostJsonAsync<ApiResponse<string>>($"{_url}/login", model);
        }
    }
}