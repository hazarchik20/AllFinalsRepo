using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
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

        [HttpGet("{id}/with-vacancies")]
        public async Task<IActionResult> GetWithVacancies(int id)
        {
            var company = await _service.GetWithVacancies(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Company company)
        {
            await _service.Add(company);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Company company)
        {
            await _service.Update(company);
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
