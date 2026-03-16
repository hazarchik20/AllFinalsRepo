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
    public class VacancyRepository : IVacancyRepository
    {
        private readonly DataContext _dataContext;
        public VacancyRepository(DataContext dataContext) 
        {        
            _dataContext = dataContext;
        }
        public async Task Add(Vacancy entity)
        {
           _dataContext.Vacancies.Add(entity);
              await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(Vacancy entity)
        {
            _dataContext.Vacancies.Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vacancy>> GetActiveVacancies(int limit, int offset)
        {
            return await _dataContext.Vacancies.Where(v => v.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Vacancy>> GetAll(int limit, int offset)
        {
           return await _dataContext.Vacancies.ToListAsync();
        }

        public async Task<IEnumerable<Vacancy>> GetByCompany(int companyId, int limit, int offset)
        {
            return await _dataContext.Vacancies.Where(v => v.CompanyId == companyId).ToListAsync();
        }

        public async Task<Vacancy?> GetById(int id)
        {
           return await _dataContext.Vacancies.FindAsync(id);
        }

        public async Task Update(Vacancy entity)
        {
            _dataContext.Vacancies.Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
