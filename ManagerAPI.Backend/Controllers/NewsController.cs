using System;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    /// <summary>
    /// News Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly INewsService _newsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="newsService">News Service</param>
        /// <param name="loggerService">Utils Service</param>
        public NewsController(INewsService newsService, ILoggerService loggerService)
        {
            _newsService = newsService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Delete news by Id
        /// </summary>
        /// <param name="postId">News Id</param>
        /// <returns>Server Response</returns>
        [HttpDelete("{postId}")]
        public IActionResult DeleteNews(int postId)
        {
            try
            {
                _newsService.DeleteNews(postId);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns>Server Response</returns>
        [HttpGet]
        public IActionResult GetNewsPosts()
        {
            try
            {
                return Ok(_newsService.GetNewsPosts());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="model">Model of news for creation</param>
        /// <returns>Server Response</returns>
        [HttpPost]
        public IActionResult PostNews([FromBody] PostModel model)
        {
            try
            {
                _newsService.PostNews(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="model">Model of news</param>
        /// <returns>Server Response</returns>
        [HttpPut("{postId}")]
        public IActionResult UpdateNews(int postId, [FromBody] PostModel model)
        {
            try
            {
                _newsService.UpdateNews(postId, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR), e));
            }
        }
    }
}