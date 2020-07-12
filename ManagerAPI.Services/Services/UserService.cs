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
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        // Action
        private const string DisableUserAction = "disable user";
        private const string UpdateUserNameAction = "update username";
        private const string UpdatePasswordAction = "update password";
        private const string UpdateImageAction = "update image";
        private const string GetGendersAction = "get genders";
        private const string UpdateUserAction = "update user";

        // Messages
        private const string NewEqualOldUserNameMessage = "New username is equal with the old username";
        private const string IncorrectOldPasswordMessage = "Incorrect old password";
        private const string OldAndNewPasswordCannotBeSameMessage = "Old and new password cannot be same";
        private const string ImageCannotBeEmptyMessage = "Image cannot be empty";
        private const string ErrorDuringUserUpdateMessage = "Error during the user update";

        // Things
        private const string UsernameThing = "username";
        private const string PasswordThing = "password";
        private const string ImageThing = "image";
        private const string UserUpdateObjectThing = "update object";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<WebsiteRole> _roleManager;
        private readonly INotificationService _notificationService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// User Service constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="userManager">User manager</param>
        /// <param name="roleManager">Role manager</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="loggerService">Logger Service</param>
        public UserService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, UserManager<User> userManager, RoleManager<WebsiteRole> roleManager, INotificationService notificationService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _userManager = userManager;
            _roleManager = roleManager;
            _notificationService = notificationService;
            this._loggerService = loggerService;
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

            _loggerService.LogInformation(user, nameof(UserService), "get user", user.Id);
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
            _loggerService.LogInformation(user, nameof(UserService), "get short user", user.Id);
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
                throw _loggerService.LogInvalidThings(user, nameof(UserService), UserUpdateObjectThing, ErrorDuringUserUpdateMessage);
            }

            _mapper.Map(updateDto, user);

            _context.AppUsers.Update(user);
            _context.SaveChanges();
            _loggerService.LogInformation(user, nameof(UserService), UpdateUserAction, user.Id);
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
            _loggerService.LogInformation(user, nameof(UserService), GetGendersAction, genders.Select(x => x.Id).ToList());
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
                throw _loggerService.LogInvalidThings(user, nameof(UserService), ImageThing, ImageCannotBeEmptyMessage);
            }

            user.ProfileImageData = image;
            user.ProfileImageTitle = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            _context.AppUsers.Update(user);
            _context.SaveChanges();
            _loggerService.LogInformation(user, nameof(UserService), UpdateImageAction, user.Id);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.ProfileImageChanged, user);
        }

        /// <summary>
        /// Update profile login password
        /// </summary>
        /// <param name="oldPassword">Old password for authentication</param>
        /// <param name="newPassword">Newly choosed password</param>
        /// <returns>Void</returns>
        public async System.Threading.Tasks.Task UpdatePassword(string oldPassword, string newPassword)
        {
            var user = _utilsService.GetCurrentUser();

            if (await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                if (newPassword == oldPassword)
                {
                    throw _loggerService.LogInvalidThings(user, nameof(UserService), PasswordThing, OldAndNewPasswordCannotBeSameMessage);
                }

                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                if (!result.Succeeded)
                {
                    // TODO: fix message
                    throw _loggerService.LogInvalidThings(user, nameof(UserService), PasswordThing, result.Errors.ToString());
                }
            }
            else
            {
                throw _loggerService.LogInvalidThings(user, nameof(UserService), PasswordThing, IncorrectOldPasswordMessage);
            }
            _loggerService.LogInformation(user, nameof(UserService), UpdatePasswordAction, user.Id);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.PasswordChanged, user);
        }

        /// <summary>
        /// Update profile username
        /// </summary>
        /// <param name="newUsername">New username</param>
        /// <returns>Void</returns>
        public async System.Threading.Tasks.Task UpdateUsername(string newUsername)
        {
            var user = _utilsService.GetCurrentUser();
            if (newUsername != user.UserName)
            {
                var result = await _userManager.SetUserNameAsync(user, newUsername);
                if (!result.Succeeded)
                {
                    throw _loggerService.LogInvalidThings(user, nameof(UserService), UsernameThing, result.Errors.ToString());
                }
            }
            else
            {
                throw _loggerService.LogInvalidThings(user, nameof(UserService), UsernameThing, NewEqualOldUserNameMessage);
            }
            _loggerService.LogInformation(user, nameof(UserService), UpdateUserNameAction, user.Id);
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
            _loggerService.LogInformation(user, nameof(UserService), DisableUserAction, user.Id);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.ProfileDisabled, user);
        }
    }
}