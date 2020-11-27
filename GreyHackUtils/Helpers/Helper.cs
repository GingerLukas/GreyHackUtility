using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreyHackUtils.Helpers
{
    public static class Helper
    {

        public static bool CompareStrings(string one, int start, string two)
        {
            if (one.Length - start < two.Length)
            {
                return false;
            }
            for (int i = 0; i < two.Length; i++, start++)
            {
                if (one[start] != two[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
