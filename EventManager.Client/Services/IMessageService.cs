using EventManager.Client.Models;
using EventManager.Client.Models.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetMessages(int friendId);
        Task<bool> SendMessage(MessageModel model);
    }
}