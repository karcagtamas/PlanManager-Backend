using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;

namespace ManagerAPI.Services.Services
{
    public interface INotificationService
    {
        void AddSystemNotificationByType(SystemNotificationType type, User user);
    }
}