using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Model
{
    public class CryptoMonet
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public Quote quote { get; set; }

    }
}
