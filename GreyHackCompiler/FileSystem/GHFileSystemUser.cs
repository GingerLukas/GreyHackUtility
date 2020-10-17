using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreyHackCompiler.FileSystem
{
    public class GHFileSystemUser
    {
        public GHFileSystemNode HomeFolder { get; set; }
        public GHFileSystem FileSystem { get; set; }

        public override string ToString()
        {
            return HomeFolder.Name;
        }
    }
}
