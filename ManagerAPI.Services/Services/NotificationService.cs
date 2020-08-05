using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Domain.Enums.CM;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;

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
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="context">Database context</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="mapper">Mapper</param>
        public NotificationService(IUtilsService utilsService, DatabaseContext context, IMapper mapper,
            ILoggerService loggerService)
        {
            _utilsService = utilsService;
            _context = context;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public void AddNotification(User user, int type, string val, params string[] args)
        {
            // Create notification
            var notification = new Notification
            {
                Content = this._utilsService.InjectString(val, args),
                OwnerId = user.Id,
                TypeId = (int) type
            };

            // Save notification
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            _loggerService.LogInformation(user, nameof(NotificationService), "add notification", notification.Id);
        }

        /// <summary>
        /// Add System notification by given type
        /// </summary>
        /// <param name="type">Type of notification</param>
        /// <param name="user">Destination user</param>
        /// <param name="args">Inject arguments</param>
        public void AddSystemNotificationByType(SystemNotificationType type, User user, params string[] args)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            // Determine message by type
            string val = type
                switch
                {
                    SystemNotificationType.Login => "Successfully logged in. Welcome.",
                    SystemNotificationType.Logout => "Successfully logged out.",
                    SystemNotificationType.Registration => "Successfully registered. Have fun.",
                    SystemNotificationType.MessageArrived => "You have a new unread message from {0}.",
                    SystemNotificationType.MyProfileUpdated => "You profile data successfully updated",
                    SystemNotificationType.AddTask => "{0} task successfully added. Do not forget it.",
                    SystemNotificationType.DeleteTask => "{0} task successfully deleted.",
                    SystemNotificationType.UpdateTask => "{0} task successfully updated.",
                    SystemNotificationType.PasswordChanged => "Password changed",
                    SystemNotificationType.ProfileImageChanged => "Profile image changed",
                    SystemNotificationType.UsernameChanged => "Username changed from {0} to {1}",
                    SystemNotificationType.ProfileDisabled => "Profile disabled",
                    SystemNotificationType.FriendRequestReceived => "Friend request received from {0}",
                    SystemNotificationType.FriendRequestSent => "Friend request sent to {0}",
                    SystemNotificationType.FriendRequestAccepted => "Your friend request accepted by {0}",
                    SystemNotificationType.FriendRequestDeclined => "Your friend request declined by {0}",
                    SystemNotificationType.YouHasANewFriend => "{0} is your friend now",
                    SystemNotificationType.FriendRemoved => "{0} removed from your friend list",
                    SystemNotificationType.AddNews => "News added by {0}",
                    SystemNotificationType.UpdateNews => "News updated by {0}",
                    SystemNotificationType.DeleteNews => "News deleted by {0}",
                    SystemNotificationType.AddMessage => "Message added",
                    SystemNotificationType.DeleteMessage => "Message deleted",
                    SystemNotificationType.UpdateMessage => "Message updated",
                    SystemNotificationType.AddGender => "Gender added with title {0}",
                    SystemNotificationType.DeleteGender => "Gender deleted with title {0}",
                    SystemNotificationType.UpdateGender => "Gender updated with title {0}",
                    _ =>
                    throw new Exception("System Notification is not implemented"),
                };

            this.AddNotification(user, (int) type, val, args);
        }

        public void AddWorkingManagerNotificationByType(WorkingManagerNotificationType type, User user,
            params string[] args)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            // Determine message by type
            string val = type
                switch
                {
                    WorkingManagerNotificationType.AddWorkingField => "Working field created with length: {0}",
                    WorkingManagerNotificationType.DeleteWorkingField => "Working field deleted on {0}",
                    WorkingManagerNotificationType.UpdateWorkingField => "Working field updated on {0}",
                    WorkingManagerNotificationType.AddWorkingDay => "Working day initialized on {0}",
                    WorkingManagerNotificationType.DeleteWorkingDay => "Working day deleted on {0}",
                    WorkingManagerNotificationType.UpdateWorkingDay => "Working day updated on {0}",
                    WorkingManagerNotificationType.AddWorkingDayType => "Working day type created with title: {0}",
                    WorkingManagerNotificationType.DeleteWorkingDayType => "Working day type deleted with title: {0}",
                    WorkingManagerNotificationType.UpdateWorkingDayType => "Working day type updated with title: {0}",
                    _ =>
                    throw new Exception("Working Manager Notification is not implemented"),
                };

            this.AddNotification(user, (int) type, val, args);
        }

        public void AddMovieCornerNotificationByType(MovieCornerNotificationType type, User user, params string[] args)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            // Determine message by type
            string val = type
                switch
                {
                    MovieCornerNotificationType.AddWorkingDay => "Successfully logged in. Welcome.",
                    _ =>
                    throw new Exception("System Notification is not implemented"),
                };

            this.AddNotification(user, (int) type, val, args);
        }

        /// <summary>
        /// Get count of unread notifications
        /// </summary>
        /// <returns>Count</returns>
        public int GetCountOfUnReadNotifications()
        {
            var user = _utilsService.GetCurrentUser();

            var count = user.Notifications.Count(x => !x.IsRead);

            _loggerService.LogInformation(user, nameof(NotificationService), "get count of unread notifications", 0);

            return count;
        }

        /// <summary>
        /// Get current logged in user's notifications
        /// </summary>
        /// <returns>List of notifications</returns>
        public List<NotificationDto> GetMyNotifications()
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<NotificationDto>>(_context.Notifications.Where(x => x.OwnerId == user.Id)
                .OrderByDescending(x => x.SentDate).ToList());

            _loggerService.LogInformation(user, nameof(NotificationService), "get notifications",
                list.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Set notifications as read
        /// </summary>
        /// <param name="notifications">Ids of the notifications</param>
        public void SetAsReadNotificationsById(int[] notifications)
        {
            var user = _utilsService.GetCurrentUser();
            var list = _context.Notifications.Where(x => notifications.ToList().Contains(x.Id)).ToList();

            foreach (var i in list)
            {
                i.IsRead = true;
            }

            _context.Notifications.UpdateRange(list);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(NotificationService), "set notifications as read",
                list.Select(x => x.Id).ToList());
        }

        void INotificationService.AddNotificationByType(Type type, object typeVal, User user, params string[] args)
        {
            if (type == typeof(SystemNotificationType))
            {
                this.AddSystemNotificationByType((SystemNotificationType) typeVal, user, args);
            }
            else if (type == typeof(WorkingManagerNotificationType))
            {
                this.AddWorkingManagerNotificationByType((WorkingManagerNotificationType) typeVal, user, args);
            }
            else if (type == typeof(MovieCornerNotificationType))
            {
                this.AddMovieCornerNotificationByType((MovieCornerNotificationType) typeVal, user, args);
            }
        }
    }
}