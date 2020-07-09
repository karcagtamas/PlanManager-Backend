using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Friends;
using EventManager.Client.Services.Interfaces;

namespace EventManager.Client.Services
{
    public class FriendService : IFriendService
    {
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/friend";
        private readonly IHelperService _helperService;
        private readonly IHttpService _httpService;

        public FriendService(HttpClient httpClient, IHelperService helperService, IHttpService httpService)
        {
            this._helperService = helperService;
            this._httpService = httpService;
        }

        public async Task<FriendDataDto> GetFriendData(string friendId)
        {
            var settings = new HttpSettings($"{this._url}/data");

            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(friendId, -1);

            return await this._httpService.get<FriendDataDto>(settings);
        }

        public async Task<List<FriendRequestListDto>> GetMyFriendRequests()
        {
            var settings = new HttpSettings($"{this._url}/request");

            return await this._httpService.get<List<FriendRequestListDto>>(settings);
        }

        public async Task<List<FriendListDto>> GetMyFriends()
        {
            var settings = new HttpSettings($"{this._url}");

            return await this._httpService.get<List<FriendListDto>>(settings);
        }

        public async Task<bool> RemoveFriend(string friendId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<string>(friendId, -1);

            var toaster = new ToasterSettings("Friend removing");

            var settings = new HttpSettings($"{this._url}", null, pathParams, toaster);

            return await this._httpService.delete(settings);
        }

        public async Task<bool> SendFriendRequest(FriendRequestModel model)
        {
            var toaster = new ToasterSettings("Friend request sending");

            var settings = new HttpSettings($"{this._url}/request", null, null, toaster);

            var body = new HttpBody<FriendRequestModel>(this._helperService, model);

            return await this._httpService.create<FriendRequestModel>(settings, body);
        }

        public async Task<bool> SendFriendRequestResponse(FriendRequestResponseModel model)
        {
            var toaster = new ToasterSettings("Friend request answering");

            var settings = new HttpSettings($"{this._url}/request", null, null, toaster);

            var body = new HttpBody<FriendRequestResponseModel>(_helperService, model);

            return await this._httpService.update<FriendRequestResponseModel>(settings, body);
        }
    }
}