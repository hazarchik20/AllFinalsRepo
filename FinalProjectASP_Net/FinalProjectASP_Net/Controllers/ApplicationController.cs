using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.GetAll(limit, offset);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var app = await _service.GetById(id);

            if (app == null)
                return NotFound();

            return Ok(app);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(int employeeId,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            var result = await _service.GetByEmployee(employeeId, limit, offset);
            return Ok(result);
        }

        [HttpGet("vacancy/{vacancyId}")]
        public async Task<IActionResult> GetByVacancy(int vacancyId,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            var result = await _service.GetByVacancy(vacancyId, limit, offset);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Core.Models.Application application)
        {
            await _service.Add(application);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Core.Models.Application application)
        {
            await _service.Update(application);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string status)
        {
            await _service.UpdateStatus(id, status);
            return Ok();
        }
    }
}
