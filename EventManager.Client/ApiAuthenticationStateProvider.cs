using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManager.Client
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this._httpClient = httpClient;
            this._localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string savedToken = await this._localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                await this.ClearStorage();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(await this.ParseClaimsFromJwt(savedToken), "jwt")));
        }

        public void MarkUserAsAuthenticated()
        {
            this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
        }

        private void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            this.NotifyAuthenticationStateChanged(authState);
        }

        private async Task<IEnumerable<Claim>> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            string payload = jwt.Split('.')[1];
            byte[] jsonBytes = this.ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null && keyValuePairs.Keys.Contains("exp"))
            {
                long.TryParse(keyValuePairs["exp"].ToString(), out long exp);
                var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddSeconds(exp);
                if (date < DateTime.Now)
                {
                    await this.ClearStorage();
                    return null;
                }
            }
            else
            {
                await this.ClearStorage();
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
                            string[] parsedRoles = JsonSerializer.Deserialize<string[]>(dic.Value.ToString());

                            foreach (string parsedRole in parsedRoles)
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
            await this._localStorageService.RemoveItemAsync("authToken");
            this.MarkUserAsLoggedOut();
            this._httpClient.DefaultRequestHeaders.Authorization = null;
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