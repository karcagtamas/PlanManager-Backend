using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
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
        public MovieCommentController(ILoggerService logger, IMovieCommentService service) : base(logger, service)
        {
        }
    }
}