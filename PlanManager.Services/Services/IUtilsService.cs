using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;

namespace PlanManager.Services.Services
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