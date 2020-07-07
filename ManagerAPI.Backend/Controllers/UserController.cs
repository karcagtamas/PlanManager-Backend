using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> GetUser () {
            try {
                return Ok (await _userService.GetUser ());
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpGet ("shorter")]
        public IActionResult GetShortUser () {
            try {
                return Ok (_userService.GetShortUser ());
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpPut]
        public IActionResult UpdateUser ([FromBody] UserUpdateDto updateDto) {
            try {
                _userService.UpdateUser (updateDto);
                return Ok ();
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpGet ("genders")]
        public IActionResult GetGenders () {
            try {
                return Ok (_userService.GetGenders ());
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpPut ("profile-image")]
        public IActionResult UpdateProfileImage ([FromBody] byte[] image) {
            try {
                _userService.UpdateProfileImage (image);
                return Ok ();
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpPut ("password")]
        public async Task<IActionResult> UpdatePassword ([FromBody] PasswordUpdateModel model) {
            try {
                await _userService.UpdatePassword (model.OldPassword, model.NewPassword);
                return Ok ();
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpPut ("username")]
        public async Task<IActionResult> UpdateUsername ([FromBody] UsernameUpdateModel model) {
            try {
                await _userService.UpdateUsername (model.UserName);
                return Ok ();
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }

        [HttpPut ("disable")]
        public IActionResult DisableUser () {
            try {
                _userService.DisableUser ();
                return Ok ();
            } catch (Exception e) {
                return BadRequest (_utilsService.ExceptionToResponse (e));
            }
        }
    }
}