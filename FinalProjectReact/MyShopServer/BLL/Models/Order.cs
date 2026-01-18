using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Order
    {
        public int Id { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public int AddressId { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }

    }
}
