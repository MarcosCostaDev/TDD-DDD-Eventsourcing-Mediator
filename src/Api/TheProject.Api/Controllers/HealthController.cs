using Microsoft.AspNetCore.Mvc;
using TheProject.Shared.Resources;

namespace TheProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AppResource.HealthResult);
        }
    }
}
