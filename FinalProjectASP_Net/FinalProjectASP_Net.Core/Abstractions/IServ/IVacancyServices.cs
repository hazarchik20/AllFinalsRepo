using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface IVacancyServices : IBaseServices<Vacancy,VacancyResponse>
    {
        Task<IEnumerable<VacancyResponse>> GetActive(int limit, int offset);
        Task<IEnumerable<VacancyResponse>> GetByCompany(int companyId, int limit, int offset);
    }
}
