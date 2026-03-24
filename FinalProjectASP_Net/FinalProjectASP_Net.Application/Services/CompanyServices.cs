using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository repository,
                              ILogger<CompanyService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAll(int limit, int offset)
        {
            return await _repository.GetAll(limit, offset);
        }

        public async Task<Company?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Company?> GetWithVacancies(int companyId)
        {
            return await _repository.GetCompanyWithVacancies(companyId);
        }

        public async Task Add(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));
            if(company.Vacancies != null )
                company.Vacancies = new List<Vacancy>();

            await _repository.Add(company);

            _logger.LogInformation("Company created. Id: {Id}", company.Id);
        }

        public async Task Update(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            var existing = await _repository.GetById(company.Id);

            if (existing == null)
                throw new Exception("Company not found");

            await _repository.Update(company);

            _logger.LogInformation("Company updated. Id: {Id}", company.Id);
        }

        public async Task Delete(int id)
        {
            var company = await _repository.GetById(id);

            if (company == null)
                throw new Exception("Company not found");

            await _repository.Delete(company);

            _logger.LogInformation("Company deleted. Id: {Id}", id);
        }
    }
}
