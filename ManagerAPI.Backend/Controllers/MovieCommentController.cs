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
    [Authorize(Roles = "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
    public class
        MovieCommentController : MyController<MovieComment, MovieCommentModel, MovieCommentListDto, MovieCommentDto>
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

        [HttpPost]
        [Authorize(Roles =
            "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
        public override IActionResult Create([FromBody] MovieCommentModel model)
        {
            this._movieCommentService.Add<MovieCommentModel>(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles =
            "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
        public override IActionResult Delete(int id)
        {
            this._movieCommentService.Remove(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles =
            "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
        public override IActionResult Update(int id, MovieCommentModel model)
        {
            this._movieCommentService.Update<MovieCommentModel>(id, model);
            return Ok();
        }
    }
}