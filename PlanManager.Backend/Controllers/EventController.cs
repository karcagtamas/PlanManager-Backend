using System;
using System.Collections.Generic;
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
    }
}