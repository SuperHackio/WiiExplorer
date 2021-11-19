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
using Hack.io;
using Hack.io.Util;
using Hack.io.RARC;
using Hack.io.U8;
using Hack.io.YAZ0;
using Hack.io.YAY0;
using System.Runtime.InteropServices;

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
                if ((OpenWith[0].Equals("--script") || OpenWith[0].Equals("-ss")))
                {
                    AttachConsole(ATTACH_PARENT_PROCESS);
                    List<string> Parameters = new List<string>();
                    for (int i = 2; i < OpenWith.Length; i++)
                        Parameters.Add(OpenWith[i]);
                    RunScript(OpenWith[1], Parameters.ToArray());
                    return;
                }
                if ((OpenWith[0].Equals("--pack") || OpenWith[0].Equals("-p")) && File.GetAttributes(OpenWith[OpenWith.Length-1]) == FileAttributes.Directory)
                {
                    Archive Archive = OpenWith.Any(S => S.ToLower().Equals("-u8")) ? (Archive)new U8() : new RARC();
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
                    Archive Archive;
                    if (IsU8())
                        Archive = IsYaz0 ? new U8(YAZ0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]), OpenWith[OpenWith.Length - 1]) : (IsYay0 ? new U8(YAY0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]), OpenWith[OpenWith.Length - 1]) : new U8(OpenWith[OpenWith.Length - 1]));
                    else
                        Archive = IsYaz0 ? new RARC(YAZ0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]), OpenWith[OpenWith.Length - 1]) : (IsYay0 ? new RARC(YAY0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]), OpenWith[OpenWith.Length - 1]) : new RARC(OpenWith[OpenWith.Length - 1]));
                    string output = new FileInfo(Archive.FileName).Directory.FullName;
                    Archive.Export(output, OpenWith.Any(S => S.Equals("--overwrite") || S.Equals("-o")));
                    return;

                    bool IsU8()
                    {
                        Stream arc;
                        if (IsYaz0)
                        {
                            arc = YAZ0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]);
                        }
                        else if (IsYay0)
                        {
                            arc = YAY0.DecompressToMemoryStream(OpenWith[OpenWith.Length - 1]);
                        }
                        else
                        {
                            arc = new FileStream(OpenWith[OpenWith.Length - 1], FileMode.Open);
                        }
                        bool Check = arc.ReadString(4) == U8.Magic;
                        arc.Close();
                        return Check;
                    }
                }

                for (int i = 0; i < OpenWith.Length; i++)
                {
                    if ((OpenWith[i].Equals("--lang") || OpenWith[i].Equals("-l")) && OpenWith.Length == i+2)
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(OpenWith[i+1]);
                }
            }
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");

            if (OpenWith.Length == 0)
                OpenWith = new string[1] { null };
            Application.Run(new MainForm(OpenWith[0]));
            return;
        }

        static void RunScript(string Filename, string[] parameters)
        {
            Console.WriteLine("Loading {0}...", Filename);
            string[] Lines = File.ReadAllLines(Filename);
            Console.WriteLine("Script Loaded.");
            Thread.Sleep(100);
            Archive CurrentArchive = null;
            string ErrorMessage = "";
            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i].StartsWith("//") || string.IsNullOrWhiteSpace(Lines[i]))
                    continue;
                string Line = string.Format(Lines[i], parameters);
                string[] Params = Line.Split(' ');
                Console.WriteLine("Executing Line {1}: \"{0}\"", Lines[i], i);
                switch (Params[0])
                {
                    case "new": //new
                        if (CurrentArchive is null)
                        {
                            if (Params.Contains("-u8"))
                                CurrentArchive = new U8();
                            else
                                CurrentArchive = new RARC();
                            CurrentArchive["Root"] = null;
                            Console.WriteLine("New Archive Created Successfully");
                        }
                        else
                        {
                            ErrorMessage = string.Format("Could not create a new archive:\n\tAn archive is already loaded in memory!");
                            goto Error;
                        }
                        break;
                    case "open": //open <filepath>
                        if (CurrentArchive is null)
                        {
                            if (Params.Length < 2)
                            {
                                ErrorMessage = string.Format("Incomplete Syntax - Expected\nopen <filepath>");
                                goto Error;
                            }
                            if (!File.Exists(Params[1]))
                            {
                                ErrorMessage = string.Format("File {0} could not be found", Params[1]);
                                goto Error;
                            }
                            bool IsYaz0 = YAZ0.Check(Params[1]);
                            bool IsYay0 = YAY0.Check(Params[1]);
                            if (IsU8())
                                CurrentArchive = IsYaz0 ? new U8(YAZ0.DecompressToMemoryStream(Params[1]), Params[1]) : (IsYay0 ? new U8(YAY0.DecompressToMemoryStream(Params[1]), Params[1]) : new U8(Params[1]));
                            else
                                CurrentArchive = IsYaz0 ? new RARC(YAZ0.DecompressToMemoryStream(Params[1]), Params[1]) : (IsYay0 ? new RARC(YAY0.DecompressToMemoryStream(Params[1]), Params[1]) : new RARC(Params[1]));
                            Console.WriteLine("Archive opened successfully!");
                            
                            bool IsU8()
                            {
                                Stream arc;
                                if (IsYaz0)
                                {
                                    arc = YAZ0.DecompressToMemoryStream(Params[1]);
                                }
                                else if (IsYay0)
                                {
                                    arc = YAY0.DecompressToMemoryStream(Params[1]);
                                }
                                else
                                {
                                    arc = new FileStream(Params[1], FileMode.Open);
                                }
                                bool Check = arc.ReadString(4) == U8.Magic;
                                arc.Close();
                                return Check;
                            }
                        }
                        else
                        {
                            ErrorMessage = string.Format("Could not open the archive:\n\tAn archive is already loaded in memory!");
                            goto Error;
                        }
                        break;
                    case "save": //save [filepath]
                        if (CurrentArchive is null)
                        {
                            ErrorMessage = string.Format("Save Failed! No Archive Loaded");
                            goto Error;
                        }
                        CurrentArchive.Save(Params.Length == 1 ? CurrentArchive.FileName : Params[1]);
                        Console.WriteLine("Archive saved successfully!");
                        break;
                    case "compress": //compress <filepath> [-yz|-yy|-yzf]
                        if (Params.Length < 3)
                        {
                            ErrorMessage = string.Format("Incomplete Syntax - Expected\ncompress <filepath> [-yz|-yy|-yzf]");
                            goto Error;
                        }
                        if (!File.Exists(Params[1]))
                        {
                            ErrorMessage = string.Format("File {0} could not be found", Params[1]);
                            goto Error;
                        }
                        switch (Params[2])
                        {
                            case "-yz":
                            case "-yzf":
                                Console.WriteLine("Compressing, please wait... {0}");
                                YAZ0.Compress(Params[1], Params[2].Contains("f"));
                                break;
                            case "-yy":
                                Console.WriteLine("Compressing, please wait... {0}");
                                YAY0.Compress(Params[1]);
                                break;
                            default:
                                ErrorMessage = string.Format("Encoding mode {0} doesn't exist", Params[1]);
                                goto Error;
                        }

                        Console.WriteLine("Compress complete!");
                        break;

                    case "replace":
                    case "add": //add <filepath> <archivepath>
                        string func = Params[0].Equals("replace") ? "replaced" : "added";
                        if (Params.Length < 3)
                        {
                            ErrorMessage = string.Format("Incomplete Syntax - Expected\n{0} <filepath> <archivepath>", Params[0]);
                            goto Error;
                        }
                        if (CurrentArchive is null)
                        {
                            ErrorMessage = string.Format("Add failed! No archive loaded");
                            goto Error;
                        }
                        if (!File.Exists(Params[1]) && !Directory.Exists(Params[1]))
                        {
                            ErrorMessage = string.Format("File or Directory {0} could not be found", Params[1]);
                            goto Error;
                        }
                        if (CurrentArchive.ItemExists(Params[1]))
                        {
                            if (Params[0].Equals("replace"))
                            {
                                CurrentArchive[Params[1]] = null;
                            }
                            else
                            {
                                ErrorMessage = string.Format("An item already exists at {0}", Params[1]);
                                goto Error;
                            }
                        }
                        if (File.GetAttributes(Params[1]) == FileAttributes.Directory)
                        {
                            if (CurrentArchive is RARC rrr)
                                CurrentArchive[Params[2]] = new RARC.Directory(Params[1], rrr);
                            else
                                CurrentArchive[Params[2]] = new ArchiveDirectory(Params[1], CurrentArchive);
                            Console.WriteLine("Folder {0} {1} successfully", Params[1], func);
                        }
                        else
                        {
                            CurrentArchive[Params[2]] = new RARC.File(Params[1]);
                            Console.WriteLine("File {0} {1} successfully", Params[1], func);
                        }
                        break;

                    case "delete": //delete <archivepath>
                        if (Params.Length < 2)
                        {
                            ErrorMessage = string.Format("Incomplete Syntax - Expected\ndelete <archivepath>");
                            goto Error;
                        }
                        if (CurrentArchive is null)
                        {
                            ErrorMessage = string.Format("Delete failed! No archive loaded");
                            goto Error;
                        }
                        if (!CurrentArchive.ItemExists(Params[1]))
                        {
                            ErrorMessage = string.Format("Can't delete the non-existant item {0}", Params[1]);
                            goto Error;
                        }

                        CurrentArchive[Params[1]] = null;
                        Console.WriteLine("Deleted {0} successfully", Params[1]);
                        break;

                    case "rename":
                    case "move": //move <archivepath> <archivepath>
                        func = Params[0].Equals("rename") ? "Renamed" : "Moved";
                        if (Params.Length < 3)
                        {
                            ErrorMessage = string.Format("Incomplete Syntax - Expected\n{0} <archivepath> <archivepath>", Params[0]);
                            goto Error;
                        }
                        if (CurrentArchive is null)
                        {
                            ErrorMessage = string.Format("{0} failed! No archive loaded", Params[0]);
                            goto Error;
                        }
                        if (!CurrentArchive.ItemExists(Params[1]))
                        {
                            ErrorMessage = string.Format("Can't {1} a non-existant item {0}", Params[1], func);
                            goto Error;
                        }
                        if (CurrentArchive.ItemExists(Params[2]))
                        {
                            ErrorMessage = string.Format("An item already exists at {0}", Params[1]);
                            goto Error;
                        }

                        CurrentArchive.MoveItem(Params[1], Params[2]);
                        Console.WriteLine("{2} {0} to {1} successfully", Params[1], Params[2], func);
                        break;

                    case "extract": //extract <archivepath> <filepath> [-o]
                        if (Params.Length < 3)
                        {
                            ErrorMessage = string.Format("Incomplete Syntax - Expected\nextract <archivepath> <filepath>");
                            goto Error;
                        }
                        if (CurrentArchive is null)
                        {
                            ErrorMessage = string.Format("Extract failed! No archive loaded");
                            goto Error;
                        }
                        if (!CurrentArchive.ItemExists(Params[1]))
                        {
                            ErrorMessage = string.Format("Can't extract the non-existant item {0}", Params[1]);
                            goto Error;
                        }
                        if (CurrentArchive[Params[1]] is RARC.File efile)
                        {
                            if (File.Exists(Params[2]) && !Params.Contains("-o"))
                            {
                                ErrorMessage = string.Format("There is already a file on your system at {0}, consider adding the -o parameter if you want to overwrite it", Params[2]);
                                goto Error;
                            }
                            efile.Save(Params[2]);
                        }
                        else if (CurrentArchive[Params[1]] is RARC.Directory edir)
                        {
                            if (Directory.Exists(Params[2]) && !Params.Contains("-o"))
                            {
                                ErrorMessage = string.Format("There is already a file on your system at {0}, consider adding the -o parameter if you want to overwrite it", Params[2]);
                                goto Error;
                            }
                            edir.Export(Params[2]);
                        }
                        break;
                    case "edit": //edit <archivepath> <Parameter[]>
                        if (Params.Length < 3)
                        {
                            ErrorMessage = string.Format("Incomplete Syntax - Expected\nedit <archivepath> <parameter[]>\nParameters include:\n-id <new id>\n-setcompressed <-yz>\n-loadmain\n-loadaux\n-loaddvd\n-auto");
                            goto Error;
                        }
                        if (CurrentArchive is null)
                        {
                            ErrorMessage = string.Format("Edit failed! No archive loaded");
                            goto Error;
                        }
                        object getitem = CurrentArchive[Params[1]];
                        if (getitem is RARC.Directory)
                        {
                            ErrorMessage = string.Format("Edit failed! Cannot change properties of folders!");
                            goto Error;
                        }
                        if (getitem is RARC.File file)
                        {
                            bool mram = true, aram = false, dvd = false, compressed = false, compressedyaz0 = false;
                            for (int x = 2; x < Params.Length; x++)
                            {
                                switch (Params[x])
                                {
                                    case "-id":
                                        if (CurrentArchive is RARC r && r.KeepFileIDsSynced)
                                        {
                                            ErrorMessage = string.Format("Edit failed! Cannot change File ID because the Archive is set to Automatically calculate file ID's", Params[x]);
                                            goto Error;
                                        }
                                        else if (CurrentArchive is U8)
                                        {
                                            ErrorMessage = string.Format("Edit failed! Cannot change File ID because U8 archives do not have file Id's", Params[x]);
                                            goto Error;
                                        }
                                        if (short.TryParse(Params[++x], out short newid))
                                            file.ID = newid;
                                        else
                                        {
                                            ErrorMessage = string.Format("Edit failed! Could not parse {0} as a int16 (short)", Params[x]);
                                            goto Error;
                                        }
                                        break;
                                    case "-setcompressed":
                                        if (x + 1 != Params.Length - 1 && Params[x + 1].Equals("-yz"))
                                            compressedyaz0 = true;
                                        compressed = true;
                                        break;
                                    case "-loadmain":
                                        mram = true;
                                        aram = false;
                                        dvd = false;
                                        break;
                                    case "-loadaux":
                                        mram = false;
                                        aram = true;
                                        dvd = false;
                                        break;
                                    case "loaddvd":
                                        mram = false;
                                        aram = false;
                                        dvd = true;
                                        break;
                                    case "auto":
                                        if (file.FileData[0] == 0x59 && file.FileData[1] == 0x61 && file.FileData[2] == 0x7A && file.FileData[3] == 0x30)
                                        {
                                            compressed = true;
                                            compressedyaz0 = true;
                                        }
                                        if (file.Name.Contains(".rel"))
                                        {
                                            mram = false;
                                            aram = true;
                                            dvd = false;
                                        }
                                        else
                                        {
                                            mram = true;
                                            aram = false;
                                            dvd = false;
                                        }
                                        if (CurrentArchive is RARC rr && !rr.KeepFileIDsSynced && file.ID == -1)
                                            file.ID = rr.NextFreeFileID;
                                        break;
                                    default:
                                        ErrorMessage = string.Format("Edit failed! Unknown file property {0}", Params[x]);
                                        goto Error;
                                }
                            }
                            RARC.FileAttribute loadattribute = mram ? RARC.FileAttribute.PRELOAD_TO_MRAM : (aram ? RARC.FileAttribute.PRELOAD_TO_ARAM : (dvd ? RARC.FileAttribute.LOAD_FROM_DVD : 0));
                            file.FileSettings = RARC.FileAttribute.FILE | (compressed ? RARC.FileAttribute.COMPRESSED : 0) | loadattribute | (compressedyaz0 ? RARC.FileAttribute.YAZ0_COMPRESSED : 0);
                        }                        
                        break;
                    default:
                        ErrorMessage = string.Format("Invalid Command {0}", Params[0]);
                        goto Error;
                }
                Console.WriteLine();
            }

            return;

        Error:
            Console.WriteLine(ErrorMessage);
            Thread.Sleep(1000);
            return;
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

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
    }
}