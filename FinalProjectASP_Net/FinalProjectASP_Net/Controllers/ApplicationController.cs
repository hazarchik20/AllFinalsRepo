using FinalProjectASP_Net.Core.Models.Users;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/application")]
    public class ApplicationController : Controller
    {
        //3. GET /applications/user - View all user’s applications
        //(User-only).
        //4. POST /applications/{id
        //    } - Apply for an application
        //(User-only).
        //5. PUT /applications/{id}/ status - Update application status
        //(Admin-only).

        [HttpPost]
        public IActionResult CreateApplication()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            return Ok();
        }

        [HttpGet("user")]
        public IActionResult GetUserApplications()
        {
            return Ok();
        }
        [HttpPost("{id}")]
        public IActionResult ApplyForApplication(int id)
        {
            return Ok();
        }
        [HttpPut("{id}/status")]
        public IActionResult UpdateApplicationStatus(int id)
        {
            return Ok();

        }

    }
}
