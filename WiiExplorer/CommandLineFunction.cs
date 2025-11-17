using System.ComponentModel;
using System.Globalization;
using Hack.io.Class;
using Hack.io.RARC;
using Hack.io.U8;
using Hack.io.Utility;
using Hack.io.YAY0;
using Hack.io.YAZ0;
using WiiExplorer.Class;
using Hack.io.Interface;


#if WINDOWS10_0_17763_0_OR_GREATER
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
#endif

namespace WiiExplorer;

internal static class CommandLineFunction
{
    public static int Execute(string[] args)
    {
        if (Program.IsGameFileLittleEndian)
            StreamUtil.PushEndianLittle();
        else
            StreamUtil.PushEndianBig();


        // WiiExplorer command line but proper this time!

        // Use US Numbers
        CultureInfo.CurrentCulture = new("", false);

        // Create a unique ID for this process
        Guid GUID = Guid.NewGuid();
        string GUIDString = $"WiiExplorer_{GUID}";

        // The new CMD is just a bunch of commands strung together

        // Each Command Group represents tasks that need to happen, in the order they need to happen in.
        // Command Groups are executed Asyncrounously, which each Command Group Item is executed in order in the stack.
        List<Queue<string[]>> CommandGroups = [];

        // Start each job with -s
        Queue<string[]>? CurrentJob = null;
        const char StartChar = 's';
        for (int i = 0; i < args.Length; i++)
        {
            if (CurrentJob is null)
            {
                if (args[i].Equals($"-{StartChar}"))
                    CurrentJob = [];
                continue;
            }
            else if (args[i].Equals($"-{StartChar}"))
            {
                CommandGroups.Add(CurrentJob);
                CurrentJob = [];
                continue;
            }

            switch (args[i])
            {
                case "-n": // -n <Input Folder Path> <Output Archive Path> <Format> - Creates a new archive from a folder.
                    CurrentJob.Enqueue([args[i], args[++i], args[++i], args[++i]]);
                    break;
                case "-e": // -e <Input File Path> <Encoding Type>                  - Re-Encodes the file with the given encoding. YAZ0, YAZ0Fast, YAY0, NONE. Use NONE to decode.
                    CurrentJob.Enqueue([args[i], args[++i], args[++i]]);
                    break;
                case "-d": // -d <Input Archive Path> <Output Folder Path>          - Exports the archive contents to this folder. The archive root is created inside.
                    CurrentJob.Enqueue([args[i], args[++i], args[++i]]);
                    break;
                case "-p": // -p <Input Archive Path> <Padding>                     - Pads the input file to the provided padding. Can be any number but the most common are 16 and 32.
                    CurrentJob.Enqueue([args[i], args[++i], args[++i]]);
                    break;
                default:
                    break;
            }
        }
        if (CurrentJob is not null)
            CommandGroups.Add(CurrentJob);
        CurrentJob = null;

        List<Task<int>> CommandGroupTasks = [];

#if WINDOWS10_0_17763_0_OR_GREATER
        // Windows 10 only
        // WiiExplorer will give toasts for progress in addition to the console

        ToastContentBuilder TCB = new();
        TCB.AddText("Processing Commands...");
        //TCB.AddCustomTimeStamp(DateTime.Now); // seemingly done automatically...
        NotificationData ND = new();
#endif

        for (int i = 0; i < CommandGroups.Count; i++)
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            TCB.AddVisualChild(new AdaptiveProgressBar()
            {
                Title = new BindableString($"progress_{i}_Title"),
                Value = new BindableProgressBarValue($"progress_{i}_Value"),
                ValueStringOverride = new BindableString($"progress_{i}_ValueString"),
                Status = new BindableString($"progress_{i}_Status")
            });
            ND.Values[$"progress_{i}_Title"] = "Waiting";
            ND.Values[$"progress_{i}_Value"] = "0.0";
            ND.Values[$"progress_{i}_ValueString"] = "0%";
            ND.Values[$"progress_{i}_Status"] = "...";
#endif
        }

#if WINDOWS10_0_17763_0_OR_GREATER
        ToastContent tc = TCB.GetToastContent();
        ToastNotification tn = new(tc.GetXml())
        {
            Tag = GUIDString,
            Data = ND,
            // Do we need a group?
        };

        ToastNotificationManagerCompat.CreateToastNotifier().Show(tn);
#endif

        for (int i = 0; i < CommandGroups.Count; i++)
        {
            var xx = CommandGroups[i];
            int ii = i;
            var t = Task.Run(() => ExecuteCommandGroup(xx, ii, GUIDString));
            if (true)
                t.Wait();
            CommandGroupTasks.Add(t);
        }


        bool IsAllDone;
        int CompleteCount;
        do
        {
            IsAllDone = true;
            CompleteCount = 0;
            for (int i = 0; i < CommandGroupTasks.Count; i++)
            {
                if (!CommandGroupTasks[i].IsCompleted)
                    IsAllDone = false;
                else
                    CompleteCount++;
            }
            //Console.Title = $"{TITLE} ({CompleteCount}/{PreparedFiles.Count})";
        }
        while (!IsAllDone);

        Thread.Sleep(3000);
#if WINDOWS10_0_17763_0_OR_GREATER
        ToastNotificationManagerCompat.History.Clear();
#endif

        return 0; // Success
    }

    private static int ExecuteCommandGroup(Queue<string[]> CommandGroup, int Index, string GUID)
    {
#if WINDOWS10_0_17763_0_OR_GREATER
        NotificationData ND = new();
#endif

        while (CommandGroup.Count > 0)
        {
            string[] args = CommandGroup.Dequeue();
            switch (args[0])
            {
                case "-n":
                    #region New Archive
                    // Validate args
                    if (!Directory.Exists(args[1]))
                        return OnTaskError('n', 1, "Source directory not found.");

                    Archive newarc;
                    switch (args[3].ToLower())
                    {
                        case "rarc":
                            newarc = new RARC();
                            break;
                        case "u8":
                            newarc = new U8();
                            break;
                        case "aaf":
                            newarc = new AAF();
                            break;
                        default:
                            return OnTaskError('n', 2, $"Unknown Archive format \"{args[3]}\"");
                    }

#if WINDOWS10_0_17763_0_OR_GREATER
                    ND.Values[$"progress_{Index}_Title"] = $"Creating \"{new FileInfo(args[2]).Name}\"";
                    ND.Values[$"progress_{Index}_Value"] = $"0.0";
                    ND.Values[$"progress_{Index}_ValueString"] = $"-.--%";
                    ND.Values[$"progress_{Index}_Status"] = "...";
                    ToastNotificationManagerCompat.CreateToastNotifier().Update(ND, GUID);
#endif
                    try
                    {
                        newarc.Import(args[1]);
                        FileUtil.SaveFile(args[2], newarc.Save);
                    }
                    catch (ArgumentException ae)
                    {
                        if (ae.Message.StartsWith("An item with the same key has already been added"))
                            return OnTaskError('n', 3, "Duplicate Key Error");
                        return OnTaskError('n', -1, ae.Message);
                    }
                    catch (Exception e)
                    {
                        Type t = e.GetType();
                        return OnTaskError('n', -1, e.Message);
                    }
                    #endregion
                    break;
                case "-e":
                    #region Encoding
                    // Validate args
                    if (!File.Exists(args[1]))
                        return OnTaskError('e', 1, "Source file not found.");

                    BackgroundWorker worker = new()
                    {
                        WorkerSupportsCancellation = true,
                        WorkerReportsProgress = true
                    };
                    worker.DoWork += (sender, e) =>
                    {
                        try
                        {
                            byte[] data = OpenFileConsiderEncoding(args[1]);
                            byte[] Compressed;
                            switch (args[2].ToUpper())
                            {
                                case "YAZ0_STRONG":
                                    Compressed = YAZ0.Compress_Strong(data, worker);
                                    break;
                                case "YAZ0_FAST":
                                    Compressed = YAZ0.Compress_Fast(data, worker);
                                    break;
                                case "YAZ0_OFFICIAL":
                                    Compressed = YAZ0.Compress_Official(data, worker);
                                    break;
                                case "YAY0_STRONG":
                                    Compressed = YAY0.Compress(data, worker);
                                    break;
                                case "NONE":
                                    Compressed = data;
                                    worker.ReportProgress(100);
                                    break;
                                default:
                                    throw new AbandonedMutexException(); // lol
                            }
                            if (Compressed.Length > 0)
                                File.WriteAllBytes(args[1], Compressed);
                            else
                                e.Cancel = true;
                        }
                        catch (Exception ex)
                        {
                            e.Result = ex;
                            e.Cancel = true;
                        }
                        
                    };
                    worker.ProgressChanged += (sender, e) =>
                    {
#if WINDOWS10_0_17763_0_OR_GREATER
                        ND.Values[$"progress_{Index}_Title"] = $"Encoding \"{new FileInfo(args[1]).Name}\"";
                        ND.Values[$"progress_{Index}_Value"] = $"{MathUtil.GetPercentOf(e.ProgressPercentage, 100, 1):0.0}";
                        ND.Values[$"progress_{Index}_ValueString"] = $"{e.ProgressPercentage:00}%";
                        ND.Values[$"progress_{Index}_Status"] = $"{args[2]} Encoding...";
                        ToastNotificationManagerCompat.CreateToastNotifier().Update(ND, GUID);
#endif
                    };

                    worker.RunWorkerAsync();
                    while (worker.IsBusy)
                    {
                        // wait
                    }
                    // how to handle errors?
                    #endregion
                    break;
                case "-d": // -d <Input Archive Path> <Output Folder Path>      - Exports the archive contents to this folder. The archive root is created inside.
                    #region Exporting
                    if (!File.Exists(args[1]))
                        return OnTaskError('d', 1, "Source file not found.");

                    if (OpenArchive(args[1]) is not Archive arc)
                        return OnTaskError('d', 2, $"Unsupported archive \"{args[1]}\"");

                    if (arc.Root is null)
                        return OnTaskError('d', 3, $"Archive has no Root.");

                    string name = arc.Root.Name;
                    if (string.IsNullOrWhiteSpace(name))
                        name = "NO_NAME";

                    string path = Path.Combine(args[2], name);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

#if WINDOWS10_0_17763_0_OR_GREATER
                    ND.Values[$"progress_{Index}_Title"] = $"Exporting all files in \"{new FileInfo(args[1]).Name}\"";
                    ND.Values[$"progress_{Index}_Value"] = $"0.0";
                    ND.Values[$"progress_{Index}_ValueString"] = $"-.--%";
                    ND.Values[$"progress_{Index}_Status"] = "...";
                    ToastNotificationManagerCompat.CreateToastNotifier().Update(ND, GUID);
#endif

                    arc.Root.Export(path);
                    #endregion
                    break;
                case "-p": // -p <Input Archive Path> <Padding>                 - Pads the input file to the provided padding. Can be any number but the most common are 16 and 32.
                    #region Padding
                    // Validate args
                    if (!File.Exists(args[1]))
                        return OnTaskError('e', 1, "Source file not found.");

                    int padto = 0;
                    if (!int.TryParse(args[2], out padto))
                        return OnTaskError('e', 2, $"Failed to interpret {args[2]} as a number.");

#if WINDOWS10_0_17763_0_OR_GREATER
                    ND.Values[$"progress_{Index}_Title"] = $"Padding \"{new FileInfo(args[1]).Name}\"";
                    ND.Values[$"progress_{Index}_Value"] = $"0.0";
                    ND.Values[$"progress_{Index}_ValueString"] = $"-.--%";
                    ND.Values[$"progress_{Index}_Status"] = "...";
                    ToastNotificationManagerCompat.CreateToastNotifier().Update(ND, GUID);
#endif
                    ArcUtil.PadFile(args[1], padto);
                    #endregion
                    break;
                default:
                    break;
            }
        }

#if WINDOWS10_0_17763_0_OR_GREATER
        ND.Values[$"progress_{Index}_Title"] = "Task Completed Successfully";
        ND.Values[$"progress_{Index}_Value"] = $"1.0";
        ND.Values[$"progress_{Index}_ValueString"] = $"100%";
        ND.Values[$"progress_{Index}_Status"] = "No Errors Occurred";
        ToastNotificationManagerCompat.CreateToastNotifier().Update(ND, GUID);
#endif
        return 0;


        Archive? OpenArchive(string Filepath)
        {
            List<(Func<Stream, bool> CheckFunc, Func<byte[], byte[]> DecodeFunction)> DecompFuncs =
            [
                (YAZ0.Check, YAZ0.Decompress),
                (YAY0.Check, YAY0.Decompress)
            ];

            Archive? loadarc = null;
            int encodingselection = -1;
            loadarc ??= TryReadFormat<RARC>();
            loadarc ??= TryReadFormat<U8>();
            loadarc ??= TryReadFormat<AAF>();
            return loadarc;

            T? TryReadFormat<T>() where T : class, ILoadSaveFile, new()
            {
                try
                {
                    T a = new();
                    encodingselection = FileUtil.LoadFileWithDecompression(Filepath, a.Load, [.. DecompFuncs]);
                    if (encodingselection == -1)
                        FileUtil.LoadFile(Filepath, a.Load);
                    return a;
                }
                catch (BadImageFormatException)
                {
                    // Thrown by bad magic exception
                    return null;
                }
                catch (Exception)
                {
                    //MessageBox.Show($"Failed to open the archive as {new T().GetType()}:\n{ex.Message}\n\n{ex.StackTrace ?? ""}", "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        byte[] OpenFileConsiderEncoding(string Filepath)
        {
            List<(Func<Stream, bool> CheckFunc, Func<byte[], byte[]> DecodeFunction)> DecompFuncs =
            [
                (YAZ0.Check, YAZ0.Decompress),
                (YAY0.Check, YAY0.Decompress)
            ];
            byte[]? d = FileUtil.ReadWithDecompression(Filepath, [.. DecompFuncs]);
            if (d is not null)
                return d;
            return File.ReadAllBytes(Filepath);
        }

        int OnTaskError(char id, int code, string msg)
        {
            int c = id << 16 | (code & 0x00FFFFFF);
#if WINDOWS10_0_17763_0_OR_GREATER
            NotificationData ND = new();
            ND.Values[$"progress_{Index}_Title"] = "Task Encountered an Error";
            ND.Values[$"progress_{Index}_Status"] = $"0x{c:X8} : {msg}";
            ToastNotificationManagerCompat.CreateToastNotifier().Update(ND, GUID);
#endif

            return c;
        }
    }
}
