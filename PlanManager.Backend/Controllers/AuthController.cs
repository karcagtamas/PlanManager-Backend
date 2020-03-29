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
        private readonly ILogger<AuthController> _logger;

        public AuthController (IAuthService authService, ILogger<AuthController> logger) {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost ("registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration ([FromBody] RegistrationModel model) {
            try {
                IdentityResult result = await _authService.Registration (model);
                return Ok (result);
            } catch (Exception e) {
                _logger.LogError (e.Message);
                return BadRequest (new ErrorResponse (e));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login ([FromBody] LoginModel model) {
            try {
                string token = await _authService.Login (model);
                return Ok (new { token });
            } catch (Exception e) {
                _logger.LogError (e.Message);
                return BadRequest (new ErrorResponse (e));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetSomething () {
            return Ok (new [] { "Alma", "Nem" });
        }

    }
}