using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Auth Service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly RoleManager<WebsiteRole> _roleManager;
        private readonly ILogger<AuthService> _logger;
        private readonly DatabaseContext _context;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Auth Service constructor
        /// </summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="appSettings">App Settings</param>
        /// <param name="roleManager">Role Manager</param>
        /// <param name="logger">Logger</param>
        /// <param name="context">Database Context</param>
        /// <param name="notificationService">Notification Service</param>
        public AuthService(UserManager<User> userManager, IOptions<ApplicationSettings> appSettings, RoleManager<WebsiteRole> roleManager, ILogger<AuthService> logger, DatabaseContext context, INotificationService notificationService)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
            _notificationService = notificationService;
        }
        
        /// <summary>
        /// Registration by registration model
        /// </summary>
        /// <param name="model">Model for the registration with main data</param>
        /// <returns>Result of the registration</returns>
        public async System.Threading.Tasks.Task Registration(RegistrationModel model)
        {
            User appUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                User user = await _userManager.FindByNameAsync(appUser.UserName);
                await _userManager.AddToRoleAsync(user, Roles.NORMAL);
                _logger.LogInformation($"{user.UserName}'s registration was successfully with e-mail {user.Email}");
                _notificationService.AddSystemNotificationByType(SystemNotificationType.Registration, user);
            }
            else
            {
                throw new Exception(result.Errors.ToString());
            }
        }
        
        /// <summary>
        /// Login by the login model
        /// </summary>
        /// <param name="model">Model for the login with login credentials</param>
        /// <returns>Token</returns>
        /// <exception cref="Exception">Incorrect credentials</exception>
        public async Task<string> Login(LoginModel model)
        {
            User user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && user.IsActive && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                Claim[] claims = new Claim[3 + roles.Count];
                claims[0] = new Claim("UserId", user.Id);
                claims[1] = new Claim(ClaimTypes.Name, user.UserName);
                claims[2] = new Claim(ClaimTypes.Email, user.Email);
                for (int i = 3; i < claims.Length; i++)
                {
                    claims[i] = new Claim(ClaimTypes.Role, roles[i - 3]);
                }

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret)), SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(securityToken);
                user.LastLogin = DateTime.Now;
                _context.AppUsers.Update(user);
                _context.SaveChanges();
                _logger.LogInformation($"User {user.UserName} successfully logged in.");
                _notificationService.AddSystemNotificationByType(SystemNotificationType.Login, user);
                return token;
            }
            else
            {
                throw new Exception($"Username or password is incorrect.");
            }
        }

        public void Logout(string userId)
        {
            var user = _context.AppUsers.Find(userId);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.Logout, user);
        }
    }
}