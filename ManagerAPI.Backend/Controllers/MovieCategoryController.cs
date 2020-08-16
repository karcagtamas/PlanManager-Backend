using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/movie-category")]
    [ApiController]
    [Authorize]
    public class MovieCategoryController : MyController<MovieCategory, MovieCategoryModel, MovieCategoryDto, MovieCategoryDto>
    {
        public MovieCategoryController(ILoggerService logger, IRepository<MovieCategory> service) : base(logger, service)
        {
        }
    }
}