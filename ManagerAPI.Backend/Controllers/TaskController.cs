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
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly ITaskService _taskService;
        private readonly ILoggerService _loggerService;

        public TaskController(ITaskService taskService, ILoggerService loggerService) 
        {
            this._taskService = taskService;
            this._loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult GetTasks([FromQuery] bool isSolved)
        {
            try
            {
                return Ok(_taskService.GetTasks(isSolved));
            }
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTasks(int id)
        {
            try
            {
                return Ok(_taskService.GetTask(id));
            }
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
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
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
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
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
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
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }
    }
}
