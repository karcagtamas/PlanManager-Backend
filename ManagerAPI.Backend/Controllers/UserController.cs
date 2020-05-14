using System;
using System.Threading.Tasks;
using ManagerAPI.DataAccess.Models;
using ManagerAPI.Services.DTOs;
using ManagerAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers {
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
        public async Task<IActionResult> GetUser()
        {
            try
            {
                return Ok(new ServerResponse<UserDto>(await _userService.GetUser(), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdateDto updateDto)
        {
            try
            {
                _userService.UpdateUser(updateDto);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }
    }
}