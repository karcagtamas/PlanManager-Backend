using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Shared.DTOs;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface INotificationService
    {
        void AddSystemNotificationByType(SystemNotificationType type, User user, params string[] args);
        List<NotificationDto> GetMyNotifications();
        int GetCountOfUnReadNotifications();
        void SetAsReadNotificationsById(int[] notifications);
    }
}