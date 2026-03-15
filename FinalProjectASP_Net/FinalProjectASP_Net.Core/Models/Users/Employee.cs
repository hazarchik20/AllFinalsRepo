using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models
{
    public class Employee : UserBase
    {
        // якщо користувач є EMPLOYEE, то він може мати багато відгуків на вакансії (один до багатьох)
        public List<Application> Applications { get; set; } = new();

    }
}
