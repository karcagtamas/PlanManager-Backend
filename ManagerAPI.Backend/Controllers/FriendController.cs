using System;
using System.Threading.Tasks;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
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
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly IFriendService _friendService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="friendService">Friend Service</param>
        /// <param name="loggerService">Utils Service</param>
        public FriendController(IFriendService friendService, ILoggerService loggerService)
        {
            _friendService = friendService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get current user's got friend requests
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet("request")]
        public IActionResult GetMyFriendRequests()
        {
            try
            {
                return Ok(_friendService.GetMyFriendRequests());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
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
                return Ok(_friendService.GetMyFriends());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        /// <summary>
        /// Get friend data by the given Id
        /// </summary>
        /// <param name="friendId">Friend's Id</param>
        /// <returns></returns>
        [HttpGet("data/{friendId}")]
        public async Task<IActionResult> GetFriendData(string friendId)
        {
            try
            {
                return Ok(await _friendService.GetFriendData(friendId));
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
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
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
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
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
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
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }
    }
}