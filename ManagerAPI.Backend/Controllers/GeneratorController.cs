﻿using CsomorGenerator.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/csomor")]
    [ApiController]
    [Authorize]
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
            return this.Ok(this._generatorService.Generate(settings));
        }

        [HttpPost]
        public IActionResult Create([FromBody] GeneratorSettingsModel model)
        {
            return this.Ok(this._generatorService.Create(model));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GeneratorSettingsModel model)
        {
            this._generatorService.Update(id, model);

            return this.Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            this._generatorService.Delete(id);

            return this.Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] int id)
        {
            return this.Ok(this._generatorService.Get(id));
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult GetPublicList()
        {
            return this.Ok(this._generatorService.GetPublicList());
        }

        [HttpGet("my")]
        public IActionResult GetOwnedList()
        {
            return this.Ok(this._generatorService.GetOwnedList());
        }

        [HttpGet("shared")]
        public IActionResult GetSharedList()
        {
            return this.Ok(this._generatorService.GetSharedList());
        }

        [HttpPut("{id}/share")]
        public IActionResult Share([FromRoute] int id, [FromBody] List<CsomorAccessModel> models)
        {
            this._generatorService.Share(id, models);

            return this.Ok();
        }

        [HttpPut("{id}/publish")]
        public IActionResult ChangePublicStatus([FromRoute] int id, [FromBody] GeneratorPublishModel model)
        {
            this._generatorService.ChangePublicStatus(id, model);

            return this.Ok();
        }

        [HttpGet("{id}/role")]
        [AllowAnonymous]
        public IActionResult GetRoleForCsomor([FromRoute] int id)
        {
            return this.Ok(this._generatorService.GetRoleForCsomor(id));
        }

        [HttpPut("{id}/export/pdf")]
        public IActionResult ExportPdf([FromRoute] int id, [FromBody] ExportSettingsModel model)
        {
            return this.Ok(this._generatorService.ExportPdf(id, model.Type, model.FilterList));
        }

        [HttpPut("{id}/export/xls")]
        public IActionResult ExportXls([FromRoute] int id, [FromBody] ExportSettingsModel model)
        {
            return this.Ok(this._generatorService.ExportXls(id, model.Type, model.FilterList));
        }

        [HttpGet("{id}/shared")]
        public IActionResult GetSharedPersonList([FromRoute] int id)
        {
            return this.Ok(this._generatorService.GetSharedPersonList(id));
        }

        [HttpGet("{id}/shared/correct")]
        public IActionResult GetCorrectPersonsForSharing([FromRoute] int id, [FromQuery] string name)
        {
            return this.Ok(this._generatorService.GetCorrectPersonsForSharing(id, name));
        }
    }
}
