using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IMessageService : IRepository<Message>
    {
        List<MessageDto> GetMessages(string friendId);
        void SendMessage(MessageModel model);
    }
}