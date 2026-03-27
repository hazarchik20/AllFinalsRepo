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

        public async Task<IEnumerable<Company>> GetAll(int limit, int offset)
        {
            return await _dataContext.Companies
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Company?> GetById(int id)
        {
            return await _dataContext.Companies.FindAsync(id);
        }

        public async Task<Company?> GetByName(string name)
        {
            return await _dataContext.Companies.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Company?> GetCompanyVacancies(int companyId)
        {
            return await _dataContext.Companies.Include(c => c.Vacancies).FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public async Task Update(int id,Company entity)
        {
            var existingCompany = await _dataContext.Companies.FindAsync(id);
            if (existingCompany == null)
            {
                throw new KeyNotFoundException($"Company with id {id} not found.");
            }
            existingCompany.Name = entity.Name;
            existingCompany.Location = entity.Location;
            existingCompany.Industry = entity.Industry;

            _dataContext.Companies.Update(existingCompany);
            await _dataContext.SaveChangesAsync();
        }
    }
}
