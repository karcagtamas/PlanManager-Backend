using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : MyController<Season, SeasonModel, SeasonListDto, SeasonDto>
    {
        protected readonly ISeasonService SeasonService;
        public SeasonController(ISeasonService seasonService, ILoggerService loggerService) : base(loggerService, seasonService)
        {
            this.SeasonService = seasonService;
        }
    }
}
