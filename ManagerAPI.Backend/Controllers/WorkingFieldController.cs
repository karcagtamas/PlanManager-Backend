using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/working-field")]
    [ApiController]
    public class
        WorkingFieldController : MyController<WorkingField, WorkingFieldModel, WorkingFieldListDto, WorkingFieldDto>
    {
        private readonly IWorkingFieldService _workingFieldService;

        public WorkingFieldController(IWorkingFieldService workingFieldService) : base(workingFieldService)
        {
            this._workingFieldService = workingFieldService;
        }

        [HttpGet("week-stat/{week}")]
        public IActionResult GetWeekStat(DateTime week)
        {
            return this.Ok(this._workingFieldService.GetWeekStat(week));
        }

        [HttpGet("month-stat/{year}/{month}")]
        public IActionResult GetMonthStat(int year, int month)
        {
            return this.Ok(this._workingFieldService.GetMonthStat(year, month));
        }
    }
}