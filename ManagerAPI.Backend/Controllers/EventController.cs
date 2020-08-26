using EventManager.Services.Services;
using ManagerAPI.Shared.Models.EM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("my")]
        public IActionResult GetMyEventsList()
        {
            return Ok(_eventService.GetMyEvents());
        }

        [HttpGet("{eventId}")]
        public IActionResult GetEvent(int eventId)
        {
            return Ok(_eventService.GetEvent(eventId));
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventModel model)
        {
            _eventService.CreateEvent(model);
            return Ok();
        }

        [HttpPost("gt/{eventId}")]
        public IActionResult SetEventAsGtEvent(int eventId)
        {
            _eventService.SetEventAsGtEvent(eventId);
            return Ok();
        }

        [HttpPost("sport/{eventId}")]
        public IActionResult SetEventAsSportEvent(int eventId)
        {
            _eventService.SetEventAsSportEvent(eventId);
            return Ok();
        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            _eventService.DeleteEvent(eventId);
            return Ok();
        }

        [HttpPut("{eventId}")]
        public IActionResult UpdateMaterEvent(int eventId, [FromBody] MasterEventModel masterUpdate)
        {
            _eventService.UpdateMasterEvent(masterUpdate);
            return Ok();
        }

        [HttpPut("sport/{sportEventId}")]
        public IActionResult UpdateSportEvent(int sportEventId, [FromBody] SportEventModel sportUpdate)
        {
            _eventService.UpdateSportEvent(sportUpdate);
            return Ok();
        }

        [HttpPut("gt/{gtEventId}")]
        public IActionResult UpdateGtEvent(int gtEventId, [FromBody] GtEventModel updateGt)
        {
            _eventService.UpdateGtEvent(updateGt);
            return Ok();
        }
    }
}