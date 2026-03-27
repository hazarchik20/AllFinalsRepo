using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
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

        public CompanyService(ICompanyRepository repository, ILogger<CompanyService> logger)
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

        public async Task<Company?> GetVacancies(int companyId)
        {
            return await _repository.GetCompanyVacancies(companyId);
        }

        public async Task Add(ShortCompanyRequest company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            var companyEntity = MapToMain(company);

            await _repository.Add(companyEntity);

            _logger.LogInformation("Company created. Id: {Id}", companyEntity.Id);
        }

        public async Task Update(int id, ShortCompanyRequest company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            var existing = await _repository.GetById(id);
            if (existing == null)
                throw new Exception("Company not found");

            var companyToUpdate = MapToMain(company);
            

            await _repository.Update(id, companyToUpdate);

            _logger.LogInformation("Company updated. Id: {Id}", id);
        }

        public async Task Delete(int id)
        {
            var company = await _repository.GetById(id);

            if (company == null)
                throw new Exception("Company not found");

            await _repository.Delete(company);

            _logger.LogInformation("Company deleted. Id: {Id}", id);
        }
        private Company MapToMain(ShortCompanyRequest request)
        {
            return new Company
            {
                Name = request.Name,
                Location = request.Location,
                Industry = request.Industry,
                Vacancies = new List<Vacancy>()

            };
        }
    }
}
