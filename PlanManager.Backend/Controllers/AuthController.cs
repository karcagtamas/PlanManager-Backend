using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlanManager.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetSomething()
        {
            return Ok(new[] {"Alma", "Nem"});
        }
    }
}