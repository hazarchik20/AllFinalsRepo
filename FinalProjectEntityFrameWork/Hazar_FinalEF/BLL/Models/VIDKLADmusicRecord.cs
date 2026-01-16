using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.ModelsInfo;

namespace BLL.Models
{
    public class VIDKLADmusicRecord
    {
        public int ID { get; set; }

        public People People { get; set; }

        public DateTime DateVidklad { get; set; }
    }
}
