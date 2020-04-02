using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;
using PlanManager.Services.Services;

namespace PlanManager.Backend.Controllers {
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;
        private readonly IUtilsService _utilsService;

        public AuthController (IAuthService authService, IUtilsService utilsService) {
            _authService = authService;
            _utilsService = utilsService;
        }

        [HttpPost ("registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration ([FromBody] RegistrationModel model) {
            try {
                IdentityResult result = await _authService.Registration (model);
                return Ok (result);
            } catch (Exception e) {
                return BadRequest (_utilsService.LogError(e));
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login ([FromBody] LoginModel model) {
            try {
                string token = await _authService.Login (model);
                return Ok (new { token });
            } catch (Exception e) {
                return BadRequest (_utilsService.LogError(e));
            }
        }
    }
}