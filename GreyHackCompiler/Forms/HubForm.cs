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
    public partial class HubForm : Form
    {
        public HubForm()
        {
            InitializeComponent();
        }

        private void _btnCompiler_Click(object sender, EventArgs e)
        {
            new CompilerForm().Show();
        }

        private void _btnFileSystemUtility_Click(object sender, EventArgs e)
        {
            new FileSystemUtilityForm().Show();
        }
    }
}
