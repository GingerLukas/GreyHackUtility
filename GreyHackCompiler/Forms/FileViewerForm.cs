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
