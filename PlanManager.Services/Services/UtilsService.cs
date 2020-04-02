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
    /// <summary>
    /// Utils Service
    /// </summary>
    public class UtilsService : IUtilsService
    {
        private readonly ILogger<UtilsService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _context;

        /// <summary>
        /// Utils Service constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="contextAccessor">Context Accessor</param>
        /// <param name="userManager">User Manager</param>
        /// <param name="context">Context</param>
        public UtilsService(ILogger<UtilsService> logger, IHttpContextAccessor contextAccessor, UserManager<User> userManager, DatabaseContext context)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _context = context;
        }
        
        /// <summary>
        /// Log error to the console
        /// </summary>
        /// <param name="e">Exception for logging</param>
        /// <returns>Error Response from Exception</returns>
        public ErrorResponse LogError(Exception e)
        {
            _logger.LogError(e.Message);
            return new ErrorResponse(e);
        }
        
        /// <summary>
        /// Get current user from the HTTP Context
        /// </summary>
        /// <returns>Current user</returns>
        public User GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            var user = _context.AppUsers.Find(userId);
            return user;
        }
        
        /// <summary>
        /// Get current user's Id from the HTTP Context
        /// </summary>
        /// <returns>Current user's Id</returns>
        public string GetCurrentUserId()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.First(c => c.Type == "UserId").Value;
            return userId;
        }
        
        /// <summary>
        /// Join user to the given message
        /// </summary>
        /// <param name="message">Source message</param>
        /// <param name="user">User for join</param>
        /// <returns>Joined message</returns>
        public string AddUserToMessage(string message, User user)
        {
            return $"Invalid action for user {user.UserName} ({user.Id}): {message}";
        }
        
        /// <summary>
        /// Log general action with given user
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="user">User</param>
        public void LogInformation(string action, User user)
        {
            _logger.LogInformation($"Successfully {action} by user {user.UserName} ({user.Id})");
        }
    }
}