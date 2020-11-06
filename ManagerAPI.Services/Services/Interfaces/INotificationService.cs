using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Domain.Enums.SL;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Shared.DTOs;
using System;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface INotificationService
    {
        void AddNotification(User user, int type, string val, params string[] args);
        void AddNotificationByType(Type type, object typeVal, User user, params string[] args);
        void AddSystemNotificationByType(SystemNotificationType type, User user, params string[] args);
        void AddWorkingManagerNotificationByType(WorkingManagerNotificationType type, User user, params string[] args);
        void AddStatusLibraryNotificationByType(StatusLibraryNotificationType type, User user, params string[] args);
        List<NotificationDto> GetMyNotifications();
        int GetCountOfUnReadNotifications();
        void SetAsReadNotificationsById(int[] notifications);
    }
}