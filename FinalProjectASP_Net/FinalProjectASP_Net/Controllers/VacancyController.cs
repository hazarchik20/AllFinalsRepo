using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/vacancy")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyServices _service;

        public VacancyController(IVacancyServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.GetAll(limit, offset);
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.GetActive(limit, offset);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vacancy = await _service.GetById(id);
            return Ok(vacancy);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(int companyId, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.GetByCompany(companyId, limit, offset);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vacancy vacancy)
        {
            await _service.Add(vacancy);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Vacancy vacancy)
        {
            await _service.Update(vacancy);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
