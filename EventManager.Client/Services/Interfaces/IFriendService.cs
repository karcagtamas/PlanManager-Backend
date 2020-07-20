using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services.Interfaces
{
    public interface IFriendService
    {
        Task<List<FriendRequestListDto>> GetMyFriendRequests();
        Task<List<FriendListDto>> GetMyFriends();
        Task<bool> RemoveFriend(string friendId);
        Task<bool> SendFriendRequest(FriendRequestModel model);
        Task<bool> SendFriendRequestResponse(FriendRequestResponseModel model);
        Task<FriendDataDto> GetFriendData(string friendId);
    }
}