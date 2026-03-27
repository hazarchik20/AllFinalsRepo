using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models.ResponseModels
{
    public class ApplicationReaponse
    {
            public int EmployeeId { get; set; }
            public int VacancyId { get; set; }
            public string Status { get; set; }
            public string CvPath { get; set; }// треба дописати запис CV в хмару( можливов якийсь файл поки ) і зберігати шлях до нього в базі даних
    }
}
