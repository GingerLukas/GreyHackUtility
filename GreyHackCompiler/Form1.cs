using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyHackCompiler
{
    public partial class Form1 : Form
    {
        GHCompiler compiler = new GHCompiler();
        private string time_label_preset = "Last optimize time:";
        public Form1()
        {
            InitializeComponent();
            Center();
        }

        public void Center()
        {
            var tmp = beforeLabel.Location;
            tmp.X = inputRichTextBox.Location.X + (inputRichTextBox.Size.Width / 2) -
                                     (beforeLabel.Size.Width / 2);
            beforeLabel.Location = tmp;

            tmp = afterLabel.Location;
            tmp.X = outputRichTextBox.Location.X + (outputRichTextBox.Size.Width / 2) - (afterLabel.Size.Width / 2);
            afterLabel.Location = tmp;

            var center = (outputRichTextBox.Location.X - inputRichTextBox.Location.X - inputRichTextBox.Size.Width) / 2 + inputRichTextBox.Location.X + inputRichTextBox.Size.Width;

            tmp = timeLabel.Location;
            tmp.X = center-timeLabel.Size.Width/2;
            timeLabel.Location = tmp;

            tmp = rationLabel.Location;
            tmp.X = center - rationLabel.Size.Width / 2;
            rationLabel.Location = tmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputRichTextBox.Text))
            {
                outputRichTextBox.Text = compiler.Optimize(inputRichTextBox.Text);
                TimeSpan t = TimeSpan.FromTicks(compiler.LastOptimizeTimeTicks);
                string tmp = "null";
                if (t.Days!=0)
                {
                    tmp = t.TotalDays + " days";
                }
                else if(t.Hours!=0)
                {
                    tmp = t.TotalHours + " hours";
                }
                else if (t.Minutes!=0)
                {
                    tmp = t.TotalMinutes + " minutes";
                }
                else if (t.Seconds!=0)
                {
                    tmp = t.TotalSeconds + " seconds";
                }
                else
                {
                    tmp = t.TotalMilliseconds + " milliseconds";
                }
                timeLabel.Text = time_label_preset + " " + tmp;
                beforeLabel.Text = "Before length: " + compiler.BeforeLength;
                afterLabel.Text = "After length: " + compiler.AfterLength;
                rationLabel.Text = (100d-Math.Round(((compiler.AfterLength / compiler.BeforeLength) * 100), 4)).ToString()+"%";
            }
            Center();
        }
    }
}
