using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IRepo
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<Company?> GetCompanyWithVacancies(int companyId);
    }
}
