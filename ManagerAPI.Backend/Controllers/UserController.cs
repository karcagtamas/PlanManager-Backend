using System.Threading.Tasks;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _userService.GetUser());
        }

        [HttpGet("shorter")]
        public IActionResult GetShortUser()
        {
            return Ok(_userService.GetShortUser());
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserModel model)
        {
            _userService.UpdateUser(model);
            return Ok();
        }

        [HttpPut("profile-image")]
        public IActionResult UpdateProfileImage([FromBody] byte[] image)
        {
            _userService.UpdateProfileImage(image);
            return Ok();
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            await _userService.UpdatePassword(model.OldPassword, model.NewPassword);
            return Ok();
        }

        [HttpPut("username")]
        public async Task<IActionResult> UpdateUsername([FromBody] UsernameUpdateModel model)
        {
            await _userService.UpdateUsername(model.UserName);
            return Ok();
        }

        [HttpPut("disable")]
        public IActionResult DisableUser()
        {
            _userService.DisableUser();
            return Ok();
        }
    }
}