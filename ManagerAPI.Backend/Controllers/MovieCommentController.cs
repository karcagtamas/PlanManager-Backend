using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/movie-comment")]
    [ApiController]
    [Authorize]
    public class MovieCommentController : MyController<MovieComment, MovieCommentModel, MovieCommentListDto, MovieCommentDto>
    {
        private readonly IMovieCommentService _movieCommentService;
        public MovieCommentController(IMovieCommentService service) : base(service)
        {
            this._movieCommentService = service;
        }

        [HttpGet("movie/{movieId}")]
        public IActionResult GetList(int movieId) 
        {

                return Ok(this._movieCommentService.GetList(movieId));

        }
    }
}