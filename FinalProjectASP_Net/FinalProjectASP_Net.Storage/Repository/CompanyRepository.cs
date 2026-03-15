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

    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;
        public CompanyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Add(Company entity)
        {
            _dataContext.Companies.Add(entity); 
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(Company entity)
        {
           _dataContext.Companies.Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _dataContext.Companies.ToListAsync();
        }

        public async Task<Company?> GetById(int id)
        {
            return await _dataContext.Companies.FindAsync(id);
        }

        public async Task<Company?> GetCompanyWithVacancies(int companyId)
        {
            return await _dataContext.Companies.Include(c => c.Vacancies).FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public async Task Update(Company entity)
        {
            _dataContext.Companies.Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
