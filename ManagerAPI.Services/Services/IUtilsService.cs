using System;
using ManagerAPI.Models.Entities;

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