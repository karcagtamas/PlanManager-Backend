using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using EventManager.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace EventManager.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/auth";

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }
        
        public async Task<ApiResponseModel<object>> Register(RegistrationModel model)
        {
            var result = await _httpClient.PostJsonAsync<ApiResponseModel<object>>(_url + "/registration", model);

            return result;
        }

        public async Task<ApiResponseModel<string>> Login(LoginModel model)
        {
            var loginAsJson = JsonSerializer.Serialize(model);
            var response = await _httpClient.PostAsync(_url + "/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<ApiResponseModel<string>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

            if (!loginResult.IsSuccess)
            {
                return loginResult;
            }

            await _localStorageService.SetItemAsync("authToken", loginResult.Content);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(model.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Content);

            return loginResult;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).ClearStorage();
        }
    }
}