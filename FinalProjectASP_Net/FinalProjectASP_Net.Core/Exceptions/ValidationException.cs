using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Validation failed.") { }
    }
}
