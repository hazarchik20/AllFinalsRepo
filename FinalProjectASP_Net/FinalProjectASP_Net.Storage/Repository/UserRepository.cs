using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Storage.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Add(UserBase entity)
        {
            _dataContext.UserBases.Add(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(UserBase entity)
        {
            _dataContext.UserBases.Remove(entity);
            await _dataContext.SaveChangesAsync();
        }
        public Task<bool> IsEmailTaken(string email)
        {
            return _dataContext.UserBases.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Admin>> GetAdmins(int limit, int offset)
        {
            return await _dataContext.UserBases.OfType<Admin>().ToListAsync();
        }

        public async Task<IEnumerable<UserBase>> GetAll(int limit, int offset)
        {
            return await _dataContext.UserBases.ToListAsync();
        }

        public async Task<UserBase?> GetByEmail(string email)
        {
            return await _dataContext.UserBases.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserBase?> GetById(int id)
        {
           return await _dataContext.UserBases.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployee(int limit, int offset)
        {
            return await _dataContext.UserBases.OfType<Employee>().ToListAsync();
        }

        public async Task<IEnumerable<HRUser>> GetHRUsers(int limit, int offset)
        {
            return await _dataContext.UserBases.OfType<HRUser>().ToListAsync();
        }

        public async Task Update(UserBase entity)
        {
            _dataContext.UserBases.Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
