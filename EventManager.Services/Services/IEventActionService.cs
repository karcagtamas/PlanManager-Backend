using System.Collections.Generic;
using ManagerAPI.Models.DTOs.EM;

namespace EventManager.Services.Services
{
    public interface IEventActionService
    {
        List<EventActionListDto> GetActions(int eventId);
    }
}