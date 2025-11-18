using DarkModeForms;
using System.Globalization;

namespace WiiExplorer;

internal static class Program
{
    public static string SETTINGS_PATH => GetFromAppPath("Settings.usrs");
    public static string USER_COMPRESSION_PATH => GetFromAppPath("Compression.txt");
    public const string UPDATEALERT_URL = "https://raw.githubusercontent.com/SuperHackio/WiiExplorer/master/WiiExplorer/UpdateAlert.txt";
    public const string GIT_RELEASE_URL = "https://github.com/SuperHackio/WiiExplorer/releases";
    public static readonly string SUPPORTED_ARCHIVES_FILTER = Properties.Resources.FileFilter_SupportedArchives;

    public static Settings Settings { get; private set; } = new(SETTINGS_PATH);
    public static bool IsGameFileLittleEndian { get; set; } = false;
    public static bool IsUnsavedChanges { get; set; } = false;
    private static MainForm? Editor;
    public static UpdateInformation? UpdateInfo;
    

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static int Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        CultureInfo culture = new("de");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        UpdateInfo = UpdateInformation.IsUpdateExist(UPDATEALERT_URL);
        if (UpdateInfo is not null && UpdateInfo.Value.IsNewer() && UpdateInfo.Value.IsUpdateRequired)
        {
            MessageBox.Show(string.Format(Properties.Resources.MessageBoxMsg_MandatoryUpdateNotice, GIT_RELEASE_URL, UpdateInfo.ToString()), Properties.Resources.MessageBoxTitle_UpdateRequired, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -5;
        }

        if (args.Any(A => A.Equals("-le")))
        {
            IsGameFileLittleEndian = true;
            args = [.. args.Where(A => !A.Equals("-le"))];
        }

        if (args.Length > 1) //If it's greater than 1 even after removing the -le arg, do cmd stuff
        {
            if (UpdateInfo is not null && UpdateInfo.Value.IsNewer())
                Console.WriteLine(Properties.Resources.MessageBoxMsg_OptionalUpdateNotice, GIT_RELEASE_URL, UpdateInfo.ToString());

            return CommandLineFunction.Execute(args);
        }

        if (UpdateInfo is not null && UpdateInfo.Value.IsNewer()) // Don't need to check if it's required, as the program will end execution early if so.
            MessageBox.Show(string.Format(Properties.Resources.MessageBoxMsg_OptionalUpdateNotice, GIT_RELEASE_URL, UpdateInfo.ToString()), Properties.Resources.MessageBoxTitle_UpdateAvailable, MessageBoxButtons.OK, MessageBoxIcon.Information);

        Editor = new(args);
        ReloadTheme();
        Application.Run(Editor);
        return 0;
    }

    public static void ReloadTheme()
    {
        if (Editor is null)
            return;
        Editor.SuspendLayout();
        ProgramColors.ReloadTheme(Editor);
        Editor.ResumeLayout();
    }
    public static string GetFromAppPath(string Target) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Target);
}