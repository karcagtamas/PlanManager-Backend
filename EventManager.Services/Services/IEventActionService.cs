using System.Collections.Generic;
using ManagerAPI.Models.DTOs;

namespace EventManager.Services.Services
{
    public interface IEventActionService
    {
        List<EventActionListDto> GetActions(int eventId);
    }
}