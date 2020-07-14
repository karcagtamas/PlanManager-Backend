using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
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