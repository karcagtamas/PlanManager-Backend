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
    [Route("api/series-comment")]
    [ApiController]
    [Authorize]
    public class SeriesCommentController : MyController<SeriesComment, SeriesCommentModel, SeriesCommentListDto, SeriesCommentDto>
    {
        private readonly ISeriesCommentService _seriesCommentService;
        public SeriesCommentController(ILoggerService logger, ISeriesCommentService service) : base(logger, service)
        {
            this._seriesCommentService = service;
        }

        [HttpGet("series/{seriesId}")]
        public IActionResult GetList(int seriesId) 
        {
            try
            {
                return Ok(this._seriesCommentService.GetList(seriesId));
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