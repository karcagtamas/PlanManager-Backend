using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IUtilsService _utilsService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="friendService">Friend Service</param>
        /// <param name="utilsService">Utils Service</param>
        public FriendController(IFriendService friendService, IUtilsService utilsService)
        {
            _friendService = friendService;
            _utilsService = utilsService;
        }

        /// <summary>
        /// Get current user's got friend requests
        /// </summary>
        /// <param name="type">Undecided / accepted / declined</param>
        /// <returns>Server Response</returns>
        [HttpGet("request")]
        public IActionResult GetMyFriendRequests([FromBody] FriendRequestFilterModel model)
        {
            try
            {
                return Ok(new ServerResponse<List<FriendRequestListDto>>(_friendService.GetMyFriendRequests(model), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
            }
        }

        /// <summary>
        /// Get current user's friends
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet]
        public IActionResult GetMyFriends()
        {
            try
            {
                return Ok(new ServerResponse<List<FriendListDto>>(_friendService.GetMyFriends(), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
            }
        }

        /// <summary>
        /// Remove current user's friend by Id
        /// </summary>
        /// <param name="friendId">Friend Id</param>
        /// <returns>Server Response</returns>
        [HttpDelete("{friendId}")]
        public IActionResult RemoveFriend(string friendId)
        {
            try
            {
                _friendService.RemoveFriend(friendId);
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
            }
        }

        /// <summary>
        /// Send friend request
        /// </summary>
        /// <param name="model">Model of the request</param>
        /// <returns>Server Response</returns>
        [HttpPost("request")]
        public IActionResult SendFriendRequest(FriendRequestModel model)
        {
            try
            {
                _friendService.SendFriendRequest(model);
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
            }
        }

        /// <summary>
        /// Send friend request response
        /// </summary>
        /// <param name="model">Model of response</param>
        /// <returns>Server Response</returns>
        [HttpPut("request")]
        public IActionResult SendFriendRequestResponse(FriendRequestResponseModel model)
        {
            try
            {
                _friendService.SendFriendRequestResponse(model);
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
            }
        }
    }
}
