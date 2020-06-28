using System;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;

namespace ManagerAPI.Services.Services {
    public interface IUtilsService {
        void LogError (Exception e);
        User GetCurrentUser ();
        string GetCurrentUserId ();
        string AddUserToMessage (string message, User user);
        void LogInformation (string action, User user);
        ErrorResponse ExceptionToResponse (Exception e);
    }
}