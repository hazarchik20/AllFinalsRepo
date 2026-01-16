using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.LSin
{
    public class AddDirect
    {
        public DirectoryInfo directory;
        public DirectoryInfo NewDirect;
        public void AddDirectory ()
        {
            this.directory = new DirectoryInfo(".");

            this.NewDirect = new DirectoryInfo(directory.FullName+@"\UsersDirectory");
            if (!NewDirect.Exists)
                NewDirect.Create(); 
        }
    }
}
