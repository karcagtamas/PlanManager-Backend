using System.Collections.Generic;
using PlanManager.Services.DTOs.EM;

namespace PlanManager.Services.Services.EM
{
    public interface IEventService
    {
        List<MyEventListDto> GetMyEvents();
    }
}