using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
    [ApiController]
    public class MovieController : MyController<Movie, MovieModel, MovieListDto, MovieDto>
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService) : base(movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("my")]
        public IActionResult GetMyList()
        {
            return Ok(this._movieService.GetMyList());
        }

        [HttpGet("my/{id}")]
        public IActionResult GetMy(int id)
        {
            return Ok(this._movieService.GetMy(id));
        }

        [HttpGet("selector")]
        public IActionResult GetMySelectorList([FromQuery] bool onlyMine)
        {
            return Ok(this._movieService.GetMySelectorList(onlyMine));
        }

        [HttpPut("map")]
        public IActionResult UpdateMyMovies([FromBody] MyMovieModel model)
        {
            this._movieService.UpdateMyMovies(model.Ids);
            return Ok();
        }

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] List<MovieSeenUpdateModel> models)
        {
            foreach (var model in models)
            {
                this._movieService.UpdateSeenStatus(model.Id, model.Seen);
            }

            return Ok();
        }

        [HttpPost("map/{id}")]
        public IActionResult AddMovieToMyMovies(int id)
        {
            this._movieService.AddMovieToMyMovies(id);
            return Ok();
        }

        [HttpDelete("map/{id}")]
        public IActionResult RemoveMovieFromMyMovies(int id)
        {
            this._movieService.RemoveMovieFromMyMovies(id);
            return Ok();
        }

        [HttpPut("image/{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult UpdateImage(int id, [FromBody] MovieImageModel model)
        {
            this._movieService.UpdateImage(id, model);
            return Ok();
        }

        [HttpPut("categories/{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult UpdateCategories(int id, [FromBody] MovieCategoryUpdateModel model)
        {
            this._movieService.UpdateCategories(id, model);
            return Ok();
        }

        [HttpPut("rate/{id}")]
        public IActionResult UpdateRate(int id, [FromBody] MovieRateModel model)
        {
            this._movieService.UpdateRate(id, model);
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public override IActionResult Create([FromBody] MovieModel model)
        {
            this._movieService.Add<MovieModel>(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Root,Status Library Administrator")]
        public override IActionResult Delete(int id)
        {
            this._movieService.Remove(id);
            return Ok();
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public override IActionResult Update(int id, MovieModel model)
        {
            this._movieService.Update<MovieModel>(id, model);
            return Ok();
        }
    }
}