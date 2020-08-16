using System;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/movie-category")]
    [ApiController]
    [Authorize]
    public class MovieCategoryController : MyController<MovieCategory, MovieCategoryModel, MovieCategoryDto, MovieCategoryDto>
    {
        private readonly IMovieCategoryService _movieCategoryService;
        public MovieCategoryController(ILoggerService logger, IMovieCategoryService service) : base(logger, service)
        {
            this._movieCategoryService = service;
        }

        [HttpGet("selector/{movieId}")]
        public IActionResult GetSelectorList(int movieId)
        {
            try
            {
                return Ok(this._movieCategoryService.GetSelectorList(movieId));
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }
    }
}