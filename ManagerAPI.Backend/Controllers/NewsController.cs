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
    /// News Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IUtilsService _utilsService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="newsService">News Service</param>
        /// <param name="utilsService">Utils Service</param>
        public NewsController(INewsService newsService, IUtilsService utilsService)
        {
            _newsService = newsService;
            _utilsService = utilsService;
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
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
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
                return Ok(new ServerResponse<List<NewsDto>>(_newsService.GetNewsPosts(), true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
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
                return Ok(new ServerResponse<object>(null, true));
            }
            catch (Exception e)
            {
                _utilsService.LogError(e);
                return Ok(new ServerResponse<object>(e));
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
