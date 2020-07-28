using System;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try againg later";
        private readonly ILoggerService _loggerService;
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService, ILoggerService loggerService)
        {
            _movieService = movieService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            try
            {
                return Ok(_movieService.GetMovies());
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

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            try
            {
                return Ok(_movieService.GetMovie(id));
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

        [HttpGet("my")]
        public IActionResult GetOwnMovies()
        {
            try
            {
                return Ok(_movieService.GetMyMovies());
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

        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieModel model)
        {
            try
            {
                _movieService.CreateMovie(model);
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

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieModel model)
        {
            try
            {
                _movieService.UpdateMovie(id, model);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);
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

        [HttpPut("map")]
        public IActionResult UpdateMyMovies([FromBody] MyMovieModel model)
        {
            try
            {
                _movieService.UpdateMyMovies(model.Ids);
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

        [HttpPut("map/status/{id}")]
        public IActionResult UpdateSeenStatus(int id, [FromBody] MovieSeenUpdateModel model)
        {
            try
            {
                _movieService.UpdateSeenStatus(id, model.Seen);
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
