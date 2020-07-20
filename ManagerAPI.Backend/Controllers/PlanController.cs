using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers {
    
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PlanController : ControllerBase {
        private readonly ILoggerService _loggerService;

        public PlanController (ILoggerService loggerService) {
            _loggerService = loggerService;
        }
    }
}