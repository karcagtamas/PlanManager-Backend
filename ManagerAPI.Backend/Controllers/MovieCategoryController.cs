using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/movie-category")]
    [ApiController]
    [Authorize(Roles = "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
    public class
        MovieCategoryController : MyController<MovieCategory, MovieCategoryModel, MovieCategoryDto, MovieCategoryDto>
    {
        private readonly IMovieCategoryService _movieCategoryService;

        public MovieCategoryController(IMovieCategoryService service) : base(service)
        {
            this._movieCategoryService = service;
        }

        [HttpGet("selector/{movieId}")]
        public IActionResult GetSelectorList(int movieId)
        {
            return this.Ok(this._movieCategoryService.GetSelectorList(movieId));
        }
    }
}