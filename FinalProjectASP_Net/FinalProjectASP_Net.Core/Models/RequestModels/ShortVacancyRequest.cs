using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models.RequestModels
{
    public class ShortVacancyRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public int CompanyId { get; set; }
    }
}
