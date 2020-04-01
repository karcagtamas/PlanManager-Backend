using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;
using PlanManager.Services.DTOs;
using PlanManager.Services.Messages;
using PlanManager.Services.Utils;

namespace PlanManager.Services.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly UserMessages _userMessages;

        /// <summary>
        /// User Service constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        public UserService(ILogger<AuthService> logger, DatabaseContext context, IMapper mapper, IUtilsService utilsService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _userMessages = new UserMessages();
        }
        
        /// <summary>
        /// Gets current user's data object
        /// </summary>
        /// <returns>User DTO</returns>
        /// <exception cref="Exception">Invalid user id</exception>
        public UserDto GetUser()
        {
            var user = _utilsService.GetCurrentUser();
            if (user == null)
            {
                throw new Exception(_userMessages.InvalidUserId);   
            }

            var userDto = _mapper.Map<UserDto>(user);
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
    }
}