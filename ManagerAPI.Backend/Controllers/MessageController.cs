using System;
using System.Collections.Generic;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services;
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
        private readonly IMessageService _messageService;
        private readonly IUtilsService _utilsService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="messageService">Message Service</param>
        /// <param name="utilsService">Utils Service</param>
        public MessageController(IMessageService messageService, IUtilsService utilsService)
        {
            _messageService = messageService;
            _utilsService = utilsService;
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
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return BadRequest(e);
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
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return BadRequest(e);
            }
        }
    }
}
