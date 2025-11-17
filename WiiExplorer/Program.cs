using DarkModeForms;

namespace WiiExplorer;

internal static class Program
{
    public static string SETTINGS_PATH => GetFromAppPath("Settings.usrs");
    public static string USER_COMPRESSION_PATH => GetFromAppPath("Compression.txt");
    public const string UPDATEALERT_URL = "https://raw.githubusercontent.com/SuperHackio/WiiExplorer/master/WiiExplorer/UpdateAlert.txt";
    public const string GIT_RELEASE_URL = "https://github.com/SuperHackio/WiiExplorer/releases";
    public const string SUPPORTED_ARCHIVES_FILTER = "All Supported Files|*.arc;*.szs;*.szp;*.carc;*.app;*.aaf|Resource Archive|*.arc|YAZ0 Identified Archive|*.szs|YAY0 Identified Archive|*.szp|U8 Archive|*.szs;*.carc;*.app|Audio Archive File|*.szs;*.aaf|All Files|*.*";

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

        UpdateInfo = UpdateInformation.IsUpdateExist(UPDATEALERT_URL);
        if (UpdateInfo is not null && UpdateInfo.Value.IsNewer())
        {
            if (UpdateInfo.Value.IsUpdateRequired)
            {
                MessageBox.Show($"An update for WiiExplorer is available. To continue using WiiExplorer, please update to the latest version.\n\n{GIT_RELEASE_URL}", "Update Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -5;
            }
        }

        if (args.Any(A => A.Equals("-le")))
        {
            IsGameFileLittleEndian = true;
            args = [.. args.Where(A => !A.Equals("-le"))];
        }

        if (args.Length > 1) //If it's greater than 1 even after removing the -le arg, do cmd stuff
            return CommandLineFunction.Execute(args);

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