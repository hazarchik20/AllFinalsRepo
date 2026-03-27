using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models.RequestModels
{
    public class ShortApplicationRequest
    {
        public int EmployeeId { get; set; }
        public int VacancyId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string CvPath { get; set; }
    }
}
