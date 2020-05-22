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

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<ApiResponseModel<UserDto>> GetUser()
        {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<UserDto>>($"{_url}");

            return result;
        }

        public async Task<ApiResponseModel<UserShortDto>> GetShortUser()
        {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<UserShortDto>>($"{_url}/shorter");

            return result;
        }

        public async Task<ApiResponseModel<object>> UpdateUser(UserUpdateDto userUpdate)
        {
            var result = await _httpClient.PutJsonAsync<ApiResponseModel<object>>($"{_url}", userUpdate);

            return result;
        }

        public async Task<ApiResponseModel<List<GenderDto>>> GetGenders()
        {
            var result = await _httpClient.GetJsonAsync<ApiResponseModel<List<GenderDto>>>($"{_url}/genders");

            return result;
        }

        public async Task<ApiResponseModel<object>> UpdatePassword(PasswordUpdateModel model)
        {
            var result = await _httpClient.PutJsonAsync<ApiResponseModel<object>>($"{_url}/password", model);

            return result;
        }

        public async Task<ApiResponseModel<object>> UpdateProfileImage(byte[] image)
        {
            var result = await _httpClient.PutJsonAsync<ApiResponseModel<object>>($"{_url}/profile-image", image);

            return result;
        }
        
        public async Task<ApiResponseModel<object>> UpdateUsername(UsernameUpdateModel model)
        {
            var result = await _httpClient.PutJsonAsync<ApiResponseModel<object>>($"{_url}/username", model);

            return result;
        }
        
        public async Task<ApiResponseModel<object>> DisableUser()
        {
            var result = await _httpClient.PutJsonAsync<ApiResponseModel<object>>($"{_url}/disable", null);

            return result;
        }
    }
}