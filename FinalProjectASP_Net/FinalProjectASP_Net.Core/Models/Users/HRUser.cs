using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Models
{
    public class HRUser: UserBase
    {
        // якщо користувач є HR, то він буде пов'язаний з компанією, яку він представляє (один до одного)
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
