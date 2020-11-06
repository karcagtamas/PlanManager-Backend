using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Services.Utils;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Auth Service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly ILogger<AuthService> _logger;
        private readonly DatabaseContext _context;
        private readonly INotificationService _notificationService;
        private readonly IUtilsService _utilsService;

        /// <summary>
        /// Auth Service constructor
        /// </summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="appSettings">App Settings</param>
        /// <param name="logger">Logger</param>
        /// <param name="context">Database Context</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="utilsService">Utils Service</param>
        public AuthService(UserManager<User> userManager, IOptions<ApplicationSettings> appSettings,
            ILogger<AuthService> logger, DatabaseContext context,
            INotificationService notificationService, IUtilsService utilsService)
        {
            this._userManager = userManager;
            this._appSettings = appSettings.Value;
            this._logger = logger;
            this._context = context;
            this._notificationService = notificationService;
            this._utilsService = utilsService;
        }

        /// <summary>
        /// Registration by registration model
        /// </summary>
        /// <param name="model">Model for the registration with main data</param>
        /// <returns>Result of the registration</returns>
        public async System.Threading.Tasks.Task Registration(RegistrationModel model)
        {
            var appUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await this._userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                var user = await this._userManager.FindByNameAsync(appUser.UserName);
                await this._userManager.AddToRoleAsync(user, Roles.NormalWebsiteRole);
                this._logger.LogInformation($"{user.UserName}'s registration was successfully with e-mail {user.Email}");
                this._notificationService.AddSystemNotificationByType(SystemNotificationType.Registration, user);
            }
            else
            {
                throw new MessageException(this._utilsService.ErrorsToString(result.Errors));
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
            var user = await this._userManager.FindByNameAsync(model.UserName);
            if (user != null && user.IsActive && await this._userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await this._userManager.GetRolesAsync(user);
                var claims = new Claim[3 + roles.Count];
                claims[0] = new Claim("UserId", user.Id);
                claims[1] = new Claim(ClaimTypes.Name, user.UserName);
                claims[2] = new Claim(ClaimTypes.Email, user.Email);
                for (int i = 3; i < claims.Length; i++)
                {
                    claims[i] = new Claim(ClaimTypes.Role, roles[i - 3]);
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._appSettings.JwtSecret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(securityToken);
                user.LastLogin = DateTime.Now;
                this._context.AppUsers.Update(user);
                await this._context.SaveChangesAsync();
                this._logger.LogInformation($"User {user.UserName} successfully logged in.");
                this._notificationService.AddSystemNotificationByType(SystemNotificationType.Login, user);
                return token;
            }

            throw new MessageException("Username or password is incorrect.");
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="userId">User Id</param>
        public void Logout(string userId)
        {
            var user = this._context.AppUsers.Find(userId);
            this._notificationService.AddSystemNotificationByType(SystemNotificationType.Logout, user);
        }
    }
}