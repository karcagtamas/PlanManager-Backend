using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Models;
using PlanManager.Services.DTOs;
using PlanManager.Services.Services;

namespace PlanManager.Backend.Controllers {
    
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PlanController : ControllerBase {
        private readonly IUtilsService _utilsService;

        public PlanController (IUtilsService utilsService) {
            _utilsService = utilsService;
        }
        
    }
}