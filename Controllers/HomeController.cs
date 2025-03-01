using Microsoft.AspNetCore.Mvc;

namespace SpaceBasedPatternApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(true); // Return 200 with the value if found
        }
    }
}
