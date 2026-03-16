using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class VacancyNotFoundException : Exception
    {
        public VacancyNotFoundException() : base("Vacancy was not found.") { }
    }
}
