using System;
using System.Threading.Tasks;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers {
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;

        public UserController (IUserService userService, ILoggerService loggerService) {
            _userService = userService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser () {
            try {
                return Ok (await _userService.GetUser ());
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }

        [HttpGet ("shorter")]
        public IActionResult GetShortUser () {
            try {
                return Ok (_userService.GetShortUser ());
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut]
        public IActionResult UpdateUser ([FromBody] UserModel model) {
            try {
                _userService.UpdateUser (model);
                return Ok ();
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut ("profile-image")]
        public IActionResult UpdateProfileImage ([FromBody] byte[] image) {
            try {
                _userService.UpdateProfileImage (image);
                return Ok ();
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut ("password")]
        public async Task<IActionResult> UpdatePassword ([FromBody] PasswordUpdateModel model) {
            try {
                await _userService.UpdatePassword (model.OldPassword, model.NewPassword);
                return Ok ();
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut ("username")]
        public async Task<IActionResult> UpdateUsername ([FromBody] UsernameUpdateModel model) {
            try {
                await _userService.UpdateUsername (model.UserName);
                return Ok ();
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }

        [HttpPut ("disable")]
        public IActionResult DisableUser () {
            try {
                _userService.DisableUser ();
                return Ok ();
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception e) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR), e));
            }
        }
    }
}