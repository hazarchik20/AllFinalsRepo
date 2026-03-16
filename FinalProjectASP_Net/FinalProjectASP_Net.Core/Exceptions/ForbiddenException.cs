using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("You do not have permission to perform this action.") { }
    }
}
