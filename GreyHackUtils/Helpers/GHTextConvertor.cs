using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GreyHackUtils.Helpers
{
    public class GHTextConvertor
    {
        public static string Utf14ToDoubleUtf7(char c)
        {
            short tmp = (short)c;
            string s = "";
            tmp = (short)(tmp << 1);
            var bytes = BitConverter.GetBytes(tmp);
            if (bytes[1] != 0) s += (char)bytes[1];
            tmp = (short)(tmp << 7);
            bytes = BitConverter.GetBytes(tmp);
            bytes[1] <<= 1;
            bytes[1] >>= 1;
            if (bytes[1] != 0) s += (char)bytes[1];
            return s;
        }

        public static Queue<char> HexToUtf16(Queue<char> queue, char cancelChar = ';')
        {
            StringBuilder tmp_hex = new StringBuilder();
            Queue<char> output = new Queue<char>();
            while (queue.Count>0 && queue.Peek()!=cancelChar)
            {
                tmp_hex.Append(queue.Dequeue());
                if (tmp_hex.Length==4)
                {
                    output.Enqueue( (char) int.Parse(tmp_hex.ToString(), NumberStyles.HexNumber));
                    tmp_hex.Clear();
                }
            }

            return output;
        }
    }
}
