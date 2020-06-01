using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Services.Messages;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Service for managing notifications
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IUtilsService _utilsService;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly NotificationMessages _notificationMessages;

        /// <summary>
        /// Injector consturctor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="context">Database context</param>
        public NotificationService(IUtilsService utilsService, DatabaseContext context, IMapper mapper)
        {
            _utilsService = utilsService;
            _context = context;
            _mapper = mapper;
            _notificationMessages = new NotificationMessages();
        }
        
        /// <summary>
        /// Add System notification by given type
        /// </summary>
        /// <param name="type">Type of notification</param>
        /// <param name="user">Destination user</param>
        public void AddSystemNotificationByType(SystemNotificationType type, User user)
        {
            // Determine message by type
            string val = type switch
            {
                SystemNotificationType.Login => $"Successfully logged in. Welcome.",
                SystemNotificationType.Logout => $"Successfully logged out.",
                SystemNotificationType.Registration => $"Successfully registered. Have fun.",
                SystemNotificationType.MessageArrived => $"You have a new unread message.",
                SystemNotificationType.MyProfileUpdated => $"You profile data successfully updated",
                SystemNotificationType.ToDoAdded => $"ToDo successfully added. Do not forget it.",
                SystemNotificationType.ToDoDeleted => $"ToDo successfully deleted.",
                SystemNotificationType.ToDoUpdated => $"ToDo successfully updated.",
                SystemNotificationType.PasswordChanged => $"Password changed",
                SystemNotificationType.ProfileImageChanged => $"Profile image changed",
                SystemNotificationType.UsernameChanged => $"Username changed",
                SystemNotificationType.ProfileDisabled => $"Profile disabled",
                _ => throw new Exception(_utilsService.AddUserToMessage(_notificationMessages.InvalidSystemNotificationType, user)),
            };
             
            // Create notification
            var notification = new Notification
            {
                Content = val,
                OwnerId = user.Id,
                TypeId = (int) type
            };

            // Save notification
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            _utilsService.LogInformation(_notificationMessages.NotificationAdded, user);
        }

        public List<NotificationDto> GetMyNotifications()
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<NotificationDto>>(_context.Notifications.Where(x => x.OwnerId == user.Id).OrderByDescending(x => x.SentDate).ToList());

            return list;
        }
    }
}