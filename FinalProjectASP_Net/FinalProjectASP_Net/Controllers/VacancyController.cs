using FinalProjectASP_Net.Core.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/vacancy")]
    public class VacancyController : Controller
    {
        [HttpPost]
        public IActionResult CreateVacancy()
        {
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAllVacancies()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVacancy(int id)
        {
            return Ok();
        }
    }
}
