using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base("Requested resource was not found.") { }
    }
}
