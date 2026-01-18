using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Address
    {
        public int Id { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("houseNumber")]
        public string HouseNumber { get; set; }
        [JsonPropertyName("postalCode")]
        public int PostalCode { get; set; }

    }
}
