using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Category
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
