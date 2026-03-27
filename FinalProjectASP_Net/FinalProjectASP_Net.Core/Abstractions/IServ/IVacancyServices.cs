using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
using FinalProjectASP_Net.Core.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface IVacancyServices : IBaseServices<Vacancy, VacancyResponse, ShortVacancyRequest >
    {
        Task<IEnumerable<VacancyResponse>> GetActive(int limit, int offset);
        Task<IEnumerable<VacancyResponse>> GetByCompany(int companyId, int limit, int offset);
    }
}
