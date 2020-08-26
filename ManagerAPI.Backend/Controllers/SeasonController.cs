using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult AddIncremented(int seriesId)
        {
            this._seasonService.AddIncremented(seriesId);
            return Ok();
        }

        [HttpDelete("decremented/{seasonId}")]
        public IActionResult DeleteDecremented(int seasonId)
        {
            this._seasonService.DeleteDecremented(seasonId);
            return Ok();
        }
    }
}