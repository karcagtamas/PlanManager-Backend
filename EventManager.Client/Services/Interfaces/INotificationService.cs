using ManagerAPI.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetMyNotifications();
        Task<bool> SetUnReadsToRead(int[] ids);
        Task<int?> GetCountOfUnReadNotifications();
    }
}