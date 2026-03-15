using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CvPath { get; set; }// треба дописати запис CV в хмару( можливов якийсь файл поки ) і зберігати шлях до нього в базі даних
    }
}
