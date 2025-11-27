using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Car
    {
        public int id { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public int mileage { get; set; }
        public string description { get; set; } 
        public string type { get; set; }   
        public decimal price { get; set; }      
        public double discount { get; set; }    
        public string imageUrl { get; set; }


    }
}
