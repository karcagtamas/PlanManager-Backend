using EventManager.Client.Models;
using EventManager.Client.Models.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public interface IMessageService
    {
        Task<ApiResponseModel<List<MessageDto>>> GetMessages(int friendId);
        Task<ApiResponseModel<object>> SendMessage(MessageModel model);
    }
}