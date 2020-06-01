using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    /// <summary>
    /// Notification Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUtilsService _utilsService;

        /// <summary>
        /// Notification Controller
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
        /// <returns>List of notifications</returns>
        [HttpGet]
        public IActionResult GetMyNotifications()
        {
            try
            {
                return Ok(new ServerResponse<List<NotificationDto>>(_notificationService.GetMyNotifications(), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
            }
        }
    }
}
