using System;
using ManagerAPI.Models.DTOs.WM;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkingManager.Services.Services;

namespace KarcagS.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WorkingManagerController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IWorkingManagerService _workingManagerService;
        private readonly ILoggerService _loggerService;

        public WorkingManagerController(IWorkingManagerService workingManagerService, ILoggerService loggerService)
        {
            _workingManagerService = workingManagerService;
            _loggerService = loggerService;
        }

        [HttpGet("{day}")]
        public IActionResult GetWorkingDay(DateTime day)
        {
            try
            {
                return Ok(_workingManagerService.GetWorkingDay(day));
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost]
        public IActionResult CreateWorkingDay([FromBody] WorkingDayDto workingDay)
        {
            try
            {
                _workingManagerService.CreateWorkingDay(workingDay);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("{workingDayId}")]
        public IActionResult UpdateWorkingDay(int workingDayId, [FromBody] WorkingDayDto workingDay)
        {
            try
            {
                _workingManagerService.UpdateWorkingDay(workingDayId, workingDay);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost("{workingDayId}/field")]
        public IActionResult AddWorkingField(int workingDayId, [FromBody] WorkingFieldDto workingField)
        {
            try
            {
                _workingManagerService.AddWorkingField(workingDayId, workingField);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpDelete("field/{workingFieldId}")]
        public IActionResult DeleteWorkingField(int workingFieldId)
        {
            try
            {
                _workingManagerService.DeleteWorkingField(workingFieldId);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("field/{workingFieldId}")]
        public IActionResult UpdateWorkingField(int workingFieldId, [FromBody] WorkingFieldDto workingField)
        {
            try
            {
                _workingManagerService.UpdateWorkingField(workingFieldId, workingField);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet("types")]
        public IActionResult GetWorkingDayTypes()
        {
            try
            {
                return Ok(_workingManagerService.GetWorkingDayTypes());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }
    }
}
