using System;
using System.Linq;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.Entities;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ManagerAPI.Services.Services {
    /// <summary>
    /// Utils Service
    /// </summary>
    public class UtilsService : IUtilsService {
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
        public UtilsService (ILogger<UtilsService> logger, IHttpContextAccessor contextAccessor, UserManager<User> userManager, DatabaseContext context) {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Get current user from the HTTP Context
        /// </summary>
        /// <returns>Current user</returns>
        public User GetCurrentUser () {
            var userId = GetCurrentUserId ();
            var user = _context.AppUsers.Find (userId);
            if (user == null) {
                throw new Exception ("Invalid user Id");
            }
            return user;
        }

        /// <summary>
        /// Get current user's Id from the HTTP Context
        /// </summary>
        /// <returns>Current user's Id</returns>
        public string GetCurrentUserId () {
            var userId = _contextAccessor.HttpContext.User.Claims.First (c => c.Type == "UserId").Value;
            return userId;
        }

        public string UserDisplay(User user)
        {
            return $"{user.UserName} ({user.Id})";
        }
    }
}