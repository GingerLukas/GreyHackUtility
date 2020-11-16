using System;

namespace GreyHackUtils.FileSystem
{
    [Serializable]
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
