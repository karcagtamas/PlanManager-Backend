using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Models.Entities.MC;
using ManagerAPI.Models.Models;
using ManagerAPI.Models.Models.MC;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try again later";
        private readonly ILoggerService _loggerService;
        private readonly ISeriesService _seriesService;
        public SeriesController(ISeriesService seriesService, ILoggerService loggerService)
        {
            _seriesService = seriesService;
            _loggerService = loggerService;
        }

        [HttpGet("my")]
        public IActionResult GetMySeries()
        {
            try
            {
                return Ok(this._seriesService.GetMySeries());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet]
        public IActionResult GetAllSeries()
        {
            try
            {
                return Ok(this._seriesService.GetAllSeries());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMySeries(int id)
        {
            try
            {
                return Ok(this._seriesService.GetSeries(id));
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost]
        public IActionResult CreateSeries([FromBody] SeriesModel model)
        {
            try
            {
                this._seriesService.CreateSeries(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSeries(int id)
        {
            try
            {
                this._seriesService.DeleteSeries(id);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSeries(int id, [FromBody] SeriesModel model)
        {
            try
            {
                this._seriesService.UpdateSeries(id, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost("{seriesId}/season")]
        public IActionResult AddSeason(int seriesId, [FromBody] SeasonModel model)
        {
            try
            {
                this._seriesService.AddSeason(seriesId, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpDelete("season/{id}")]
        public IActionResult DeleteSeason(int id)
        {
            try
            {
                this._seriesService.DeleteSeason(id);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("season/{id}")]
        public IActionResult UpdateSeason(int id, [FromBody] SeasonModel model)
        {
            try
            {
                this._seriesService.UpdateSeason(id, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost("season/{seasonId}")]
        public IActionResult AddEpisode(int seasonId, [FromBody] EpisodeModel model)
        {
            try
            {
                this._seriesService.AddEpisode(seasonId, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpDelete("episode/{id}")]
        public IActionResult DeleteEpisode(int id)
        {
            try
            {
                this._seriesService.DeleteEpisode(id);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("my")]
        public IActionResult UpdateMySeries([FromBody] MySeriesModel model)
        {
            try
            {
                this._seriesService.UpdateMySeries(model.Ids);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("episode/{id}/status")]
        public IActionResult UpdateSeenStatus(int id, [FromBody] EpisodeSeenStatusModel model)
        {
            try
            {
                this._seriesService.UpdateSeenStatus(id, model.Seen);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }
    }
}
