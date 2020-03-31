using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;
using PlanManager.Services.DTOs;
using PlanManager.Services.Services;

namespace PlanManager.Backend.Controllers {
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly UserManager<User> _userManager;

        public UserController (IUserService userService, ILogger<UserController> logger, DatabaseContext context, IUtilsService utilsService, UserManager<User> userManager) {
            _userService = userService;
            _logger = logger;
            _context = context;
            _utilsService = utilsService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                return Ok(_userService.GetUser());
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.LogError(e));
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdateDto updateDto)
        {
            try
            {
                _userService.UpdateUser(updateDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(_utilsService.LogError(e));
            }
        }
    }
}