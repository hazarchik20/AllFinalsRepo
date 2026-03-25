using FinalProjectASP_Net.Core.Abstractions.Base;
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

        public async Task<IEnumerable<ApplicationReaponse>> GetByEmployee(int employeeId, int limit, int offset)
        {
            return MapToResponse(await _repository.GetByEmployee(employeeId, limit, offset));
        }

        public async Task<IEnumerable<ApplicationReaponse>> GetByVacancy(int vacancyId, int limit, int offset)
        {
            return MapToResponse(await _repository.GetByVacancy(vacancyId, limit, offset));
        }

        public async Task<IEnumerable<ApplicationReaponse>> GetAll(int limit, int offset)
        {
            
            return MapToResponse(await _repository.GetAll(limit, offset));
        }

        public async Task<ApplicationReaponse> GetById(int id)
        {
            return MapToResponse(await _repository.GetById(id));
        }
        private ApplicationReaponse MapToResponse(Core.Models.Application application)=>
            new() { 
                CvPath = application.CvPath,
                EmployeeId = application.EmployeeId,
                VacancyId = application.VacancyId,
                Status = application.Status.ToString()
            };
        private IEnumerable<ApplicationReaponse> MapToResponse(IEnumerable<Core.Models.Application> applications) =>
            applications.Select(a => new ApplicationReaponse
            {
                CvPath = a.CvPath,
                EmployeeId = a.EmployeeId,
                VacancyId = a.VacancyId,
                Status = a.Status.ToString()
            });

    }
}
