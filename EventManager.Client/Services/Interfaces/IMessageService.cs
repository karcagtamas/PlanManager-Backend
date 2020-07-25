using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services.Interfaces {
    public interface IMessageService {
        Task<List<MessageDto>> GetMessages (int friendId);
        Task<bool> SendMessage (MessageModel model);
    }
}