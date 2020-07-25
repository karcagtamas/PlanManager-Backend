using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.EM;

namespace EventManager.Services.Services
{
    public interface IEventActionService
    {
        List<EventActionListDto> GetActions(int eventId);
    }
}