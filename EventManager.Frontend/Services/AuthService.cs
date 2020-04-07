using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EventManager.Frontend.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task Login(string userName, string password)
        {
            await _http.PostJsonAsync("localhost:8080/api/auth/login", new {userName, password});
        }
    }
}