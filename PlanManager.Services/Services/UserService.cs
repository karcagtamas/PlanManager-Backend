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
    public class UserService : IUserService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly UserMessages _userMessages;

        public UserService(ILogger<AuthService> logger, DatabaseContext context, IMapper mapper, IUtilsService utilsService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _userMessages = new UserMessages();
        }

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
    }
}