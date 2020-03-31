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
        private readonly IUtilsService _utilsService;

        public UserController (IUserService userService, IUtilsService utilsService) {
            _userService = userService;
            _utilsService = utilsService;
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