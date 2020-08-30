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
    [Authorize(Roles = "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
    [ApiController]
    public class SeriesController : MyController<Series, SeriesModel, SeriesListDto, SeriesDto>
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService) : base(seriesService)
        {
            this._seriesService = seriesService;
        }

        [HttpGet("my")]
        public IActionResult GetMyList()
        {
            return Ok(this._seriesService.GetMyList());
        }

        [HttpGet("my/{id}")]
        public IActionResult GetMy(int id)
        {
            return Ok(this._seriesService.GetMy(id));
        }

        [HttpGet("selector")]
        public IActionResult GetMySelectorList([FromQuery] bool onlyMine)
        {
            return Ok(this._seriesService.GetMySelectorList(onlyMine));
        }

        [HttpPut("map")]
        public IActionResult UpdateMySeries([FromBody] MySeriesModel model)
        {
            this._seriesService.UpdateMySeries(model.Ids);
            return Ok();
        }

        [HttpPut("map/status")]
        public IActionResult UpdateSeenStatus([FromBody] SeriesSeenStatusModel model)
        {
            this._seriesService.UpdateSeenStatus(model.Id, model.Seen);
            return Ok();
        }

        [HttpPost("map/{id}")]
        public IActionResult AddSeriesToMySeries(int id)
        {
            this._seriesService.AddSeriesToMySeries(id);
            return Ok();
        }

        [HttpDelete("map/{id}")]
        public IActionResult RemoveBookFromMyBooks(int id)
        {
            this._seriesService.RemoveSeriesFromMySeries(id);
            return Ok();
        }

        [HttpPut("image/{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult UpdateImage(int id, [FromBody] SeriesImageModel model)
        {
            this._seriesService.UpdateImage(id, model);
            return Ok();
        }

        [HttpPut("categories/{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public IActionResult UpdateCategories(int id, [FromBody] SeriesCategoryUpdateModel model)
        {
            this._seriesService.UpdateCategories(id, model);
            return Ok();
        }

        [HttpPut("rate/{id}")]
        public IActionResult UpdateRate(int id, [FromBody] SeriesRateModel model)
        {
            this._seriesService.UpdateRate(id, model);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public override IActionResult Create([FromBody] SeriesModel model)
        {
            this._seriesService.Add<SeriesModel>(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Root,Status Library Administrator")]
        public override IActionResult Delete(int id)
        {
            this._seriesService.Remove(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public override IActionResult Update(int id, SeriesModel model)
        {
            this._seriesService.Update<SeriesModel>(id, model);
            return Ok();
        }
    }
}