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
        Task<ApiResponseModel<List<NotificationDto>>> GetMyNotifications();
        Task<ApiResponseModel<object>> SetUnReadsToRead(int[] ids);
        Task<ApiResponseModel<int>> GetCountOfUnReadNotifications();
    }
}
