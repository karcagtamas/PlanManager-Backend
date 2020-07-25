using System;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers {
    /// <summary>
    /// Notification Controller
    /// </summary>
    [Route ("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase 
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly INotificationService _notificationService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="loggerService">Utils Service</param>
        public NotificationController (INotificationService notificationService, ILoggerService loggerService) {
            _notificationService = notificationService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get My Notifications
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet]
        public IActionResult GetMyNotifications () {
            try {
                return Ok (_notificationService.GetMyNotifications ());
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }

        /// <summary>
        /// Get count of unread notifications
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet ("unreads/count")]
        public IActionResult GetCountOfUnReadNotifications () {
            try {
                return Ok (_notificationService.GetCountOfUnReadNotifications ());
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }

        /// <summary>
        /// Set notifications as read
        /// </summary>
        /// <param name="notifications">Ids of the notifications</param>
        /// <returns>Server Response</returns>
        [HttpPut]
        public IActionResult SetAsReadNotificationsById ([FromBody] int[] notifications) {
            try {
                _notificationService.SetAsReadNotificationsById (notifications);
                return Ok ();
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }
    }
}