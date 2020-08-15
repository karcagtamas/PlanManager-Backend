using System;
using System.Collections.Generic;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : MyController<Episode, EpisodeModel, EpisodeListDto, EpisodeDto>
    {
        private readonly IEpisodeService _episodeService;
        public EpisodeController(IEpisodeService episodeService, ILoggerService loggerService) : base(loggerService, episodeService)
        {
            this._episodeService = episodeService;
        }

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] List<EpisodeSeenStatusModel> models)
        {
            try
            {
                foreach (var episode in models)
                {
                    this._episodeService.UpdateSeenStatus(episode.Id, episode.Seen);
                }
                return Ok();
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
        
        [HttpPost("{seasonId}")]
        public IActionResult AddIncremented(int seasonId)
        {
            try
            {
                this._episodeService.AddIncremented(seasonId);
                return Ok();
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
        
        [HttpDelete("decremented/{episodeId}")]
        public IActionResult DeleteDecremented(int episodeId)
        {
            try
            {
                this._episodeService.DeleteDecremented(episodeId);
                return Ok();
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
        
        [HttpGet("my/{id}")]
        public IActionResult GetMy(int id)
        {
            try
            {
                return Ok(this._episodeService.GetMy(id));
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

        [HttpPut("short/{id}")]
        public IActionResult UpdateShort(int id, [FromBody] EpisodeShortModel model)
        {
            try
            {
                this._episodeService.Update<EpisodeShortModel>(id, model);
                return Ok();
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
