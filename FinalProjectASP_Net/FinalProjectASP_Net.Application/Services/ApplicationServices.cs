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
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;
        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(IApplicationRepository repository,ILogger<ApplicationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Core.Models.Application>> GetAll(int limit, int offset)
        {
            return await _repository.GetAll(limit, offset);
        }

        public async Task<Core.Models.Application> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Core.Models.Application>> GetByEmployee(int employeeId, int limit, int offset)
        {
            return await _repository.GetByEmployee(employeeId, limit, offset);
        }

        public async Task<IEnumerable<Core.Models.Application>> GetByVacancy(int vacancyId, int limit, int offset)
        {
            return await _repository.GetByVacancy(vacancyId, limit, offset);
        }

        public async Task Add(Core.Models.Application application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            var exists = await _repository.Exists(application.EmployeeId, application.VacancyId);

            if (exists)
                throw new Exception("Application already exists");

            await _repository.Add(application);

            _logger.LogInformation("Application created. Id: {Id}", application.Id);
        }

        public async Task Update(Core.Models.Application application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            var existing = await _repository.GetById(application.Id);

            if (existing == null)
                throw new Exception("Application not found");

            await _repository.Update(application);

            _logger.LogInformation("Application updated. Id: {Id}", application.Id);
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                throw new Exception("Application not found");
            await _repository.Delete(entity);

            _logger.LogInformation("Application deleted. Id: {Id}", entity.Id);
        }

        public async Task UpdateStatus(int applicationId, string status)
        {
            var application = await _repository.GetById(applicationId);

            if (application == null)
                throw new Exception("Application not found");

            await _repository.UpdateStatus(status, applicationId);

            _logger.LogInformation("Application status updated. Id: {Id}, Status: {Status}", applicationId, status);
        }

        
    }
}
