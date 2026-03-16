using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException() : base("The operation could not be completed.") { }
    }
}
