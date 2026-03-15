using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IRepo
{
    public interface IUserRepository : IBaseRepository<UserBase>
    {
        Task<UserBase?> GetByEmail(string email);
        Task<IEnumerable<Employee>> GetEmployee();
        Task<IEnumerable<HRUser>> GetHRUsers();
        Task<IEnumerable<Admin>> GetAdmins();
    }
}
