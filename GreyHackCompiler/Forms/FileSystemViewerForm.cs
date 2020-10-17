using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GreyHackCompiler.FileSystem;

namespace GreyHackCompiler.Forms
{
    public partial class FileSystemUtilityForm : Form
    {
        public FileSystemUtilityForm()
        {
            InitializeComponent();

            //LoadAllFileSystems();
            ClearUi();
        }

        private void ClearUi()
        {
            _lblLocalIp.Text = "";
            _lblPublicIp.Text = "";
        }

        private void LoadAllFileSystems(string path)
        {
            _tvIPs.Nodes.Clear();
            var list = GHFileSystem.LoadFromString(File.ReadAllText(path)).OrderByIp().ToList();
            AddFileSystemToTreeView(list);

        }

        private void AddFileSystemToTreeView(List<GHFileSystem> fileSystems)
        {
            foreach (GHFileSystem fileSystem in fileSystems)
            {
                if (!_tvIPs.Nodes.ContainsKey(fileSystem.PublicIp))
                {
                    _tvIPs.Nodes.Add(fileSystem.PublicIp, fileSystem.PublicIp);
                }

                _tvIPs.Nodes[fileSystem.PublicIp].Nodes.Add(fileSystem.LocalIp, fileSystem.LocalIp);
                _tvIPs.Nodes[fileSystem.PublicIp].Nodes[fileSystem.LocalIp].Tag = fileSystem;
                _fileSystems[fileSystem.ToString()] = fileSystem;
            }
        }

        private Dictionary<string,GHFileSystem> _fileSystems = new Dictionary<string, GHFileSystem>();

        private void AddToTreeNodeCollection(GHFileSystemNode node, TreeNodeCollection treeNodeCollection)
        {
            treeNodeCollection.Add(node.Name);
            treeNodeCollection = treeNodeCollection[treeNodeCollection.Count - 1].Nodes;
            foreach (var child in node.Children)
            {
                AddToTreeNodeCollection(child.Value,treeNodeCollection);
            }
        }

        public void LoadFileSystem(GHFileSystem fileSystem)
        {
            _tvFileSystem.Nodes.Clear();
            _lbUsers.Items.Clear();

            _lblPublicIp.Text = fileSystem.PublicIp;
            _lblLocalIp.Text = fileSystem.LocalIp;


            AddToTreeNodeCollection(fileSystem.RootNode,_tvFileSystem.Nodes);
            _tvFileSystem.ExpandAll();
            _tvFileSystem.SelectedNode = _tvFileSystem.Nodes[0];

            foreach (var user in fileSystem.Users)
            {
                _lbUsers.Items.Add(user.Value);
            }
        }

        private void _tvIPs_DoubleClick(object sender, EventArgs e)
        {
            if (_tvIPs.SelectedNode == null || _tvIPs.SelectedNode.Tag == null)
            {
                return;
            }
            LoadFileSystem((GHFileSystem)_tvIPs.SelectedNode.Tag);
        }

        private void _lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lbUsers.SelectedItem==null)
            {
                return;
            }

            _tvFileSystem.Nodes.Clear();
            AddToTreeNodeCollection(((GHFileSystemUser) _lbUsers.SelectedItem).HomeFolder,_tvFileSystem.Nodes);
            _tvFileSystem.ExpandAll();
        }

        private void FileSystemUtilityForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void FileSystemUtilityForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                LoadAllFileSystems(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
