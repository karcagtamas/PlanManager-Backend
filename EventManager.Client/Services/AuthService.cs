using Blazored.LocalStorage;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly IHelperService _helperService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/auth";

        public AuthService(IHttpService httpService, HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService, IHelperService helperService)
        {
            this._httpService = httpService;
            this._authenticationStateProvider = authenticationStateProvider;
            this._localStorageService = localStorageService;
            this._helperService = helperService;
            this._httpClient = httpClient;
        }

        public async Task<bool> Register(RegistrationModel model)
        {
            var settings = new HttpSettings($"{this._url}/registration", null, null, "Registration");

            var body = new HttpBody<RegistrationModel>(model);

            return await this._httpService.Create<RegistrationModel>(settings, body);
        }

        public async Task<string> Login(LoginModel model)
        {
            var settings = new HttpSettings($"{this._url}/login", null, null, "Login");

            var body = new HttpBody<LoginModel>(model);

            string result = await this._httpService.CreateString<LoginModel>(settings, body);

            if (!string.IsNullOrEmpty(result))
            {
                await this._localStorageService.SetItemAsync("authToken", result);
                this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result);
                ((ApiAuthenticationStateProvider)this._authenticationStateProvider).MarkUserAsAuthenticated();
            }

            return result;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)this._authenticationStateProvider).ClearStorage();
        }

        public async Task<bool> HasRole(params string[] roles)
        {
            var state = await this._authenticationStateProvider.GetAuthenticationStateAsync();
            var claims = state.User.Claims.Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value).ToList();

            return claims.Any(roles.Contains);
        }

        public async Task<bool> IsLoggedIn()
        {
            return !string.IsNullOrEmpty(await this._localStorageService.GetItemAsync<string>("authToken"));
        }
    }
}