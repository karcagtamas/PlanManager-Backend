using CsomorGenerator.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerAPI.Backend.Controllers
{
    public class GeneratorController : ControllerBase
    {
        private readonly IGeneratorService _generatorService;

        public GeneratorController(IGeneratorService generatorService)
        {
            this._generatorService = generatorService;
        }

        [HttpPut("generate")]
        public IActionResult Generate([FromBody] GeneratorSettings settings)
        {
            return Ok(this._generatorService.GenerateSimple(settings));
        }

        [HttpPost]
        public IActionResult Create([FromBody] GeneratorSettingsModel model)
        {
            this._generatorService.Create(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GeneratorSettingsModel model)
        {
            this._generatorService.Update(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            this._generatorService.Delete(id);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(this._generatorService.Get(id));
        }

        [HttpGet("public")]
        public IActionResult GetPublicList()
        {
            return Ok(this._generatorService.GetPublicList());
        }

        [HttpGet("my")]
        public IActionResult GetOwnedList()
        {
            return Ok(this._generatorService.GetOwnedList());
        }

        [HttpGet("shared")]
        public IActionResult GetSharedList()
        {
            return Ok(this._generatorService.GetSharedList());
        }

        [HttpPut("{id}/share")]
        public IActionResult Share([FromQuery] int id, [FromBody] List<CsomorAccessModel> models)
        {
            this._generatorService.Share(id, models);

            return Ok();
        }

        [HttpPut("{id}/public")]
        public IActionResult ChangePublicStatus([FromQuery] int id, [FromBody] bool status)
        {
            this._generatorService.ChangePublicStatus(id, status);

            return Ok();
        }
    }
}
