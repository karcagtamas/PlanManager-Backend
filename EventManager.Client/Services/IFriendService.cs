using EventManager.Client.Models;
using EventManager.Client.Models.Friends;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public interface IFriendService
    {
        Task<List<FriendRequestListDto>> GetMyFriendRequests();
        Task<List<FriendListDto>> GetMyFriends();
        Task<bool> RemoveFriend(string friendId);
        Task<bool> SendFriendRequest(FriendRequestModel model);
        Task<bool> SendFriendRequestResponse(FriendRequestResponseModel model);
    }
}