using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreyHackCompiler
{
    class GHFileSystem
    {
        public string PublicIp { get; set; }
        public string LocalIp { get; set; }
        public GHFileSystemNode RootNode { get; set; }

        private static string Utf14ToDoubleUtf7(char c)
        {
            short tmp = (short) c;
            string s = "";
            tmp = (short) (tmp << 1);
            var bytes = BitConverter.GetBytes(tmp);
            if (bytes[1] != 0) s += (char) bytes[1];
            tmp = (short) (tmp << 7);
            bytes = BitConverter.GetBytes(tmp);
            bytes[1] <<= 1;
            bytes[1] >>= 1;
            if (bytes[1] != 0) s += (char) bytes[1];
            return s;
        }

        public static GHFileSystem LoadFromString(string input)
        {
            GHFileSystem fileSystem = new GHFileSystem();
            Queue<char> queue = new Queue<char>(input);
            StringBuilder sb = new StringBuilder();
            while (queue.Peek()!=';') {
                sb.Append(queue.Dequeue());
            }
            queue.Dequeue();
            fileSystem.PublicIp = sb.ToString();

            sb.Clear();
            while (queue.Peek() != ';') {
                sb.Append(queue.Dequeue());
            }
            queue.Dequeue();
            fileSystem.LocalIp = sb.ToString();

            sb.Clear();

            GHFileSystemNode activeNode = null;

            Queue<char> imageQueue = GHTextConvertor.Instance.HexToUtf16(queue);
            
            while (imageQueue.Count>0)
            {
                sb.Clear();
                while (imageQueue.Peek() < 32768)
                {
                    sb.Append(Utf14ToDoubleUtf7(imageQueue.Dequeue()));
                }

                if (imageQueue.Peek()==(char)Depth.Up)
                {
                    activeNode = activeNode.Parent;
                    imageQueue.Dequeue();
                    continue;
                }

                var tmp = new GHFileSystemNode(activeNode, sb.ToString());
                tmp.FileFlags = (FileFlag)imageQueue.Dequeue()-32768;
                activeNode?.AddChild(tmp);

                if (imageQueue.Peek()==(char)Depth.Down)
                {
                    imageQueue.Dequeue();
                    activeNode = tmp;
                }
            }

            fileSystem.RootNode = activeNode;
            return fileSystem;
        }
    }

    class GHFileSystemNode
    {
        public FileFlag FileFlags { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }


        public bool Directory
        {
            get { return (FileFlags & FileFlag.directory) > 0; }
            set { }
        }

        public bool OwnerWrite
        {
            get { return (FileFlags & FileFlag.ownerWrite) > 0; }
            set { }
        }

        public bool OwnerRead
        {
            get { return (FileFlags & FileFlag.ownerRead) > 0; }
            set { }
        }

        public bool OwnerExecute
        {
            get { return (FileFlags & FileFlag.ownerExecute) > 0; }
            set { }
        }

        public bool GroupWrite
        {
            get { return (FileFlags & FileFlag.groupWrite) > 0; }
            set { }
        }

        public bool GroupRead
        {
            get { return (FileFlags & FileFlag.groupRead) > 0; }
            set { }
        }

        public bool GroupExecute
        {
            get { return (FileFlags & FileFlag.groupExecute) > 0; }
            set { }
        }

        public bool OtherWrite
        {
            get { return (FileFlags & FileFlag.otherWrite) > 0; }
            set { }
        }

        public bool OtherRead
        {
            get { return (FileFlags & FileFlag.otherRead) > 0; }
            set { }
        }

        public bool OtherExecute
        {
            get { return (FileFlags & FileFlag.otherExecute) > 0; }
            set { }
        }

        public Dictionary<string,GHFileSystemNode> Children { get; set; }
        public GHFileSystemNode Parent { get; set; }

        public GHFileSystemNode(GHFileSystemNode parent,string name)
        {
            Name = name;
            Parent = parent ?? this;
            Children = new Dictionary<string, GHFileSystemNode>();
        }

        public void AddChild(GHFileSystemNode node)
        {
            Children.Add(node.Name,node);
        }

        private string ToString(int depth)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string(' ', depth*4) + Name);
            if (Children.Count != 0)
            {
                foreach (KeyValuePair<string, GHFileSystemNode> child in Children)
                {
                    sb.Append(child.Value.ToString(depth+1));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(0);
        }
    }

    [Flags]
    enum FileFlag
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
