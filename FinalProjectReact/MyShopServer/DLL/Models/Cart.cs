using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    }
}
