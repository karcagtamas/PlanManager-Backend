using System;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
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
    public class SeriesController : MyController<Series, SeriesModel, SeriesListDto, SeriesDto>
    {
        protected readonly ISeriesService SeriesService;

        public SeriesController(ISeriesService seriesService, ILoggerService loggerService): base(loggerService, seriesService)
        {
            this.SeriesService = seriesService;
        }

        [HttpGet("my")]
        public IActionResult GetMySeries()
        {
            try
            {
                return Ok(this.SeriesService.GetMySeries());
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost("{seriesId}/season")]
        public IActionResult AddSeason(int seriesId, [FromBody] SeasonModel model)
        {
            try
            {
                this.SeriesService.AddSeason(seriesId, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost("season/{seasonId}")]
        public IActionResult AddEpisode(int seasonId, [FromBody] EpisodeModel model)
        {
            try
            {
                this.SeriesService.AddEpisode(seasonId, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("my")]
        public IActionResult UpdateMySeries([FromBody] MySeriesModel model)
        {
            try
            {
                this.SeriesService.UpdateMySeries(model.Ids);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("episode/{id}/status")]
        public IActionResult UpdateSeenStatus(int id, [FromBody] EpisodeSeenStatusModel model)
        {
            try
            {
                this.SeriesService.UpdateSeenStatus(id, model.Seen);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }
    }
}
