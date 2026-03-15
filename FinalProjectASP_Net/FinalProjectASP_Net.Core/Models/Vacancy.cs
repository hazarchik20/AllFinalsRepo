using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Application> Applications { get; set; } = new();
    }
}
