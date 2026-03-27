using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
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
            var company = await _service.GetById(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet("{id}/vacancies")]
        public async Task<IActionResult> GetVacancies(int id)
        {
            var company = await _service.GetVacancies(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShortCompanyRequest company)
        {
            await _service.Add(company);
            return Ok();
        }

        [Authorize(Roles = "HR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] ShortCompanyRequest company)
        {
            await _service.Update(id,company);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
