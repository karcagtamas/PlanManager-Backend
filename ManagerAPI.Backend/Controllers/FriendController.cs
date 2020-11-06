using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ManagerAPI.Backend.Controllers
{
    /// <summary>
    /// Friend Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="friendService">Friend Service</param>
        public FriendController(IFriendService friendService)
        {
            this._friendService = friendService;
        }

        /// <summary>
        /// Get current user's got friend requests
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet("request")]
        public IActionResult GetMyFriendRequests()
        {
            return this.Ok(this._friendService.GetMyFriendRequests());
        }

        /// <summary>
        /// Get current user's friends
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet]
        public IActionResult GetMyFriends()
        {
            return this.Ok(this._friendService.GetMyFriends());
        }

        /// <summary>
        /// Get friend data by the given Id
        /// </summary>
        /// <param name="friendId">Friend's Id</param>
        /// <returns></returns>
        [HttpGet("data/{friendId}")]
        public async Task<IActionResult> GetFriendData(string friendId)
        {
            return this.Ok(await this._friendService.GetFriendData(friendId));
        }

        /// <summary>
        /// Remove current user's friend by Id
        /// </summary>
        /// <param name="friendId">Friend Id</param>
        /// <returns>Server Response</returns>
        [HttpDelete("{friendId}")]
        public IActionResult RemoveFriend(string friendId)
        {
            this._friendService.RemoveFriend(friendId);
            return this.Ok();
        }

        /// <summary>
        /// Send friend request
        /// </summary>
        /// <param name="model">Model of the request</param>
        /// <returns>Server Response</returns>
        [HttpPost("request")]
        public IActionResult SendFriendRequest(FriendRequestModel model)
        {
            this._friendService.SendFriendRequest(model);
            return this.Ok();
        }

        /// <summary>
        /// Send friend request response
        /// </summary>
        /// <param name="model">Model of response</param>
        /// <returns>Server Response</returns>
        [HttpPut("request")]
        public IActionResult SendFriendRequestResponse(FriendRequestResponseModel model)
        {
            this._friendService.SendFriendRequestResponse(model);
            return this.Ok();
        }
    }
}