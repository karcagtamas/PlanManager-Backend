using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="notificationService">Notification Service</param>
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get My Notifications
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet]
        public IActionResult GetMyNotifications()
        {
            return Ok(_notificationService.GetMyNotifications());
        }

        /// <summary>
        /// Get count of unread notifications
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet("unreads/count")]
        public IActionResult GetCountOfUnReadNotifications()
        {
            return Ok(_notificationService.GetCountOfUnReadNotifications());
        }

        /// <summary>
        /// Set notifications as read
        /// </summary>
        /// <param name="notifications">Ids of the notifications</param>
        /// <returns>Server Response</returns>
        [HttpPut]
        public IActionResult SetAsReadNotificationsById([FromBody] int[] notifications)
        {
            _notificationService.SetAsReadNotificationsById(notifications);
            return Ok();
        }
    }
}