using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : MyController<Task, TaskModel, TaskListDto, TaskDto>
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService) : base(taskService)
        {
            this._taskService = taskService;
        }

        [HttpGet("date")]
        public IActionResult GetDate([FromQuery] bool? isSolved)
        {
            return Ok(this._taskService.GetDate(isSolved));
        }
    }
}