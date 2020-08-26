using EventManager.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/event/action")]
    [Authorize]
    [ApiController]
    public class EventActionController : ControllerBase
    {
        private readonly IEventActionService _eventActionService;

        public EventActionController(IEventActionService eventActionService)
        {
            _eventActionService = eventActionService;
        }

        [HttpGet("{id}")]
        public IActionResult GetEventActions(int id)
        {
            return Ok(_eventActionService.GetActions(id));
        }
    }
}