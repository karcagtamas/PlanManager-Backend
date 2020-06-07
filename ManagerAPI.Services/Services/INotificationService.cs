using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services
{
    public interface INotificationService
    {
        void AddSystemNotificationByType(SystemNotificationType type, User user);
        List<NotificationDto> GetMyNotifications();
        int GetCountOfUnReadNotifications();
        void SetAsReadNotificationsById(int[] notifications);
    }
}