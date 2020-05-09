using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanManager.DataAccess.Models;
using PlanManager.Services.DTOs.EM;
using PlanManager.Services.Services;
using PlanManager.Services.Services.EM;

namespace PlanManager.Backend.Controllers
{
    
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUtilsService _utilsService;

        public EventController(IEventService eventService, IUtilsService utilsService)
        {
            _eventService = eventService;
            _utilsService = utilsService;
        }

        [HttpGet("my")]
        public IActionResult GetMyEventsList()
        {
            try
            {
                return Ok(new ServerResponse<List<MyEventListDto>>(_eventService.GetMyEvents(), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
            
        }

        [HttpGet("{eventId}")]
        public IActionResult GetEvent(int eventId)
        {
            try
            {
                return Ok(new ServerResponse<EventDto>(_eventService.GetEvent(eventId), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventCreateDto model)
        {
            try
            {
                _eventService.CreateEvent(model);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPost("gt/{eventId}")]
        public IActionResult SetEventAsGtEvent(int eventId)
        {
            try
            {
                _eventService.SetEventAsGtEvent(eventId);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }
        
        [HttpPost("sport/{eventId}")]
        public IActionResult SetEventAsSportEvent(int eventId)
        {
            try
            {
                _eventService.SetEventAsSportEvent(eventId);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            try
            {
                _eventService.DeleteEvent(eventId);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPut("{eventId}")]
        public IActionResult UpdateMaterEvent(int eventId, [FromBody] MasterEventUpdateDto masterUpdate)
        {
            try
            {
                _eventService.UpdateMasterEvent(masterUpdate);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }
        
        [HttpPut("sport/{sportEventId}")]
        public IActionResult UpdateSportEvent(int sportEventId, [FromBody] SportEventUpdateDto sportUpdate)
        {
            try
            {
                _eventService.UpdateSportEvent(sportUpdate);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }
        
        [HttpPut("gt/{gtEventId}")]
        public IActionResult UpdateGtEvent(int gtEventId, [FromBody] GtEventUpdateDto updateGt)
        {
            try
            {
                _eventService.UpdateGtEvent(updateGt);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }
    }
}