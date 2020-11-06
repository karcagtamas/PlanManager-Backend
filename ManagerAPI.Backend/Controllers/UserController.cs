using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return this.Ok(await this._userService.GetUser());
        }

        [HttpGet("shorter")]
        public IActionResult GetShortUser()
        {
            return this.Ok(this._userService.GetShortUser());
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserModel model)
        {
            this._userService.UpdateUser(model);
            return this.Ok();
        }

        [HttpPut("profile-image")]
        public IActionResult UpdateProfileImage([FromBody] byte[] image)
        {
            this._userService.UpdateProfileImage(image);
            return this.Ok();
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            await this._userService.UpdatePassword(model.OldPassword, model.NewPassword);
            return this.Ok();
        }

        [HttpPut("username")]
        public async Task<IActionResult> UpdateUsername([FromBody] UsernameUpdateModel model)
        {
            await this._userService.UpdateUsername(model.UserName);
            return this.Ok();
        }

        [HttpPut("disable")]
        public IActionResult DisableUser()
        {
            this._userService.DisableUser();
            return this.Ok();
        }
    }
}