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
    
    [Route ("api/event/action")]
    [Authorize]
    [ApiController]
    public class EventActionController : ControllerBase
    {
        private readonly IEventActionService _eventActionService;
        private readonly IUtilsService _utilsService;

        public EventActionController(IEventActionService eventActionService, IUtilsService utilsService)
        {
            _eventActionService = eventActionService;
            _utilsService = utilsService;
        }

        [HttpGet("{id}")]
        public IActionResult GetEventActions(int id)
        {
            try
            {
                return Ok(new ServerResponse<List<EventActionListDto>>(_eventActionService.GetActions(id), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
            
        }
    }
}