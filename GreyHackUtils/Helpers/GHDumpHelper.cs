using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using GingerGHClient;

namespace GreyHackUtils.Helpers
{
    public class GHDumpHelper
    {

        public static string patternAccount = @"(\w+|.+|@+):{1}([a-fA-F0-9]{32})";
        public static string patternBank = @"(\d+):{1}([a-fA-F0-9]{32})";
        public static string patternIp = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";

        public static List<string> FormatPasswdDump(string pathToPasswd, string pathToNetdToIp)
        {
            Dictionary<string,string> NetIdToIp = new Dictionary<string, string>();
            foreach (string line in File.ReadAllLines(pathToNetdToIp))
            {
                var tmp = line.Split(':');
                NetIdToIp[tmp[0]] = tmp[1];
            }
            List<string> output = new List<string>();
            foreach (string line in File.ReadAllLines(pathToPasswd))
            {
                var tmp = line.Split(':');
                if (NetIdToIp.ContainsKey(tmp[0]))
                    output.Add(NetIdToIp[tmp[0]]+':'+ tmp[1].Replace(";;", ":").Replace(';', ':'));
            }

            return output;
        }


        public static IEnumerable<string> ExtractIps(string path)
        {
            string[] array = File.ReadAllLines(path);
            var matches = array.FindMatches(patternIp);
            return matches.Select(x => x.Value);
        }

        public static string CompileAttack(string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string ip in File.ReadAllLines(path))
            {
                sb.Append("attack ");
                sb.Append(ip);
                sb.Append(" -f @@ ");
            }
            return sb.ToString();
        }

        public static GingerSave LoadSave(string path)
        {
            GingerSave save;
            using (FileStream fs = File.Open(GingerUtility.pathToMod + "save.ghmod", FileMode.Open))
            {
                save = (GingerSave)new BinaryFormatter().Deserialize(fs);
                fs.Close();
            }

            return save;
        }
    }
}
