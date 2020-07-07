using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Services.Messages;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<WebsiteRole> _roleManager;
        private readonly INotificationService _notificationService;
        private readonly UserMessages _userMessages;

        /// <summary>
        /// User Service constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="userManager">User manager</param>
        /// <param name="roleManager">Role manager</param>
        /// <param name="notificationService">Notification Service</param>
        public UserService(ILogger<AuthService> logger, DatabaseContext context, IMapper mapper, IUtilsService utilsService, UserManager<User> userManager, RoleManager<WebsiteRole> roleManager, INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _userManager = userManager;
            _roleManager = roleManager;
            _notificationService = notificationService;
            _userMessages = new UserMessages();
        }
        
        /// <summary>
        /// Gets current user's data object
        /// </summary>
        /// <returns>User DTO</returns>
        /// <exception cref="Exception">Invalid user id</exception>
        public async Task<UserDto> GetUser()
        {
            var user = _utilsService.GetCurrentUser();

            var userDto = _mapper.Map<UserDto>(user);
            var list = (await _userManager.GetRolesAsync(user)).ToList();
            userDto.Roles = _context.AppRoles.OrderByDescending(x => x.AccessLevel).Where(x => list.Contains(x.Name)).Select(x => x.Name).ToList();

            _utilsService.LogInformation(_userMessages.UserGet, user);
            return userDto;
        }

        /// <summary>
        /// Get small data object from user object
        /// </summary>
        /// <returns>Minimalized user data object</returns>
        public UserShortDto GetShortUser()
        {
            var user = _utilsService.GetCurrentUser();

            var userDto = _mapper.Map<UserShortDto>(user);
            _utilsService.LogInformation(_userMessages.UserShortGet, user);
            return userDto;
        }

        /// <summary>
        /// Update current user's data object by the given update object
        /// </summary>
        /// <param name="updateDto">Update object</param>
        /// <exception cref="Exception">Invalid user update object</exception>
        public void UpdateUser(UserUpdateDto updateDto)
        {
            var user = _utilsService.GetCurrentUser();
            if (updateDto == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_userMessages.InvalidUserUpdate, user));
            }

            _mapper.Map(updateDto, user);

            _context.AppUsers.Update(user);
            _context.SaveChanges();
            _utilsService.LogInformation(_userMessages.UserUpdate, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.MyProfileUpdated, user);
        }

        /// <summary>
        /// Get genders
        /// </summary>
        /// <returns>List of genders</returns>
        public List<GenderDto> GetGenders()
        {
            var user = _utilsService.GetCurrentUser();
            var genders = _mapper.Map<List<GenderDto>>(_context.Genders.ToList());
            _utilsService.LogInformation(_userMessages.GendersGet, user);
            return genders;
        }

        /// <summary>
        /// Update profile image
        /// </summary>
        /// <param name="image">New image</param>
        public void UpdateProfileImage(byte[] image)
        {
            var user = _utilsService.GetCurrentUser();

            if (image == null || image.Length == 0)
            {
                throw new Exception(_utilsService.AddUserToMessage(_userMessages.InvalidImage, user));
            }

            user.ProfileImageData = image;
            user.ProfileImageTitle = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            _context.AppUsers.Update(user);
            _context.SaveChanges();
            _utilsService.LogInformation(_userMessages.ProfileImageUpdate, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.ProfileImageChanged, user);
        }

        /// <summary>
        /// Update profile login password
        /// </summary>
        /// <param name="oldPassword">Old password for authentication</param>
        /// <param name="newPassword">Newly choosed password</param>
        /// <returns>Void</returns>
        public async Task UpdatePassword(string oldPassword, string newPassword)
        {
            var user = _utilsService.GetCurrentUser();

            if (await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                if (newPassword == oldPassword)
                {
                    throw new Exception(_utilsService.AddUserToMessage(_userMessages.OldAndNewPasswordCannotBeSame, user));
                }

                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(_utilsService.AddUserToMessage(result.Errors.ToString(), user));
                }
            }
            else
            {
                throw new Exception(_utilsService.AddUserToMessage(_userMessages.InvalidOldPassword, user));
            }
            _utilsService.LogInformation(_userMessages.PasswordUpdate, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.PasswordChanged, user);
        }

        /// <summary>
        /// Update profile username
        /// </summary>
        /// <param name="newUsername">New username</param>
        /// <returns>Void</returns>
        public async Task UpdateUsername(string newUsername)
        {
            var user = _utilsService.GetCurrentUser();
            if (newUsername != user.UserName)
            {
                var result = await _userManager.SetUserNameAsync(user, newUsername);
                if (!result.Succeeded)
                {
                    throw new Exception(_utilsService.AddUserToMessage(result.Errors.ToString(), user));
                }
            }
            else
            {
                throw new Exception(_utilsService.AddUserToMessage(_userMessages.AlreadyOwnThisUsername, user));
            }
            _utilsService.LogInformation(_userMessages.UsernameUpdate, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.UsernameChanged, user);
        }

        /// <summary>
        /// Disable my user
        /// </summary>
        public void DisableUser()
        {
            var user = _utilsService.GetCurrentUser();
            user.IsActive = false;

            _context.AppUsers.Update(user);
            _context.SaveChanges();
            _utilsService.LogInformation(_userMessages.DisableStatus, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.ProfileDisabled, user);
        }
    }
}