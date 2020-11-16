using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyHackCompiler.Forms
{
    public partial class FileViewerForm : Form
    {
        public FileViewerForm()
        {
            InitializeComponent();
        }

        public FileViewerForm(string content) : this()
        {
            richTextBox1.Text = content;
        }
    }
}
