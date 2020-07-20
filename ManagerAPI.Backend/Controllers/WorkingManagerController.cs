using System;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KarcagS.Backend.Controllers
{
    [Route("api/working-manager")]
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
        public IActionResult CreateWorkingDay([FromBody] WorkingDayInitModel workingDay)
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
        public IActionResult UpdateWorkingDay(int workingDayId, [FromBody] WorkingDayModel workingDay)
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
        public IActionResult AddWorkingField(int workingDayId, [FromBody] WorkingFieldModel workingField)
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
        public IActionResult UpdateWorkingField(int workingFieldId, [FromBody] WorkingFieldModel workingField)
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

        [HttpGet("field/{id}")]
        public IActionResult GetWorkingField(int id) {
            try
            {
                return Ok(_workingManagerService.GetWorkingField(id));
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
