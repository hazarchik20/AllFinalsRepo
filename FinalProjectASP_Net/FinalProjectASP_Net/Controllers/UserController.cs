using Microsoft.AspNetCore.Mvc;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController: Controller
    {
        
        [HttpGet("HR")]
        public IActionResult GetHR()
        {
            return Ok();
        }

    }
}
