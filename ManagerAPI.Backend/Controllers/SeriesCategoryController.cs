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
    [Route("api/series-category")]
    [ApiController]
    [Authorize]
    public class SeriesCategoryController : MyController<SeriesCategory, SeriesCategoryModel, SeriesCategoryDto, SeriesCategoryDto>
    {
        private readonly ISeriesCategoryService _seriesCategoryService;
        public SeriesCategoryController(ILoggerService logger, ISeriesCategoryService service) : base(logger, service)
        {
            this._seriesCategoryService = service;
        }

        [HttpGet("selector/{seriesId}")]
        public IActionResult GetSelectorList(int seriesId)
        {
            try
            {
                return Ok(this._seriesCategoryService.GetSelectorList(seriesId));
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