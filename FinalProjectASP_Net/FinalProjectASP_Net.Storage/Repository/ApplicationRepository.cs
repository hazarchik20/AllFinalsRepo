using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Storage.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _dataContext;
        public ApplicationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Add(Application entity)
        {
            _dataContext.Applications.Add(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(Application entity)
        {
            _dataContext.Applications.Remove(entity);   
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int employeeId, int vacancyId)
        {
           return await _dataContext.Applications.AnyAsync(a => a.EmployeeId == employeeId && a.VacancyId == vacancyId);
        }

        public async Task<IEnumerable<Application>> GetAll(int limit, int offset)
        {
           return await _dataContext.Applications.ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetByEmployee(int employeeId, int limit, int offset)
        {
          return await _dataContext.Applications.Where(a => a.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<Application?> GetById(int id)
        {
            return await _dataContext.Applications.FindAsync(id);
        }

        public async Task<IEnumerable<Application>> GetByVacancy(int vacancyId, int limit, int offset)
        {
            return await _dataContext.Applications.Where(a => a.VacancyId == vacancyId).ToListAsync();
        }
        public async Task UpdateStatus(string status, int applicationsId)
        {
            var application = await _dataContext.Applications.FindAsync(applicationsId);
            if (application != null)
            {
                
                application.Status = new ApplicationStatus;
                _dataContext.Applications.Update(application);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Update(Application entity)
        {
            _dataContext.Applications.Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
