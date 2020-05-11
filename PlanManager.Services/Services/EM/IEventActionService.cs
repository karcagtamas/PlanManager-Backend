using System.Collections.Generic;
using PlanManager.Services.DTOs.EM;

namespace PlanManager.Services.Services.EM
{
    public interface IEventActionService
    {
        List<EventActionListDto> GetActions(int eventId);
    }
}