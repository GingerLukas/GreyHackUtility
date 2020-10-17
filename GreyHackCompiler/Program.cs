using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyHackCompiler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path =
                @"C:\Users\lukas\OneDrive - Střední škola a vyšší odborná škola aplikované kybernetiky s.r.o\Plocha\GreyHack\Anonymous\images\part0.txt";
            GHFileSystem.LoadFromString(File.ReadAllText(path));


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
