using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
    public class SeasonController : MyController<Season, SeasonModel, SeasonListDto, SeasonDto>
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService) : base(seasonService)
        {
            this._seasonService = seasonService;
        }

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] List<SeasonSeenStatusModel> models)
        {
            foreach (var season in models)
            {
                this._seasonService.UpdateSeenStatus(season.Id, season.Seen);
            }

            return Ok();
        }

        [HttpPost("{seriesId}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult AddIncremented(int seriesId)
        {
            this._seasonService.AddIncremented(seriesId);
            return Ok();
        }

        [HttpDelete("decremented/{seasonId}")]
        [Authorize(Roles = "Administrator,Root,Status Library Administrator")]
        public IActionResult DeleteDecremented(int seasonId)
        {
            this._seasonService.DeleteDecremented(seasonId);
            return Ok();
        }
    }
}