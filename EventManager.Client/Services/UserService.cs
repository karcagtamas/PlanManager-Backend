using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.User;
using Microsoft.AspNetCore.Components;
using PasswordUpdateModel = EventManager.Client.Models.PasswordUpdateModel;

namespace EventManager.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/user";
        private readonly IHelperService _helperService;

        public UserService(HttpClient httpClient, IHelperService helperService)
        {
            _httpClient = httpClient;
            _helperService = helperService;
        }
        
        public async Task<UserDto> GetUser()
        {
            var response = await _httpClient.GetAsync($"{_url}");

            if (response.IsSuccessStatusCode)
            {
                using (var sr = await response.Content.ReadAsStreamAsync()) 
                {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<UserDto>(sr, _helperService.GetSerializerOptions());
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<UserShortDto> GetShortUser()
        {
            var response = await _httpClient.GetAsync($"{_url}/shorter");

            if (response.IsSuccessStatusCode)
            {
                using (var sr = await response.Content.ReadAsStreamAsync()) 
                {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<UserShortDto>(sr, _helperService.GetSerializerOptions());
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserUpdateDto userUpdate)
        {
            var response = await _httpClient.PutAsync($"{_url}", _helperService.CreateContent(userUpdate));

            await _helperService.AddToaster(response, "User update");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<GenderDto>> GetGenders()
        {
            var response = await _httpClient.GetAsync($"{_url}/genders");

            if (response.IsSuccessStatusCode)
            {
                using (var sr = await response.Content.ReadAsStreamAsync()) 
                {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<GenderDto>>(sr, _helperService.GetSerializerOptions());
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdatePassword(PasswordUpdateModel model)
        {
            var response = await _httpClient.PutAsync($"{_url}/password", _helperService.CreateContent(model));

            await _helperService.AddToaster(response, "Password update");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProfileImage(byte[] image)
        {
            var response = await _httpClient.PutAsync($"{_url}/profile-image", _helperService.CreateContent(image));

            await _helperService.AddToaster(response, "Image update");

            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> UpdateUsername(UsernameUpdateModel model)
        {
            var response = await _httpClient.PutAsync($"{_url}/username", _helperService.CreateContent(model));

            await _helperService.AddToaster(response, "UserUame update");

            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> DisableUser()
        {
            var response = await _httpClient.PutAsync($"{_url}/disable", null);

            await _helperService.AddToaster(response, "Disable user");

            return response.IsSuccessStatusCode;
        }
    }
}