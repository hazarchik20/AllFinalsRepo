using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Exceptions
{
    public class EmailAlreadyTakenException : Exception
    {
        public EmailAlreadyTakenException() : base("This email is already taken.") { }
    }
}
