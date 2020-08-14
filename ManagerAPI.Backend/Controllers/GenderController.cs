using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenderController : MyController<Gender, GenderModel, GenderListDto, GenderDto, SystemNotificationType>
    {
        private readonly IGenderService _genderService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="genderService">Message Service</param>
        /// <param name="loggerService">Utils Service</param>
        public GenderController(IGenderService genderService, ILoggerService loggerService) : base (loggerService, genderService)
        {
            _genderService = genderService;
        }
    }
}