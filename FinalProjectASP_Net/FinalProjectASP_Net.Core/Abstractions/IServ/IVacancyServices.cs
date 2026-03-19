using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface IVacancyServices : IBaseServices<Vacancy>
    {
        Task<IEnumerable<Vacancy>> GetActive(int limit, int offset);
        Task<IEnumerable<Vacancy>> GetByCompany(int companyId, int limit, int offset);
    }
}
