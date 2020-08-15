using System;
using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
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
    public class SeasonController : MyController<Season, SeasonModel, SeasonListDto, SeasonDto>
    {
        private readonly ISeasonService _seasonService;
        public SeasonController(ISeasonService seasonService, ILoggerService loggerService) : base(loggerService, seasonService)
        {
            this._seasonService = seasonService;
        }

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] List<SeasonSeenStatusModel> models)
        {
            try
            {
                foreach (var season in models)
                {
                    this._seasonService.UpdateSeenStatus(season.Id, season.Seen);
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

        [HttpPost("{seriesId}")]
        public IActionResult AddIncremented(int seriesId)
        {
            try
            {
                this._seasonService.AddIncremented(seriesId);
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
        
        [HttpDelete("decremented/{seasonId}")]
        public IActionResult DeleteDecremented(int seasonId)
        {
            try
            {
                this._seasonService.DeleteDecremented(seasonId);
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
