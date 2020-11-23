using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiiExplorer.Properties;
using System.Configuration;

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
            if (Settings.Default.IsNeedUpgrade)
            {
                Settings.Default.Upgrade();
                Settings.Default.IsNeedUpgrade = false;
                Settings.Default.Save();
            }

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

        public static class ProgramColours
        {
            public static Color ControlBackColor => Settings.Default.IsDarkMode ? Color.FromArgb(62, 62, 66) : Color.FromArgb(240, 240, 240);
            public static Color WindowColour => Settings.Default.IsDarkMode ? Color.FromArgb(37, 37, 38) : Color.FromArgb(255, 255, 255);
            public static Color TextColour => Settings.Default.IsDarkMode ? Color.FromArgb(241, 241, 241) : Color.FromArgb(0, 0, 0);
            public static Color BorderColour => Settings.Default.IsDarkMode ? Color.FromArgb(50, 50, 50) : Color.Gray;
        }
    }
}
