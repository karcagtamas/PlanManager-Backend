namespace EventManager.Client.Services
{
    public interface IFriendService
    {
        Task<ApiResponseModel<List<FriendRequestListDto>>> GetMyFriendRequests(FriendRequestFilterModel model);
        Task<ApiResponseModel<List<FriendListDto>>> GetMyFriends();
        Task<ApiResponseModel<object>> RemoveFriend(string friendId);
        Task<ApiResponseModel<object>> SendFriendRequest(FriendRequestModel model);
        Task<ApiResponseModel<object>> SendFriendRequestResponse(FriendRequestResponseModel model);
    }
}