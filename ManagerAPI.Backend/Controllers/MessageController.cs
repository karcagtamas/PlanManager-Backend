using System;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    /// <summary>
    /// Message Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IMessageService _messageService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="messageService">Message Service</param>
        /// <param name="loggerService">Utils Service</param>
        public MessageController(IMessageService messageService, ILoggerService loggerService)
        {
            _messageService = messageService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get current user's messages
        /// </summary>
        /// <param name="friendId">Partner Id</param>
        /// <returns>Server Response</returns>
        [HttpGet("{friendId}")]
        public IActionResult GetMessages(int friendId)
        {
            try
            {
                return Ok(_messageService.GetMessages(friendId));
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        /// <summary>
        /// Send message to partner
        /// </summary>
        /// <param name="model">Model of message sending</param>
        /// <returns>Server Response</returns>
        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageModel model)
        {
            try
            {
                _messageService.SendMessage(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }
    }
}