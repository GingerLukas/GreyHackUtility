using System.Collections.Generic;

namespace GreyHackUtils.Helpers
{
    class IPHelper
    {
        private static HashSet<string> _lanIpRanges = new HashSet<string>(new []
        {
            "10.0",
            "172.16",
            "192.168"
        });
        public static bool IsLan(string ip)
        {
            var tmp = ip.Split('.');
            if (tmp.Length!=4)
            {
                return false;
            }

            return _lanIpRanges.Contains(tmp[0] + '.' + tmp[1]);
        }
    }
}
