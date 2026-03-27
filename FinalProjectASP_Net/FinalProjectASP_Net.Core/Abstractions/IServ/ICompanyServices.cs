using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface ICompanyService : IBaseServices<Company, Company, ShortCompanyRequest>
    {
        Task<Company?> GetVacancies(int companyId);
    }
}
