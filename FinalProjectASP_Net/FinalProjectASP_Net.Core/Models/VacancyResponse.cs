using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models
{
    public class VacancyResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public int CompanyId { get; set; }
        public List<Application> Applications { get; set; } = new();
    }
}
