using System.Collections.Generic;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;

namespace ManagerAPI.Services.Services
{
    public interface IMessageService
    {
        List<MessageDto> GetMessages(int friendId);
        void SendMessage(MessageModel model);
    }
}