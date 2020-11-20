using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GreyHackUtils.FileSystem;
using GreyHackUtils.Helpers;

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
            var info = new FileInfo(path);
            if (info.Extension==".ghmod")
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = File.Open(path,FileMode.Open))
                {
                    GingerSave save = (GingerSave) bf.Deserialize(fs);
                    fileSystems = save.IpPairToFileSystem.Values.Select(x => x).ToList();
                    fs.Close();
                }   
            }
            else
            {
                fileSystems = GHFileSystem.LoadFromString(File.ReadAllText(path)).OrderByIp().ToList();
            }
            AddFileSystemToTreeView(fileSystems);

        }

        List<GHFileSystem> fileSystems = new List<GHFileSystem>();

        private void AddFileSystemToTreeView(List<GHFileSystem> fileSystems)
        {
            foreach (GHFileSystem fileSystem in fileSystems)
            {
                if (!_tvIPs.Nodes.ContainsKey(fileSystem.PublicIp))
                {
                    _tvIPs.Nodes.Add(fileSystem.PublicIp, fileSystem.PublicIp);
                }

                _tvIPs.Nodes[fileSystem.PublicIp].Nodes.Add(fileSystem.LocalIp, fileSystem.LocalIp);
                var publicNode = _tvIPs.Nodes[fileSystem.PublicIp];
                var node = publicNode.Nodes[fileSystem.LocalIp];
                node.Tag = fileSystem;
                if (fileSystem.IsRouter)
                {
                    node.ForeColor = Color.White;
                    node.BackColor = Color.Black;
                }
                else if (fileSystem.IsServer)
                {
                    node.ForeColor = Color.Yellow;
                    node.BackColor = Color.Red;
                }
                else
                {
                    node.ForeColor = Color.White;
                    node.BackColor = Color.Blue;
                }
                _fileSystems[fileSystem.ToString()] = fileSystem;



            }

            _tvIPs.ExpandAll();
            _tvIPs.SelectedNode = _tvIPs.Nodes[0];
        }

        private Dictionary<string,GHFileSystem> _fileSystems = new Dictionary<string, GHFileSystem>();


        public void LoadFileSystem(GHFileSystem fileSystem)
        {
            _tvFileSystem.Nodes.Clear();
            _lbUsers.Items.Clear();

            _lblPublicIp.Text = fileSystem.PublicIp;
            _lblLocalIp.Text = fileSystem.LocalIp;


            fileSystem.RootNode.AddToTreeNodeCollection(_tvFileSystem.Nodes);
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
            ((GHFileSystemUser)_lbUsers.SelectedItem).HomeFolder.AddToTreeNodeCollection(_tvFileSystem.Nodes);
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

        private void _tvFileSystem_DoubleClick(object sender, EventArgs e)
        {
            GHFileSystemNode node = _tvFileSystem.SelectedNode.Tag as GHFileSystemNode;
            if (node!=null)
            {
                new FileViewerForm(node.Content).Show();
            }
        }
    }
}
