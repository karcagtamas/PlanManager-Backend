using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Services.Messages;
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
        private readonly UserMessages _userMessages;

        /// <summary>
        /// User Service constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="userManager"></param>
        public UserService(ILogger<AuthService> logger, DatabaseContext context, IMapper mapper, IUtilsService utilsService, UserManager<User> userManager, RoleManager<WebsiteRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _userManager = userManager;
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
            userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            _utilsService.LogInformation(_userMessages.UserGet, user);
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

            // user.FullName = updateDto.FullName;
            _context.AppUsers.Update(user);
            _context.SaveChanges();
            _utilsService.LogInformation(_userMessages.UserUpdate, user);
        }

        public List<GenderDto> GetGenders()
        {
            var user = _utilsService.GetCurrentUser();
            var genders = _mapper.Map<List<GenderDto>>(_context.Genders.ToList());
            _utilsService.LogInformation(_userMessages.GendersGet, user);
            return genders;
        }
    }
}