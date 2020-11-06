using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly INotificationService _notificationService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// User Service constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="userManager">User manager</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="loggerService">Logger Service</param>
        public UserService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            UserManager<User> userManager,
            INotificationService notificationService, ILoggerService loggerService)
        {
            this._context = context;
            this._mapper = mapper;
            this._utilsService = utilsService;
            this._userManager = userManager;
            this._notificationService = notificationService;
            this._loggerService = loggerService;
        }

        /// <summary>
        /// Gets current user's data object
        /// </summary>
        /// <returns>User DTO</returns>
        /// <exception cref="Exception">Invalid user id</exception>
        public async Task<UserDto> GetUser()
        {
            var user = this._utilsService.GetCurrentUser();

            var userDto = this._mapper.Map<UserDto>(user);
            var list = (await this._userManager.GetRolesAsync(user)).ToList();
            userDto.Roles = this._context.AppRoles.OrderByDescending(x => x.AccessLevel).Where(x => list.Contains(x.Name))
                .Select(x => x.Name).ToList();

            this._loggerService.LogInformation(user, nameof(UserService), "get user", user.Id);
            return userDto;
        }

        /// <summary>
        /// Get small data object from user object
        /// </summary>
        /// <returns>Minimized user data object</returns>
        public UserShortDto GetShortUser()
        {
            var user = this._utilsService.GetCurrentUser();

            var userDto = this._mapper.Map<UserShortDto>(user);
            this._loggerService.LogInformation(user, nameof(UserService), "get short user", user.Id);
            return userDto;
        }

        /// <summary>
        /// Update current user's data object by the given update object
        /// </summary>
        /// <param name="model">Update object</param>
        /// <exception cref="Exception">Invalid user update object</exception>
        public void UpdateUser(UserModel model)
        {
            var user = this._utilsService.GetCurrentUser();
            if (model == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(UserService), UserUpdateObjectThing,
                    ErrorDuringUserUpdateMessage);
            }

            this._mapper.Map(model, user);

            this._context.AppUsers.Update(user);
            this._context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(UserService), UpdateUserAction, user.Id);
            this._notificationService.AddSystemNotificationByType(SystemNotificationType.MyProfileUpdated, user);
        }

        /// <summary>
        /// Update profile image
        /// </summary>
        /// <param name="image">New image</param>
        public void UpdateProfileImage(byte[] image)
        {
            var user = this._utilsService.GetCurrentUser();

            if (image == null || image.Length == 0)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(UserService), ImageThing, ImageCannotBeEmptyMessage);
            }

            user.ProfileImageData = image;
            user.ProfileImageTitle = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            this._context.AppUsers.Update(user);
            this._context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(UserService), UpdateImageAction, user.Id);
            this._notificationService.AddSystemNotificationByType(SystemNotificationType.ProfileImageChanged, user);
        }

        /// <summary>
        /// Update profile login password
        /// </summary>
        /// <param name="oldPassword">Old password for authentication</param>
        /// <param name="newPassword">Newly cheesed password</param>
        /// <returns>Void</returns>
        public async System.Threading.Tasks.Task UpdatePassword(string oldPassword, string newPassword)
        {
            var user = this._utilsService.GetCurrentUser();

            if (await this._userManager.CheckPasswordAsync(user, oldPassword))
            {
                if (newPassword == oldPassword)
                {
                    throw this._loggerService.LogInvalidThings(user, nameof(UserService), PasswordThing,
                        OldAndNewPasswordCannotBeSameMessage);
                }

                var result = await this._userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                if (!result.Succeeded)
                {
                    throw this._loggerService.LogInvalidThings(user, nameof(UserService), PasswordThing,
                        this._utilsService.ErrorsToString(result.Errors));
                }
            }
            else
            {
                throw this._loggerService.LogInvalidThings(user, nameof(UserService), PasswordThing,
                    IncorrectOldPasswordMessage);
            }

            this._loggerService.LogInformation(user, nameof(UserService), UpdatePasswordAction, user.Id);
            this._notificationService.AddSystemNotificationByType(SystemNotificationType.PasswordChanged, user);
        }

        /// <summary>
        /// Update profile username
        /// </summary>
        /// <param name="newUsername">New username</param>
        /// <returns>Void</returns>
        public async System.Threading.Tasks.Task UpdateUsername(string newUsername)
        {
            var user = this._utilsService.GetCurrentUser();
            if (newUsername != user.UserName)
            {
                var result = await this._userManager.SetUserNameAsync(user, newUsername);
                if (!result.Succeeded)
                {
                    throw this._loggerService.LogInvalidThings(user, nameof(UserService), UsernameThing,
                        this._utilsService.ErrorsToString(result.Errors));
                }
            }
            else
            {
                throw this._loggerService.LogInvalidThings(user, nameof(UserService), UsernameThing,
                    NewEqualOldUserNameMessage);
            }

            this._loggerService.LogInformation(user, nameof(UserService), UpdateUserNameAction, user.Id);
            this._notificationService.AddSystemNotificationByType(SystemNotificationType.UsernameChanged, user,
                user.UserName, newUsername);
        }

        /// <summary>
        /// Disable my user
        /// </summary>
        public void DisableUser()
        {
            var user = this._utilsService.GetCurrentUser();
            user.IsActive = false;

            this._context.AppUsers.Update(user);
            this._context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(UserService), DisableUserAction, user.Id);
            this._notificationService.AddSystemNotificationByType(SystemNotificationType.ProfileDisabled, user);
        }
    }
}