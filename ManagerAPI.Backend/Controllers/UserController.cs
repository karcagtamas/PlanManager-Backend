using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUtilsService _utilsService;

        public UserController(IUserService userService, IUtilsService utilsService)
        {
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

        [HttpGet("shorter")]
        public IActionResult GetShortUser()
        {
            try
            {
                return Ok(new ServerResponse<UserShortDto>(_userService.GetShortUser(), true));
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

        [HttpGet("genders")]
        public IActionResult GetGenders()
        {
            try
            {
                return Ok(new ServerResponse<List<GenderDto>>(_userService.GetGenders(), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPut("profile-image")]
        public IActionResult UpdateProfileImage([FromBody] byte[] image)
        {
            try
            {
                _userService.UpdateProfileImage(image);
                return Ok(new ServerResponse<Object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            try
            {
                await _userService.UpdatePassword(model.OldPassword, model.NewPassword);
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPut("username")]
        public async Task<IActionResult> UpdateUsername([FromBody] UsernameUpdateModel model)
        {
            try
            {
                await _userService.UpdateUsername(model.UserName);
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }

        [HttpPut("disable")]
        public IActionResult DisableUser()
        {
            try
            {
                _userService.DisableUser();
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<Object>(e));
            }
        }
    }
}