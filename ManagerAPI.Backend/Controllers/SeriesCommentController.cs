using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/series-comment")]
    [ApiController]
    [Authorize]
    public class
        SeriesCommentController : MyController<SeriesComment, SeriesCommentModel, SeriesCommentListDto, SeriesCommentDto
        >
    {
        private readonly ISeriesCommentService _seriesCommentService;

        public SeriesCommentController(ISeriesCommentService service) : base(service)
        {
            this._seriesCommentService = service;
        }

        [HttpGet("series/{seriesId}")]
        public IActionResult GetList(int seriesId)
        {
            return Ok(this._seriesCommentService.GetList(seriesId));
        }
    }
}