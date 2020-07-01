using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using EventManager.Client.Models;
using EventManager.Client.Models.Auth;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace EventManager.Client.Services {
    public class AuthService : IAuthService {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly IHelperService _helperService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/auth";

        public AuthService (HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService, IHelperService helperService) {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
            _helperService = helperService;
        }

        public async Task<bool> Register (RegistrationModel model) {
            var response = await _httpClient.PostAsync (_url + "/registration", _helperService.CreateContent (model));

            await _helperService.AddToaster (response, "Registration");

            return response.IsSuccessStatusCode;
        }

        public async Task<string> Login (LoginModel model) {
            var response = await _httpClient.PostAsync (_url + "/login", _helperService.CreateContent (model));

            await _helperService.AddToaster (response, "Login");

            if (response.IsSuccessStatusCode) {
                string token = await response.Content.ReadAsStringAsync ();
                await _localStorageService.SetItemAsync ("authToken", token);
                ((ApiAuthenticationStateProvider) _authenticationStateProvider).MarkUserAsAuthenticated (model.UserName);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("bearer", token);
                return token;
            } else {
                return "";
            }
        }

        public async Task Logout () {
            await ((ApiAuthenticationStateProvider) _authenticationStateProvider).ClearStorage ();
        }
    }
}