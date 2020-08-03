using System;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : MyController<Task, TaskModel, TaskListDto, TaskDto, SystemNotificationType>
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService, ILoggerService loggerService) : base(loggerService, taskService)
        {
            this._taskService = taskService;
        }

        [HttpGet("date")]
        public IActionResult GetDate([FromQuery] bool? isSolved)
        {
            try
            {
                return Ok(this._taskService.GetDate(isSolved));
            }
            catch (MessageException me) {
                return BadRequest (this.Logger.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (this.Logger.ExceptionToResponse (new Exception(FatalError), e));
            }
        }
    }
}
