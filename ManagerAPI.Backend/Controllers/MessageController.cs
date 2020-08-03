using System;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
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
    public class MessageController : MyController<Message, MessageListDto, MessageDto, MessageModel, SystemNotificationType>
    {
        private readonly IMessageService _messageService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="messageService">Message Service</param>
        /// <param name="loggerService">Utils Service</param>
        public MessageController(IMessageService messageService, ILoggerService loggerService) : base (loggerService, messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Get current user's messages
        /// </summary>
        /// <param name="friendId">Partner Id</param>
        /// <returns>Server Response</returns>
        [HttpGet("friend/{friendId}")]
        public IActionResult GetMessages(int friendId)
        {
            try
            {
                return Ok(this._messageService.GetMessages(friendId));
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

        /// <summary>
        /// Send message to partner
        /// </summary>
        /// <param name="model">Model of message sending</param>
        /// <returns>Server Response</returns>
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] MessageModel model)
        {
            try
            {
                this._messageService.SendMessage(model);
                return Ok();
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