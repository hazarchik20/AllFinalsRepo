using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //роль буде використовуватись для перевірок(ADMIN - може редагувати всі вакансії; HR- може редагувати тільки вакансії своєї компанії; EMPLOYEE - може відгукуватись на резюме)
        public Role Role { get; set; } = Role.Employee;

    }
    public enum Role
    {
        Employee,
        HR,
        Admin,
    }
}
