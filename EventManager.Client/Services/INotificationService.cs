using EventManager.Client.Models;
using EventManager.Client.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetMyNotifications();
        Task<bool> SetUnReadsToRead(int[] ids);
        Task<int?> GetCountOfUnReadNotifications();
    }
}
