using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string State { get; set; }
        public string Comment { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

    }
}
