using ManagerAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanManager.Services.Services;

namespace ManagerAPI.Backend.Controllers {
    
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