using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.User;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Services
{
    public class UserService : IUserService
    {
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/user";
        private readonly IHelperService _helperService;
        private readonly IHttpService _httpService;

        public UserService(IHttpService httpService, IHelperService helperService)
        {
            _httpService = httpService;
            _helperService = helperService;
        }

        public async Task<UserDto> GetUser()
        {
            var settings = new HttpSettings($"{this._url}");

            return await this._httpService.get<UserDto>(settings);
        }

        public async Task<UserShortDto> GetShortUser()
        {
            var settings = new HttpSettings($"{this._url}/shorter");

            return await this._httpService.get<UserShortDto>(settings);
        }

        public async Task<bool> UpdateUser(UserUpdateDto userUpdate)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "User updating");

            var body = new HttpBody<UserUpdateDto>(this._helperService, userUpdate);

            return await this._httpService.update<UserUpdateDto>(settings, body);
        }

        public async Task<List<GenderDto>> GetGenders()
        {
            var settings = new HttpSettings($"{this._url}/genders");

            return await this._httpService.get<List<GenderDto>>(settings);
        }

        public async Task<bool> UpdatePassword(PasswordUpdateModel model)
        {
            var settings = new HttpSettings($"{this._url}/password", null, null, "Password updating");

            var body = new HttpBody<PasswordUpdateModel>(this._helperService, model);

            return await this._httpService.update<PasswordUpdateModel>(settings, body);
        }

        public async Task<bool> UpdateProfileImage(byte[] image)
        {
            var settings = new HttpSettings($"{this._url}/profile-image", null, null, "Image updating");

            var body = new HttpBody<byte[]>(this._helperService, image);

            return await this._httpService.update<byte[]>(settings, body);
        }

        public async Task<bool> UpdateUsername(UsernameUpdateModel model)
        {
            var settings = new HttpSettings($"{this._url}/username", null, null, "User Name updating");

            var body = new HttpBody<UsernameUpdateModel>(this._helperService, model);

            return await this._httpService.update<UsernameUpdateModel>(settings, body);
        }

        public async Task<bool> DisableUser()
        {
            var settings = new HttpSettings($"{this._url}/disable", null, null, "User disabling");

            var body = new HttpBody<object>(this._helperService, null);

            return await this._httpService.update<object>(settings, body);
        }
    }
}