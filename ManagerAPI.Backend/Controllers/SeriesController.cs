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
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SeriesController : MyController<Series, SeriesModel, SeriesListDto, SeriesDto>
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService, ILoggerService loggerService): base(loggerService, seriesService)
        {
            this._seriesService = seriesService;
        }

        [HttpGet("my")]
        public IActionResult GetMyList()
        {
            try
            {
                return Ok(this._seriesService.GetMyList());
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
                return Ok(this._seriesService.GetMy(id));
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

        [HttpGet("selector")]
        public IActionResult GetMySelectorList([FromQuery] bool onlyMine)
        {
            try
            {
                return Ok(this._seriesService.GetMySelectorList(onlyMine));
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

        [HttpPut("map")]
        public IActionResult UpdateMySeries([FromBody] MySeriesModel model)
        {
            try
            {
                this._seriesService.UpdateMySeries(model.Ids);
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

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] SeriesSeenStatusModel model)
        {
            try
            {
                this._seriesService.UpdateSeenStatus(model.Id, model.Seen);
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

        [HttpPost("map/{id}")]
        public IActionResult AddSeriesToMySeries(int id) {
            try
            {
                this._seriesService.AddSeriesToMySeries(id);
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

        [HttpDelete("map/{id}")]
        public IActionResult RemoveBookFromMyBooks(int id) {
            try
            {
                this._seriesService.RemoveSeriesFromMySeries(id);
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
