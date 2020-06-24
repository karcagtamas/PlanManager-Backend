using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    /// <summary>
    /// Notification Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUtilsService _utilsService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="utilsService">Utils Service</param>
        public NotificationController(INotificationService notificationService, IUtilsService utilsService)
        {
            _notificationService = notificationService;
            _utilsService = utilsService;
        }


        /// <summary>
        /// Get My Notifications
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet]
        public IActionResult GetMyNotifications()
        {
            try
            {
                return Ok(_notificationService.GetMyNotifications());
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Get count of unread notifications
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet("unreads/count")]
        public IActionResult GetCountOfUnReadNotifications()
        {
            try
            {
                return Ok(_notificationService.GetCountOfUnReadNotifications());
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Set notifications as read
        /// </summary>
        /// <param name="notifications">Ids of the notifications</param>
        /// <returns>Server Response</returns>
        [HttpPut]
        public IActionResult SetAsReadNotificationsById([FromBody] int[] notifications)
        {
            try
            {
                _notificationService.SetAsReadNotificationsById(notifications);
                return Ok();
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(e);
            }
        }
    }
}
