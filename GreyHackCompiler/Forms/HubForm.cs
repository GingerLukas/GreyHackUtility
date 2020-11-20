using System;
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
