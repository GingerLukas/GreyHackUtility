using System.Collections.Generic;
using System.Linq;
using GreyHackCompiler.FileSystem;

namespace GreyHackCompiler
{
    public static class GHExtension
    {
        public static IEnumerable<GHFileSystem> OrderByIp(this IEnumerable<GHFileSystem> enumerable)
        {
            return enumerable.OrderBy(x => x.IpAsArray[0])
                .ThenBy(x => x.IpAsArray[1]).ThenBy(x => x.IpAsArray[2])
                .ThenBy(x => x.IpAsArray[3]).ThenBy(x => x.IpAsArray[4])
                .ThenBy(x => x.IpAsArray[5]).ThenBy(x => x.IpAsArray[6])
                .ThenBy(x => x.IpAsArray[7]);
        }
    }
}