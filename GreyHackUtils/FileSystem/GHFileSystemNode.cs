using System;
using System.Collections.Generic;
using System.Text;

namespace GreyHackUtils.FileSystem
{
    [Serializable]
    public class GHFileSystemNode
    {
        public GHFileSystem FileSystem { get; set; }
        public FileFlag FileFlags { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

        public string Content
        {
            get
            {
                if (FileSystem.FilesContents.ContainsKey(Path))
                    return FileSystem.FilesContents[Path];
                return null;
            }
        }

        public string Path
        {
            get
            {
                List<string> list = new List<string>();
                GHFileSystemNode node = this;
                do
                {
                    list.Add(node.Name);
                    node = node.Parent;
                } while (node!=node.Parent);
                list.Add("");
                list.Reverse();

                return string.Join("/", list.ToArray());
            }
        }

        public bool IsHomeFolder => Name == "home" && Parent.IsRootFolder;

        public bool IsRootFolder => Parent == this;

        public bool IsRootUserFolder => Name == "root" && Parent.IsRootFolder;

        #region Flags

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


        #endregion

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
            if (!Children.ContainsKey(node.Name))
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
}