using System.Collections.Generic;
using System.Linq;
using GreyHackUtils.FileSystem;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;


namespace GreyHackUtils.Helpers
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

        public static void AddToTreeNodeCollection(this GHFileSystemNode node, TreeNodeCollection treeNodeCollection)
        {
            treeNodeCollection.Add(node.Name);
            treeNodeCollection[treeNodeCollection.Count - 1].Tag = node;
            if (!string.IsNullOrWhiteSpace(node.Content))
                treeNodeCollection[treeNodeCollection.Count - 1].BackColor = Color.Aqua;
            treeNodeCollection = treeNodeCollection[treeNodeCollection.Count - 1].Nodes;
            foreach (var child in node.Children)
            {
                child.Value.AddToTreeNodeCollection(treeNodeCollection);
            }
        }
        public static List<Match> FindMatches(this IEnumerable<GHFileSystem> fileSystems, string pattern)
        {
            List<Match> output = new List<Match>();
            foreach (GHFileSystem fileSystem in fileSystems)
                output = output.Concat(fileSystem.FilesContents.Values.FindMatches(pattern)).ToList();
            

            return output;
        }

        public static List<Match> FindMatches(this IEnumerable<string> strings, string pattern)
        {
            List<Match> output = new List<Match>();
            foreach (string s in strings)
            {
                var matches = Regex.Matches(s, pattern);
                if (matches.Count>0)
                    foreach (Match match in matches)
                        output.Add(match);
            }

            return output;
        }


    }
}