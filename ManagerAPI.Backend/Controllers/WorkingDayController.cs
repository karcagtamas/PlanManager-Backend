using System;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/working-day")]
    [ApiController]
    public class WorkingDayController : MyController<WorkingDay, WorkingDayModel, WorkingDayListDto, WorkingDayDto>
    {
        private readonly IWorkingDayService _workingDayService;
        public WorkingDayController(IWorkingDayService workingDayService, ILoggerService loggerService) : base(loggerService, workingDayService)
        {
            this._workingDayService = workingDayService;
        }

        [HttpGet("day/{day}")]
        public IActionResult Get(DateTime day)
        {
            try
            {
                return Ok(this._workingDayService.Get(day));
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpGet("{id}/stat")]
        public IActionResult Stat(int id)
        {
            try
            {
                return Ok(this._workingDayService.Stat(id));
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }
    }
}
