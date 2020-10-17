using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PlanController : ControllerBase
    {
        public PlanController()
        {
        }
    }
}