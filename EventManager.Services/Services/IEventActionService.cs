using System.Collections.Generic;
using EventManager.Services.DTOs;

namespace EventManager.Services.Services
{
    public interface IEventActionService
    {
        List<EventActionListDto> GetActions(int eventId);
    }
}