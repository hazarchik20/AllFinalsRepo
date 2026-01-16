using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class CmcResponseSingle<T>
    {
        public T data { get; set; }
    }
    public class CmcResponseDictionary<T>
    {
        public Dictionary<string, T> data { get; set; }
    }
}
