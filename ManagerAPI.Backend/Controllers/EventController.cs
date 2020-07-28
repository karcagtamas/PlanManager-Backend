using System;
using EventManager.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.EM;
using ManagerAPI.Shared.Models;
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
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IEventService _eventService;
        private readonly ILoggerService _loggerService;

        public EventController(IEventService eventService, ILoggerService loggerService)
        {
            _eventService = eventService;
            _loggerService = loggerService;
        }

        [HttpGet("my")]
        public IActionResult GetMyEventsList()
        {
            try
            {
                return Ok(_eventService.GetMyEvents());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpGet("{eventId}")]
        public IActionResult GetEvent(int eventId)
        {
            try
            {
                return Ok(_eventService.GetEvent(eventId));
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventModel model)
        {
            try
            {
                _eventService.CreateEvent(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPost("gt/{eventId}")]
        public IActionResult SetEventAsGtEvent(int eventId)
        {
            try
            {
                _eventService.SetEventAsGtEvent(eventId);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPost("sport/{eventId}")]
        public IActionResult SetEventAsSportEvent(int eventId)
        {
            try
            {
                _eventService.SetEventAsSportEvent(eventId);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            try
            {
                _eventService.DeleteEvent(eventId);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut("{eventId}")]
        public IActionResult UpdateMaterEvent(int eventId, [FromBody] MasterEventUpdateDto masterUpdate)
        {
            try
            {
                _eventService.UpdateMasterEvent(masterUpdate);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut("sport/{sportEventId}")]
        public IActionResult UpdateSportEvent(int sportEventId, [FromBody] SportEventUpdateDto sportUpdate)
        {
            try
            {
                _eventService.UpdateSportEvent(sportUpdate);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut("gt/{gtEventId}")]
        public IActionResult UpdateGtEvent(int gtEventId, [FromBody] GtEventUpdateDto updateGt)
        {
            try
            {
                _eventService.UpdateGtEvent(updateGt);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }
    }
}