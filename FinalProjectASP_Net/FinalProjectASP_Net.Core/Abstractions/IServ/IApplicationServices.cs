using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface IApplicationService : IBaseServices<Application,ApplicationReaponse>
    {
        Task<IEnumerable<ApplicationReaponse>> GetByEmployee(int employeeId, int limit, int offset);
        Task<IEnumerable<ApplicationReaponse>> GetByVacancy(int vacancyId, int limit, int offset);
        Task UpdateStatus(int applicationId, string status);
    }
}
