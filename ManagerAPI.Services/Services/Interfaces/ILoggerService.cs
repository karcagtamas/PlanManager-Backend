using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogError(Exception e);
        void LogInformation(User user, string service, string action, int id);
        void LogInformation(User user, string service, string action, int id, object entity);
        void LogInformation(User user, string service, string action, string id);
        void LogInformation(User user, string service, string action, string id, object entity);
        MessageException LogInvalidThings(User user, string service, string thing, string message);
        string AddUserToMessage(string message, User user);
        ErrorResponse ExceptionToResponse (Exception e);
    }
}
