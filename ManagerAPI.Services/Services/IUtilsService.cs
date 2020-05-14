using System;
using ManagerAPI.DataAccess.Entities;

namespace ManagerAPI.Services.Services
{
    public interface IUtilsService
    {
        void LogError(Exception e);
        User GetCurrentUser();
        string GetCurrentUserId();
        string AddUserToMessage(string message, User user);
        void LogInformation(string action, User user);
    }
}