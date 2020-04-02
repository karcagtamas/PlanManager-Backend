using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;
using PlanManager.Services.Utils;

namespace PlanManager.Services.Services
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
        
        /// <summary>
        /// Auth Service constructor
        /// </summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="appSettings">App Settings</param>
        /// <param name="roleManager">Role Manager</param>
        /// <param name="logger">Logger</param>
        /// <param name="context">Database Context</param>
        public AuthService(UserManager<User> userManager, IOptions<ApplicationSettings> appSettings, RoleManager<WebsiteRole> roleManager, ILogger<AuthService> logger, DatabaseContext context)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }
        
        /// <summary>
        /// Registration by registration model
        /// </summary>
        /// <param name="model">Model for the registration with main data</param>
        /// <returns>Result of the registration</returns>
        public async Task<IdentityResult> Registration(RegistrationModel model)
        {
            User appUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);
            User user = await _userManager.FindByNameAsync(appUser.UserName);
            await _userManager.AddToRoleAsync(user, Roles.NORMAL);
            _logger.LogInformation($"{user.UserName}'s registration was successfully with e-mail {user.Email}");
            return result;
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
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                Claim[] claims = new Claim[1 + roles.Count];
                claims[0] = new Claim("UserId", user.Id);
                for (int i = 1; i < claims.Length; i++)
                {
                    claims[i] = new Claim(ClaimTypes.Role, roles[i - 1]);
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
                return token;
            }
            else
            {
                throw new Exception($"Username or password is incorrect.");
            }
        }
    }
}