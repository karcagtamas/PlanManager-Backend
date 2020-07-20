using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.Models;
using System;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogError(Exception e);
        void LogInformation(User user, string service, string action, int id);
        void LogInformation(User user, string service, string action, int id, object entity);
        void LogInformation(User user, string service, string action, string id);
        void LogInformation(User user, string service, string action, string id, object entity);
        void LogInformation(User user, string service, string action, List<string> ids);
        void LogInformation(User user, string service, string action, List<string> ids, object entity);
        void LogInformation(User user, string service, string action, List<int> ids);
        void LogInformation(User user, string service, string action, List<int> ids, object entity);
        MessageException LogInvalidThings(User user, string service, string thing, string message);
        string AddUserToMessage(string message, User user);
        ErrorResponse ExceptionToResponse (Exception e);
    }
}
