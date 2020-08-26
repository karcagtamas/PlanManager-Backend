using System;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/working-day")]
    [ApiController]
    public class WorkingDayController : MyController<WorkingDay, WorkingDayModel, WorkingDayListDto, WorkingDayDto>
    {
        private readonly IWorkingDayService _workingDayService;

        public WorkingDayController(IWorkingDayService workingDayService) : base(workingDayService)
        {
            this._workingDayService = workingDayService;
        }

        [HttpGet("day/{day}")]
        public IActionResult Get(DateTime day)
        {
            return Ok(this._workingDayService.Get(day));
        }

        [HttpGet("{id}/stat")]
        public IActionResult Stat(int id)
        {
            return Ok(this._workingDayService.Stat(id));
        }
    }
}