using EventManager.Client.Models;
using EventManager.Client.Models.Friends;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class FriendService : IFriendService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/friend";
        private readonly IHelperService _helperService;

        public FriendService(HttpClient httpClient, IHelperService helperService)
        {
            _httpClient = httpClient;
            _helperService = helperService;
        }

        public async Task<List<FriendRequestListDto>> GetMyFriendRequests()
        {
            var response = await _httpClient.GetAsync($"{_url}/request");

            if (response.IsSuccessStatusCode)
            {
                using (var sr = await response.Content.ReadAsStreamAsync()) 
                {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<FriendRequestListDto>>(sr, _helperService.GetSerializerOptions());
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FriendListDto>> GetMyFriends()
        {
            var response = await _httpClient.GetAsync($"{_url}");

            if (response.IsSuccessStatusCode)
            {
                using (var sr = await response.Content.ReadAsStreamAsync())
                {

                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<FriendListDto>>(sr, _helperService.GetSerializerOptions());
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveFriend(string friendId)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/{friendId}");

            await _helperService.AddToaster(response, "Friend removing");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendFriendRequest(FriendRequestModel model)
        {
            var response = await _httpClient.PostAsync($"{_url}/request", _helperService.CreateContent(model));

            await _helperService.AddToaster(response, "Friend request sending");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendFriendRequestResponse(FriendRequestResponseModel model)
        {
            var response = await _httpClient.PutAsync($"{_url}/request", _helperService.CreateContent(model));

            await _helperService.AddToaster(response, "Friend request answering");

            return response.IsSuccessStatusCode;
        }
    }
}
