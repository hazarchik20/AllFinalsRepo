using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("You must be logged in to perform this action.") { }
    }
}
