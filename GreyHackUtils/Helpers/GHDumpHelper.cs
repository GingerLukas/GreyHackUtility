using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GreyHackUtils.FileSystem;

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
        }/*

        public static GingerSave LoadSave(string path)
        {
            GingerSave save;
            using (FileStream fs = File.Open(GingerUtility.pathToMod + "save.ghmod", FileMode.Open))
            {
                save = (GingerSave)new BinaryFormatter().Deserialize(fs);
                fs.Close();
            }

            return save;
        }*/
    }
    [Serializable]
    public class GingerSave
    {
        public Dictionary<string, HashSet<string>> NetIdToPlayersIps { get; set; }

        public Dictionary<string, string> NetIdToIp { get; set; }

        public Dictionary<string, string> IpToNetId { get; set; }

        public Dictionary<string, string> UrlToNetId { get; set; }

        public Dictionary<string, GHFileSystem> IpPairToFileSystem { get; set; }

        public HashSet<string> PlayerNetIds { get; set; }

        public HashSet<string> PlayerIPs { get; set; }

        public HashSet<string> NetIds { get; set; }

        public HashSet<string> Urls { get; set; }

        public HashSet<string> ShopUrls { get; set; }

        public HashSet<string> BankUrls { get; set; }

        public HashSet<string> MailUrls { get; set; }

        public Dictionary<string, string> RentedServers { get; set; }

        public Dictionary<string, string> WifiToNetId { get; set; }

        public GingerSave()
        {
            this.NetIdToPlayersIps = new Dictionary<string, HashSet<string>>();
            this.NetIdToIp = new Dictionary<string, string>();
            this.IpToNetId = new Dictionary<string, string>();
            this.UrlToNetId = new Dictionary<string, string>();
            this.IpPairToFileSystem = new Dictionary<string, GHFileSystem>();
            this.PlayerNetIds = new HashSet<string>();
            this.PlayerIPs = new HashSet<string>();
            this.NetIds = new HashSet<string>();
            this.Urls = new HashSet<string>();
            this.ShopUrls = new HashSet<string>();
            this.BankUrls = new HashSet<string>();
            this.MailUrls = new HashSet<string>();
            this.WifiToNetId = new Dictionary<string, string>();
        }
    }
}
