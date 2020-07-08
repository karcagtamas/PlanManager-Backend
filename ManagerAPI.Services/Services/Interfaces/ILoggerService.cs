using ManagerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogError(Exception e);
        void LogInformation(string action, User user);
        string AddUserToMessage(string message, User user);
        string CreateMessage(string message, string args);
    }
}
