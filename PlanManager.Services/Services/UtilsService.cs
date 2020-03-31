using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;

namespace PlanManager.Services.Services
{
    public class UtilsService : IUtilsService
    {
        private readonly ILogger<UtilsService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _context;

        public UtilsService(ILogger<UtilsService> logger, IHttpContextAccessor contextAccessor, UserManager<User> userManager, DatabaseContext context)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _context = context;
        }
        public ErrorResponse LogError(Exception e)
        {
            _logger.LogError(e.Message);
            return new ErrorResponse(e);
        }

        public User GetCurrentUser()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.First(c => c.Type == "UserId").Value;
            var user = _context.AppUsers.Find(userId);
            return user;
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }

        public string AddUserToMessage(string message, User user)
        {
            return $"Invalid action for user {user.UserName} ({user.Id}): {message}";
        }

        public void LogInformation(string action, User user)
        {
            _logger.LogInformation($"Successfully {action} by user {user.UserName} ({user.Id})");
        }
    }
}