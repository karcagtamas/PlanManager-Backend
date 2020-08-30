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
    public class EpisodeController : MyController<Episode, EpisodeModel, EpisodeListDto, EpisodeDto>
    {
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService) : base(episodeService)
        {
            this._episodeService = episodeService;
        }

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] List<EpisodeSeenStatusModel> models)
        {
            foreach (var episode in models)
            {
                this._episodeService.UpdateSeenStatus(episode.Id, episode.Seen);
            }

            return Ok();
        }

        [HttpPost("{seasonId}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult AddIncremented(int seasonId)
        {
            this._episodeService.AddIncremented(seasonId);
            return Ok();
        }

        [HttpDelete("decremented/{episodeId}")]
        [Authorize(Roles = "Administrator,Root,Status Library Administrator")]
        public IActionResult DeleteDecremented(int episodeId)
        {
            this._episodeService.DeleteDecremented(episodeId);
            return Ok();
        }

        [HttpGet("my/{id}")]
        public IActionResult GetMy(int id)
        {
            return Ok(this._episodeService.GetMy(id));
        }

        [HttpPut("short/{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult UpdateShort(int id, [FromBody] EpisodeShortModel model)
        {
            this._episodeService.Update<EpisodeShortModel>(id, model);
            return Ok();
        }

        [HttpPut("image/{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult UpdateImage(int id, [FromBody] EpisodeImageModel model)
        {
            this._episodeService.UpdateImage(id, model);
            return Ok();
        }
    }
}