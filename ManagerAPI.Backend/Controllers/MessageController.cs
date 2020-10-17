using ManagerAPI.Domain.Entities;
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
    public class MessageController : MyController<Message, MessageModel, MessageListDto, MessageDto>
    {
        private readonly IMessageService _messageService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="messageService">Message Service</param>
        public MessageController(IMessageService messageService) : base(messageService)
        {
            this._messageService = messageService;
        }

        /// <summary>
        /// Get current user's messages
        /// </summary>
        /// <param name="friendId">Partner Id</param>
        /// <returns>Server Response</returns>
        [HttpGet("friend/{friendId}")]
        public IActionResult GetMessages(string friendId)
        {
            return this.Ok(this._messageService.GetMessages(friendId));
        }

        /// <summary>
        /// Send message to partner
        /// </summary>
        /// <param name="model">Model of message sending</param>
        /// <returns>Server Response</returns>
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] MessageModel model)
        {
            this._messageService.SendMessage(model);
            return this.Ok();
        }
    }
}