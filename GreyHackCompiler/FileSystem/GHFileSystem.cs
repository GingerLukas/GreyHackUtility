using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GreyHackCompiler.FileSystem;

namespace GreyHackCompiler.FileSystem
{
    public class GHFileSystem
    {
        public string PublicIp
        {
            get { return _publicIp; }
            set
            {
                _publicIp = value;
                RecalculateIpOrderValue();
            }
        }

        private string _publicIp;

        public string LocalIp
        {
            get { return _localIp;}
            set
            {
                _localIp = value;
                RecalculateIpOrderValue();
            }
        }

        private string _localIp;
        public GHFileSystemNode RootNode { get; set; }


        public Dictionary<string,GHFileSystemUser> Users = new Dictionary<string, GHFileSystemUser>();

        public static List<GHFileSystem> LoadFromString(string input)
        {
            List<GHFileSystem> output = new List<GHFileSystem>();
            Queue<char> queue = new Queue<char>(input);
            while (queue.Count>0)
            {
                GHFileSystem fileSystem = new GHFileSystem();
                StringBuilder sb = new StringBuilder();
                while (queue.Peek() != ';')
                {
                    sb.Append(queue.Dequeue());
                }

                queue.Dequeue();
                fileSystem.PublicIp = sb.ToString();

                sb.Clear();
                while (queue.Peek() != ';')
                {
                    sb.Append(queue.Dequeue());
                }

                queue.Dequeue();
                fileSystem.LocalIp = sb.ToString();

                sb.Clear();

                GHFileSystemNode activeNode = null;

                Queue<char> imageQueue = GHTextConvertor.HexToUtf16(queue);
                while (imageQueue.Count > 0)
                {
                    sb.Clear();
                    while (imageQueue.Peek() < 32768)
                    {
                        sb.Append(GHTextConvertor.Utf14ToDoubleUtf7(imageQueue.Dequeue()));
                    }

                    if (imageQueue.Peek() == (char) Depth.Up)
                    {
                        activeNode = activeNode.Parent;
                        imageQueue.Dequeue();
                        continue;
                    }

                    var tmp = new GHFileSystemNode(activeNode, sb.ToString());
                    tmp.FileFlags = (FileFlag) imageQueue.Dequeue() - 32768;
                    if (activeNode!=null)
                    {
                        activeNode.AddChild(tmp);
                        if (activeNode.IsHomeFolder && !fileSystem.Users.ContainsKey(tmp.Name))
                        {
                            fileSystem.Users.Add(tmp.Name,new GHFileSystemUser(){FileSystem = fileSystem,HomeFolder = tmp});
                        }
                        else if (tmp.IsRootUserFolder)
                        {
                            fileSystem.Users.Add(tmp.Name,new GHFileSystemUser(){FileSystem = fileSystem,HomeFolder = tmp});
                        }
                    }

                    if (imageQueue.Peek() == (char) Depth.Down)
                    {
                        imageQueue.Dequeue();
                        activeNode = tmp;
                    }
                }

                fileSystem.RootNode = activeNode;
                output.Add(fileSystem);
                queue.Dequeue();
            }

            return output;
        }

        public byte[] IpAsArray = new byte[8];

        private void RecalculateIpOrderValue()
        {
            int i = 0;
            foreach (var s in ToString().Split('.', ':').Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                IpAsArray[i++] = byte.Parse(s);
            }

            for (;i < IpAsArray.Length; i++)
            {
                IpAsArray[i] = 0;
            }

        }

        public override string ToString()
        {
            return $"{PublicIp}:{LocalIp}";
        }
    }

    [Flags]
    public enum FileFlag
    {
        directory = 1<<9,
        ownerWrite = 1<<8,
        ownerRead = 1<<7,
        ownerExecute = 1<<6,
        groupWrite = 1<<5,
        groupRead = 1<<4,
        groupExecute = 1<<3,
        otherWrite = 1<<2,
        otherRead = 1<<1,
        otherExecute = 1
    }

    enum Depth
    {
        Up = 33792,
        Down = 33793
    }
}
