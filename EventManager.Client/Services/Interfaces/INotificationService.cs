using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs;

namespace EventManager.Client.Services.Interfaces {
    public interface INotificationService {
        Task<List<NotificationDto>> GetMyNotifications ();
        Task<bool> SetUnReadsToRead (int[] ids);
        Task<int?> GetCountOfUnReadNotifications ();
    }
}