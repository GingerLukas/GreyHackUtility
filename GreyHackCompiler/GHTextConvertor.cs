using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreyHackCompiler
{
    class GHTextConvertor
    {
        public static GHTextConvertor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GHTextConvertor();
                }

                return _instance;
            }
        }

        private static  GHTextConvertor _instance;


        public GHTextConvertor()
        {
        }

        public void Setup()
        {

        }

        public Queue<char> HexToUtf16(Queue<char> queue, char cancelChar = ';')
        {
            StringBuilder tmp_hex = new StringBuilder();
            Queue<char> output = new Queue<char>();
            while (queue.Count>0 && queue.Peek()!=cancelChar)
            {
                tmp_hex.Append(queue.Dequeue());
                if (tmp_hex.Length==4)
                {
                    output.Enqueue( (char) int.Parse(tmp_hex.ToString(), System.Globalization.NumberStyles.HexNumber));
                    tmp_hex.Clear();
                }
            }

            return output;
        }
    }
}
