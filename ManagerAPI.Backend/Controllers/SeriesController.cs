using System;
using System.Collections.Generic;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Domain.Enums.CM;
using ManagerAPI.Services.Common;
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
    public class SeriesController : MyController<Series, SeriesModel, SeriesListDto, SeriesDto, MovieCornerNotificationType>
    {
        protected readonly ISeriesService SeriesService;

        public SeriesController(ISeriesService seriesService, ILoggerService loggerService): base(loggerService, seriesService)
        {
            this.SeriesService = seriesService;
        }

        [HttpGet("my")]
        public IActionResult GetMyList()
        {
            try
            {
                return Ok(this.SeriesService.GetMyList());
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
                return Ok(this.SeriesService.GetMy(id));
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
                return Ok(this.SeriesService.GetMySelectorList(onlyMine));
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
                this.SeriesService.UpdateMySeries(model.Ids);
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
                this.SeriesService.UpdateSeenStatus(model.Id, model.Seen);
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
                this.SeriesService.AddSeriesToMySeries(id);
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
                this.SeriesService.RemoveSeriesFromMySeries(id);
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
