using System;
using System.Threading.Tasks;
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
    public class AuthController : ControllerBase {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IAuthService _authService;
        private readonly ILoggerService _loggerService;

        public AuthController (IAuthService authService, ILoggerService loggerService) {
            _authService = authService;
            _loggerService = loggerService;
        }

        [HttpPost ("registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration ([FromBody] RegistrationModel model) {
            try {
                await _authService.Registration (model);
                return Ok ();
            }
            catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost ("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login ([FromBody] LoginModel model) {
            try {
                string token = await _authService.Login (model);
                return Ok (token);
            } catch (MessageException me) {
                return BadRequest (_loggerService.ExceptionToResponse (me));
            } 
            catch (Exception) {
                return BadRequest (_loggerService.ExceptionToResponse (new Exception(FATAL_ERROR)));
            }
        }
    }
}