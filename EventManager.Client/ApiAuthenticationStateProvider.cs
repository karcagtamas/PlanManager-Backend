using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace EventManager.Client
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                await ClearStorage();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(await ParseClaimsFromJwt(savedToken), "jwt")));
        }

        public void MarkUserAsAuthenticated(string userName)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<IEnumerable<Claim>> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null && keyValuePairs.Keys.Contains("exp"))
            {
                long.TryParse(keyValuePairs["exp"].ToString(), out long exp);
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddSeconds(exp);
                if (date < DateTime.Now)
                {
                    await ClearStorage();
                    return null;
                }
            }
            else
            {
                await ClearStorage();
                return null;
            }

            foreach (var dic in keyValuePairs)
            {
                switch (dic.Key)
                {
                    case "unique_name":
                        claims.Add(new Claim(ClaimTypes.Name, dic.Value.ToString()));
                        break;
                    case "role":
                        if (dic.Value.ToString().Trim().StartsWith("["))
                        {
                            var parsedRoles = JsonSerializer.Deserialize<string[]>(dic.Value.ToString());

                            foreach (var parsedRole in parsedRoles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                            }
                        }
                        else
                        {
                            claims.Add(new Claim(ClaimTypes.Role, dic.Value.ToString()));
                        }
                        break;
                    case "email":
                        claims.Add(new Claim(ClaimTypes.Email, dic.Value.ToString()));
                        break;
                    default:
                        claims.Add(new Claim(dic.Key, dic.Value.ToString()));
                        break;
                }
            }
            return claims;
        }

        public async Task ClearStorage()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}