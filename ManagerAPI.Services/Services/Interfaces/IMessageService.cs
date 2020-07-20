using System.Collections.Generic;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IMessageService
    {
        List<MessageDto> GetMessages(int friendId);
        void SendMessage(MessageModel model);
    }
}