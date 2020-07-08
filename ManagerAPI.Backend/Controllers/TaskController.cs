using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUtilsService _utilsService;

        public TaskController(ITaskService taskService, IUtilsService utilsService) 
        {
            this._taskService = taskService;
            this._utilsService = utilsService;
        }

        [HttpGet]
        public IActionResult GetTasks([FromQuery] bool isSolved)
        {
            try
            {
                return Ok(_taskService.GetTasks(isSolved));
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.ExceptionToResponse(e));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTasks(int id)
        {
            try
            {
                return Ok(_taskService.GetTask(id));
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.ExceptionToResponse(e));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.ExceptionToResponse(e));
            }
        }

        [HttpPut]
        public IActionResult UpdateTask([FromBody] TaskDataDto task)

        {
            try
            {
                _taskService.UpdateTask(task);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.ExceptionToResponse(e));
            }
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel model)

        {
            try
            {
                _taskService.CreateTask(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.ExceptionToResponse(e));
            }
        }
    }
}
