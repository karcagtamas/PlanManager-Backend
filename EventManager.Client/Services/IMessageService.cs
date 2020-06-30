using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Messages;

namespace EventManager.Client.Services {
    public interface IMessageService {
        Task<List<MessageDto>> GetMessages (int friendId);
        Task<bool> SendMessage (MessageModel model);
    }
}