using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] OpenWith)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (OpenWith.Length == 0)
                OpenWith = new string[1] { null };
            Application.Run(new MainForm(OpenWith[0]));
        }

        public static byte Yaz0Mode
        {
            get => Properties.Settings.Default.Yaz0Encode;
            set
            {
                Properties.Settings.Default.Yaz0Encode = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
