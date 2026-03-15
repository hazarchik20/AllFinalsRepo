using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IRepo
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        Task<IEnumerable<Application>> GetByEmployee(int employeeId);
        Task<IEnumerable<Application>> GetByVacancy(int vacancyId);

        Task<bool> Exists(int employeeId, int vacancyId);
    }
}
