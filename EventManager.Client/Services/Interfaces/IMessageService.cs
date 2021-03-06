using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMessageService : IHttpCall<MessageListDto, MessageDto, MessageModel>
    {
        Task<List<MessageDto>> GetMessages(int friendId);
        Task<bool> SendMessage(MessageModel model);
    }
}