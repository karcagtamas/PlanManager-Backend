using System.Collections.Generic;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;

namespace ManagerAPI.Services.Services
{
    public interface IFriendService
    {
        List<FriendListDto> GetMyFriends();
        void RemoveFriend(string friendId);
        List<FriendRequestListDto> GetMyFriendRequests();
        void SendFriendRequestResponse(FriendRequestResponseModel model);
        void SendFriendRequest(FriendRequestModel model);
    }
}