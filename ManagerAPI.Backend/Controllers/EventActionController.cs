using System;
using EventManager.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    
    [Route ("api/event/action")]
    [Authorize]
    [ApiController]
    public class EventActionController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IEventActionService _eventActionService;
        private readonly ILoggerService _loggerService;

        public EventActionController(IEventActionService eventActionService, ILoggerService loggerService)
        {
            _eventActionService = eventActionService;
            _loggerService = loggerService;
        }

        [HttpGet("{id}")]
        public IActionResult GetEventActions(int id)
        {
            try
            {
                return Ok(_eventActionService.GetActions(id));
            }
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
            
        }
    }
}