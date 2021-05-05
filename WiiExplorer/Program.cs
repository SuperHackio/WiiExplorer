using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiiExplorer.Properties;
using System.Configuration;
using System.Threading;
using System.Globalization;
using Hack.io.RARC;
using Hack.io.YAZ0;
using Hack.io.YAY0;

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

            if (OpenWith.Length > 1)
            {
                if ((OpenWith[0].Equals("--pack") || OpenWith[0].Equals("-p")) && File.GetAttributes(OpenWith[OpenWith.Length-1]) == FileAttributes.Directory)
                {
                    RARC Archive = new RARC();
                    Archive.Import(OpenWith[OpenWith.Length - 1]);
                    DirectoryInfo di = new DirectoryInfo(OpenWith[OpenWith.Length - 1]);
                    string output = Path.Combine(di.Parent.FullName, di.Name + ".arc");
                    Archive.Save(output);
                    if (OpenWith.Any(S => S.Equals("--yaz0") || S.Equals("-yz")))
                        YAZ0.Compress(output, OpenWith.Any(S => S.Equals("--fast") || S.Equals("-f")));
                    else if (OpenWith.Any(S => S.Equals("--yay0") || S.Equals("-yy")))
                        YAY0.Compress(output);
                    return;
                }
                else if ((OpenWith[0].Equals("--unpack") || OpenWith[0].Equals("-u")) && File.GetAttributes(OpenWith[OpenWith.Length - 1]) == FileAttributes.Normal)
                {
                    bool IsYaz0 = YAZ0.Check(OpenWith[OpenWith.Length - 1]);
                    bool IsYay0 = YAY0.Check(OpenWith[OpenWith.Length - 1]);
                    RARC Archive = IsYaz0 ? new RARC(YAZ0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]), OpenWith[OpenWith.Length - 1]) : (IsYay0 ? new RARC(YAY0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]), OpenWith[OpenWith.Length - 1]) : new RARC(OpenWith[OpenWith.Length - 1]));
                    string output = new FileInfo(Archive.FileName).Directory.FullName;
                    Archive.Export(output, OpenWith.Any(S => S.Equals("--overwrite") || S.Equals("-o")));
                    return;
                }

                for (int i = 0; i < OpenWith.Length; i++)
                {
                    if ((OpenWith[i].Equals("--lang") || OpenWith[i].Equals("-l")) && OpenWith.Length == i+2)
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(OpenWith[i+1]);
                }
            }
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ja");

            if (OpenWith.Length == 0)
                OpenWith = new string[1] { null };
            Application.Run(new MainForm(OpenWith[0]));
        }

        public static byte EncodingMode
        {
            get => Settings.Default.Yaz0Encode;
            set
            {
                Settings.Default.Yaz0Encode = value;
                Settings.Default.Save();
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