namespace EventManager.Client.Services
{
    public interface IMessageService
    {
        Task<ApiResponseModel<List<MessageDto>>> GetMessages(int friendId);
        Task<ApiResponseModel<object>> SendMessage(MessageModel model);
    }
}