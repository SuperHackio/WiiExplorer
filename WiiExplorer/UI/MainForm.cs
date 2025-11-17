using DarkModeForms;
using Hack.io.Class;
using Hack.io.Interface;
using Hack.io.RARC;
using Hack.io.U8;
using Hack.io.Utility;
using Hack.io.YAY0;
using Hack.io.YAZ0;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WiiExplorer.Class;

namespace WiiExplorer;

public partial class MainForm : Form
{
    private readonly OpenFileDialog ArchiveOFD = new() { Filter = Program.SUPPORTED_ARCHIVES_FILTER };
    private readonly SaveFileDialog ArchiveSFD = new() { Filter = Program.SUPPORTED_ARCHIVES_FILTER };
    private readonly OpenFileDialog FileOFD = new();
    private readonly SaveFileDialog FileSFD = new();
    private readonly FolderBrowserDialog FBD = new();

    private Archive? Archive;
    private static string[] KnownExtensions = [];
    private static readonly List<(string LocalizeName, Func<byte[], BackgroundWorker?, byte[]>? Function)> BuiltInCompressions =
    [
        ("Off", null),

        ("Yaz0 Strong", (src, bgw) => YAZ0.Compress_Strong(src, bgw)),
        ("Yaz0 Fast", (src, bgw) => YAZ0.Compress_Fast(src, bgw)),
        ("Yaz0 Official", (src, bgw) => YAZ0.Compress_Official(src, bgw)),

        ("Yay0 Strong", YAY0.Compress), // Love how this one fits without a lambda but the others don't lol
    ];
    private static readonly List<(string LocalizeName, string CommandFormat)> UserCompressions = [];

    // For the tree view dragging
    private string? NodeMap;
    private const int MAPSIZE = 128;
    private StringBuilder NewNodeMap = new(MAPSIZE);
    private static SolidBrush ArchiveTreeViewBrush => new(ProgramColors.TextColor);
    private int DragHeldCounter = 0;
    private Point LastDragPos = new(0, 0);


    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value + " " + Archive switch
        {
            null => "",
            U8 => "(U8)",
            RARC => "(RARC)",
            AAF => "(AAF)",
            _ => "<UNKNOWN>"
        };
    }


    public MainForm(string[] args)
    {
        if (Program.IsGameFileLittleEndian)
            StreamUtil.PushEndianLittle();
        else
            StreamUtil.PushEndianBig();

        InitializeComponent();
        CenterToScreen();
        ReloadUserCompressions();

        Yaz0ToolStripComboBox.ComboBox.SetDoubleBuffered();
        ThemeToolStripComboBox.ComboBox.SetDoubleBuffered();
        PaddingToolStripColorComboBox.ComboBox.SetDoubleBuffered();
        AutoYaz0ToolStripColorComboBox.ComboBox.SetDoubleBuffered();
        ArchiveTreeView.SetDoubleBuffered();

        if (Program.Settings.CompressionIndex >= Yaz0ToolStripComboBox.Items.Count)
            Program.Settings.CompressionIndex = 0; // Reset it

        Yaz0ToolStripComboBox.SelectedIndex = Program.Settings.CompressionIndex;
        PaddingToolStripColorComboBox.SelectedIndex = Program.Settings.PaddingIndex;
        AutoYaz0ToolStripColorComboBox.SelectedIndex = Program.Settings.AutoHandleCompression ? 1 : 0;

        Text = "WiiExplorer - " + UpdateInformation.ApplicationVersion;
        ThemeToolStripComboBox.SelectedIndex = Program.Settings.ThemeIndex; //Default to Light for the funny
        ProgramColors.IsDarkMode = ThemeToolStripComboBox.SelectedIndex == 1;

        ReloadTreeViewIcons();

        if (args.Length != 0)
            Open(args[0]);
    }

    private void SetControlsEnabled(bool toggle = true, bool affectall = false)
    {
        EditToolStripMenuItem.Enabled = toggle;
        AddFileToolStripMenuItem.Enabled = toggle;
        AddFolderToolStripMenuItem.Enabled = ContextAddFolderToolStripMenuItem.Enabled = TryEnableControl(HasFolders);
        DeleteSelectedToolStripMenuItem.Enabled = toggle;
        RenameSelectedToolStripMenuItem.Enabled = toggle;
        ExportSelectedToolStripMenuItem.Enabled = toggle;
        ExportAllToolStripMenuItem.Enabled = toggle;
        ReplaceSelectedToolStripMenuItem.Enabled = toggle;
        CreateEmptyFolderToolStripMenuItem.Enabled = ContextCreateEmptyFolderToolStripMenuItem.Enabled = TryEnableControl(HasFolders);
        ArchiveTreeView.Enabled = toggle;
        RootNameTextBox.Enabled = TryEnableControl(HasRoot);
        KeepIDsSyncedCheckBox.Enabled = TryEnableControl(IsRARC);
        SaveToolStripMenuItem.Enabled = toggle;
        SaveAsToolStripMenuItem.Enabled = toggle;

        if (affectall)
        {
            FileToolStripMenuItem.Enabled = toggle;
            NewToolStripMenuItem.Enabled = toggle;
            NewFromFolderToolStripMenuItem.Enabled = toggle;
            OpenToolStripMenuItem.Enabled = toggle;
            Yaz0ToolStripComboBox.Enabled = toggle;
        }

        // allowance allows (lol) the optional disabling of specific items based on archive type.
        bool TryEnableControl(Predicate<Archive?>? allowance = null)
        {
            if (!toggle)
                return toggle;

            if (allowance is null)
                return toggle;

            return allowance(Archive);
        }

        static bool IsRARC(Archive? a) => a is RARC;
        //static bool IsU8(Archive? a) => a is U8;
        static bool HasRoot(Archive? a) => a is RARC or U8;
        static bool HasFolders(Archive? a) => a is RARC or U8;
    }

    private void ReloadTreeViewIcons()
    {
        ImageList oldList = ArchiveTreeView.ImageList;

        (ImageList newlist, string[] Extensions) = ArcUtil.CreateKnownExtensionList();
        ArchiveTreeView.ImageList = newlist;
        oldList?.Dispose();

        KnownExtensions = Extensions;
        ArcUtil.RefreshTreeViewIcons(ArchiveTreeView.Nodes, ArchiveTreeView.ImageList);
    }

    private void ReloadTreeViewNodes()
    {
        ArchiveTreeView.SuspendLayout();
        ArchiveTreeView.Nodes.Clear();
        if (Archive is not null)
            ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(ArchiveTreeView.ImageList));
        ArchiveTreeView.ResumeLayout();
    }

    private void ReloadUserCompressions()
    {
        UserCompressions.Clear();
        Yaz0ToolStripComboBox.Items.Clear();
        for (int i = 0; i < BuiltInCompressions.Count; i++)
            Yaz0ToolStripComboBox.Items.Add(BuiltInCompressions[i].LocalizeName);


        if (!File.Exists(Program.USER_COMPRESSION_PATH))
            return; // No User compressions
        string[] UserCompression = File.ReadAllLines(Program.USER_COMPRESSION_PATH);
        foreach (string Compression in UserCompression)
        {
            if (string.IsNullOrWhiteSpace(Compression) || Compression.StartsWith('#'))
                continue;

            string[] comp = Compression.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (comp.Length < 2 || comp[0].Length <= 0 || comp[1].Length <= 0)
                continue;

            UserCompressions.Add((comp[0], comp[1]));
        }

        for (int i = 0; i < UserCompressions.Count; i++)
            Yaz0ToolStripComboBox.Items.Add(UserCompressions[i].LocalizeName);
    }

    private void Open(string Filepath)
    {
        ArchiveOFD.FileName = Filepath;
        MainToolStripProgressBar.Value = 0;

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

        if (!InitializeArchive(loadarc, "The file you tried to open is not supported."))
            return;

        if (loadarc is not null)
            loadarc.FileName = Filepath;
        Text = $"WiiExplorer {UpdateInformation.ApplicationVersion} - {new FileInfo(Filepath).Name}";

        if (AutoYaz0ToolStripColorComboBox.SelectedIndex > 0)
            Yaz0ToolStripComboBox.SelectedIndex = encodingselection switch
            {
                0 => 1,
                1 => 3,
                _ => 0,
            };


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
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open the archive as {new T().GetType()}:\n{ex.Message}\n\n{ex.StackTrace ?? ""}", "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

    private void Save(string Filepath)
    {
        if (Archive is null)
            return; //should be impossible...

        FileInfo fi = new(Filepath);
        int prevencoding = Yaz0ToolStripComboBox.SelectedIndex;

        if (AutoYaz0ToolStripColorComboBox.SelectedIndex > 0)
        {
            // TODO: Messagebox Messages
            if (fi.Extension.Equals(".szp") && Yaz0ToolStripComboBox.SelectedIndex != 0x03 && MessageBox.Show("TODO", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Yaz0ToolStripComboBox.SelectedIndex = 0x03;
            else if (fi.Extension.Equals(".szs") && Yaz0ToolStripComboBox.SelectedIndex != 0x01 && Yaz0ToolStripComboBox.SelectedIndex != 0x02 && MessageBox.Show("TODO", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Yaz0ToolStripComboBox.SelectedIndex = 0x01;
        }

        MainToolStripProgressBar.Value = 0;
        Archive.FromTreeNode(ArchiveTreeView);
        MainToolStripProgressBar.Value = 70;
        FileUtil.SaveFile(Filepath, Archive.Save);
        Archive.FileName = Filepath;
        Text = $"WiiExplorer {Application.ProductVersion} - {fi.Name}";

        if (PaddingToolStripColorComboBox.SelectedIndex == 1 || PaddingToolStripColorComboBox.SelectedIndex == 3)
            ArcUtil.PadFile(Filepath, PaddingToolStripColorComboBox.SelectedIndex == 3 ? 32 : 16);

        if (Yaz0ToolStripComboBox.SelectedIndex != 0)
        {
            EncodingBackgroundWorker.RunWorkerAsync((Filepath, Yaz0ToolStripComboBox.SelectedIndex, PaddingToolStripColorComboBox.SelectedIndex));
            MainToolStripProgressBar.Value = 0;
        }
        else
            MainToolStripStatusLabel.Text = "Save Complete!";

        SetControlsEnabled(false, true);
        FilePropertiesToolStripMenuItem.Enabled = false;

        while (EncodingBackgroundWorker.IsBusy)
        {
            Application.DoEvents();
        }

        Program.IsUnsavedChanges = false;
        MainToolStripProgressBar.Value = 100;
        SetControlsEnabled(affectall: true);

        Yaz0ToolStripComboBox.SelectedIndex = prevencoding;
    }

    private bool NewArchive(string? SourceFolder)
    {
        string FailureMessage = "Failed to create new archive.";

        NewArchiveForm NAF = new();
        if (NAF.ShowDialog() != DialogResult.OK)
            return false;
        Archive? arc = NAF.GetNewArchive();
        if (arc is null)
        {
            MessageBox.Show(FailureMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (SourceFolder is not null)
            arc.Import(SourceFolder);

        if (!InitializeArchive(arc, FailureMessage))
            return false;
        Text = $"WiiExplorer {Application.ProductVersion} - New Archive";
        return true;
    }

    private bool InitializeArchive(Archive? archive, string FailureMessage)
    {
        if (archive is null)
        {
            MessageBox.Show(FailureMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (archive.Root is null)
        {
            MessageBox.Show("If you are seeing this, Congratulations!\nI have no idea how you made this happen...\n\nArchive has no Root", "Bruh moment?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        Archive = archive;

        ReloadTreeViewNodes();
        RootNameTextBox.Text = Archive.Root.Name;

        KeepIDsSyncedCheckBox.Checked = Archive is not RARC r || r.KeepFileIDsSynced; // Interesting...

        Program.IsUnsavedChanges = false;
        SetControlsEnabled();
        MainToolStripProgressBar.Value = 100;

        int Count = Archive.TotalFileCount; // Do it here so we don't need to do it twice, as that would be taxing for large archives
        MainToolStripStatusLabel.Text = string.Format("Archive loaded successfully! [{0} file(s) total]", Count);
        FilePropertiesToolStripMenuItem.Enabled = false;
        DeleteSelectedToolStripMenuItem.Enabled = ContextDeleteSelectedToolStripMenuItem.Enabled = false;
        RenameSelectedToolStripMenuItem.Enabled = ContextRenameToolStripMenuItem.Enabled = false;
        ReplaceSelectedToolStripMenuItem.Enabled = ContextReplaceSelectedToolStripMenuItem.Enabled = false;
        ExportSelectedToolStripMenuItem.Enabled = ContextExportSelectedToolStripMenuItem.Enabled = false;
        return true;
    }

    private void InsertItemsToArchive(string[] DrivePaths, TreeNode? PlacementTreeNode, bool Before = false, bool NoInsertDir = false)
    {
        if (Archive is null || Archive.Root is null) // Second case should be impossible
            return;

        for (int i = 0; i < DrivePaths.Length; i++)
        {
            string Current = DrivePaths[i];

            if (!File.Exists(Current) && !Directory.Exists(Current))
                continue;

            FileAttributes attr = File.GetAttributes(Current);
            string ogname;
            int imgidx;
            dynamic? NewItem;
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                ogname = new DirectoryInfo(Current).Name;
                imgidx = ArcUtil.IndexOfInternalImageOrDefault(ArcUtil.INTERNAL_FOLDERCLOSE, ArchiveTreeView.ImageList);
                NewItem = ArcUtil.CreateNewDirectory(Archive);
            }
            else
            {
                FileInfo fi = new(Current);
                ogname = fi.Name;
                imgidx = ArcUtil.IndexOfExtensionImageOrDefault(fi.Extension, ArchiveTreeView.ImageList);
                NewItem = ArcUtil.CreateNewFile(Archive);
            }
            if (NewItem is null)
                continue;
            if (NewItem is ArchiveFile AF)
                FileUtil.LoadFile(Current, AF.Load);
            if (NewItem is ArchiveDirectory AD)
                AD.CreateFromFolder(Current, Archive);

            TreeNode NewTreeNode = new(ogname) { ImageIndex = imgidx, SelectedImageIndex = imgidx };
            InsertItemToTreeView(PlacementTreeNode, NewTreeNode, NewItem, Before ? 0 : i, Before, NoInsertDir);
        }
    }

    private void InsertItemToTreeView(TreeNode? PlacementTreeNode, TreeNode NewTreeNode, dynamic ArchiveElement, int Offset = 0, bool Before = false, bool NoInsertDir = false)
    {
        if (Archive is null)
            return; // Cannot add while no archive is active

        if (!Before)
            Offset++;
        // If no target node is provided, just place things on the root at the bottom.
        if (PlacementTreeNode is null)
            ArchiveTreeView.Nodes.Add(NewTreeNode);
        else
        {
            // If the currently selected node is that of an ArchiveDirectory, add the new items at the bottom of that directory
            if (Archive[PlacementTreeNode.FullPath] is ArchiveDirectory && !NoInsertDir)
                PlacementTreeNode.Nodes.Add(NewTreeNode);
            else if (PlacementTreeNode.Parent is null)
                ArchiveTreeView.Nodes.Insert(PlacementTreeNode.Index + Offset, NewTreeNode);
            else
                PlacementTreeNode.Parent.Nodes.Insert(PlacementTreeNode.Index + Offset, NewTreeNode);
        }

        UpdateTreeViewNodeWithUniqueName(NewTreeNode, ArchiveElement);

        if (ArchiveElement is ArchiveDirectory d)
            NewTreeNode.Nodes.AddRange(d.ToTreeNode(ArchiveTreeView.ImageList));

        Archive[NewTreeNode.FullPath] = ArchiveElement;

        Program.IsUnsavedChanges = true;
    }

    private void UpdateTreeViewNodeWithUniqueName(TreeNode NewTreeNode, dynamic? ArchiveElement)
    {
        if (Archive is null)
            return;
        string pre = Path.GetFileNameWithoutExtension(NewTreeNode.Text);
        string post = new FileInfo(NewTreeNode.Text).Extension;
        int no = 2;
        while (Archive.ItemExists(NewTreeNode.FullPath))
            NewTreeNode.Text = pre + $" ({no++})" + post;

        try
        {
            if (ArchiveElement is not null)
                ArchiveElement.Name = NewTreeNode.Text;
        }
        catch (Exception)
        {
        }
    }

    private void ExportDirectory(string ArchivePath, string OutputPath)
    {
        if (Archive is null || Archive[ArchivePath] is not ArchiveDirectory AD)
            return; // TODO: Error message? Is this even possible?

        string name = AD.Name;
        if (string.IsNullOrWhiteSpace(name))
            name = "NO_NAME";

        string path = Path.Combine(OutputPath, name);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        else
        {
            if (MessageBox.Show($"The output directory already exists!\n\"{path}\"\nSome of the contents may be overwritten.\n\nProceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
        }
        AD.Export(path);
    }

    private void ExportFile(string ArchivePath, string OutputPath)
    {
        if (Archive is null || Archive[ArchivePath] is not ArchiveFile AF)
            return; // TODO: Error message? Is this even possible?

        // Don't bother checking if Output exists, the file dialogue would've done that already
        FileUtil.SaveFile(OutputPath, AF.Save);
    }

    private static string GetKnownExtensionFilter(string Extension)
    {
        if (Extension.Length == 0)
            return KnownExtensions[0];
        for (int i = 0; i < KnownExtensions.Length; i++)
        {
            string[] Splitter = KnownExtensions[i].Split('|');
            if (Splitter.Length != 2 || Splitter[1].Length == 0)
                continue;
            if (Extension.Equals(Splitter[1][1..]))
                return KnownExtensions[i];
        }

        return "All Files|*.*";
    }

    #region Form
    private void RootNameTextBox_TextChanged(object sender, EventArgs e)
    {
        if (Archive is null || Archive.Root is null)
            return;
        Archive.Root.Name = RootNameTextBox.Text;
        Program.IsUnsavedChanges = true;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (EncodingBackgroundWorker.IsBusy)
        {
            if (MessageBox.Show("Cancel Compresion?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                EncodingBackgroundWorker.CancelAsync();
            e.Cancel = true;
            return;
        }
        if (e.CloseReason == CloseReason.UserClosing && Program.IsUnsavedChanges && !IsDiscardChanges())
            e.Cancel = true;
    }
    #endregion

    #region Toolstrip
    private void NewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Program.IsUnsavedChanges && !IsDiscardChanges())
            return;

        NewArchive(null);
    }

    private void NewFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Program.IsUnsavedChanges && !IsDiscardChanges())
            return;

        if (FBD.ShowDialog() == DialogResult.OK && Path.Exists(FBD.SelectedPath))
            NewArchive(FBD.SelectedPath);
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Program.IsUnsavedChanges && !IsDiscardChanges())
            return;

        //TODO: Set this
        //ArchiveOFD.InitialDirectory;
        if (ArchiveOFD.ShowDialog() == DialogResult.OK)
            Open(ArchiveOFD.FileName);
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || string.IsNullOrWhiteSpace(Archive.FileName))
        {
            //ArchiveSFD.InitialDirectory = Settings.Default.PreviousSaveArchivePath;
            if (ArchiveSFD.ShowDialog() == DialogResult.OK && ArchiveSFD.FileName != "")
                Save(ArchiveSFD.FileName);
            else
                return;
        }
        else
            Save(Archive.FileName);
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //ArchiveSFD.InitialDirectory = Settings.Default.PreviousSaveArchivePath;
        if (ArchiveSFD.ShowDialog() == DialogResult.OK && ArchiveSFD.FileName != "")
            Save(ArchiveSFD.FileName);
    }


    private void AddFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FileOFD.Multiselect = true;
        if (FileOFD.ShowDialog() == DialogResult.OK)
            InsertItemsToArchive(FileOFD.FileNames, ArchiveTreeView.SelectedNode);
    }

    private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (FBD.ShowDialog() == DialogResult.OK)
            InsertItemsToArchive([FBD.SelectedPath], ArchiveTreeView.SelectedNode);
    }

    private void CreateEmptyFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || Archive.Root is null) // Second case should be impossible
            return;
        dynamic? NewDir = ArcUtil.CreateNewDirectory(Archive);
        if (NewDir is null)
            return;
        int imgidx = ArcUtil.IndexOfInternalImageOrDefault(ArcUtil.INTERNAL_FOLDERCLOSE, ArchiveTreeView.ImageList);
        TreeNode NewTreeNode = new(NewDir.Name) { ImageIndex = imgidx, SelectedImageIndex = imgidx };
        InsertItemToTreeView(ArchiveTreeView.SelectedNode, NewTreeNode, NewDir);
        ArchiveTreeView.SelectedNode = NewTreeNode;
        RenameSelectedToolStripMenuItem_Click(sender, e);
    }

    private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || ArchiveTreeView.SelectedNode is null)
            return;

        Archive[ArchiveTreeView.SelectedNode.FullPath] = null;
        ArchiveTreeView.SelectedNode.Remove();
        Program.IsUnsavedChanges = true;
    }

    private void RenameSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || ArchiveTreeView.SelectedNode is null)
            return;

        string tmp = ArchiveTreeView.SelectedNode.Text;
        RenameForm RN = new(ArchiveTreeView, Archive);

        if (RN.ShowDialog() != DialogResult.OK)
            return;

        MainToolStripStatusLabel.Text = $"Renamed \"{tmp}\" to \"{ArchiveTreeView.SelectedNode.Text}\"";
    }

    private void ReplaceSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || ArchiveTreeView.SelectedNode is null)
            return;

        TreeNode Working = ArchiveTreeView.SelectedNode;
        string OldPath = Working.FullPath;
        string OldName = Working.Text;
        dynamic? Item = Archive[OldPath];
        if (Item is ArchiveDirectory)
        {
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo info = new(FBD.SelectedPath);
                Working.Text = info.Name;
                Archive[OldPath] = null; // Delete the item in the archive
                ArchiveDirectory? NewItem = ArcUtil.CreateNewDirectory(Archive) ?? throw new InvalidOperationException("This should be impossible");
                NewItem.CreateFromFolder(FBD.SelectedPath, Archive);
                UpdateTreeViewNodeWithUniqueName(Working, NewItem);
                Archive[Working.FullPath] = NewItem;

                Working.Nodes.Clear();
                Working.Nodes.AddRange(NewItem.ToTreeNode(ArchiveTreeView.ImageList));
            }
        }
        else if (Item is ArchiveFile file)
        {
            FileOFD.Multiselect = false;
            if (FileOFD.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(FileOFD.FileName))
            {
                FileInfo info = new(FileOFD.FileName);
                Working.Text = info.Name;
                Archive[OldPath] = null; // Delete the item in the archive
                ArchiveFile? NewItem = ArcUtil.CreateNewFile(Archive) ?? throw new InvalidOperationException("This should be impossible");
                FileUtil.LoadFile(FileOFD.FileName, NewItem.Load);
                UpdateTreeViewNodeWithUniqueName(Working, NewItem);
                Archive[Working.FullPath] = NewItem;

                int imgidx = ArcUtil.IndexOfExtensionImageOrDefault(info.Extension, ArchiveTreeView.ImageList);
                Working.ImageIndex = Working.SelectedImageIndex = imgidx;
            }
        }
        else
            throw new InvalidDataException("This should be impossible");

        Program.IsUnsavedChanges = true;
    }

    private void ExportSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || ArchiveTreeView.SelectedNode is null)
            return;

        string Path = ArchiveTreeView.SelectedNode.FullPath;
        dynamic? Item = Archive[Path];
        if (Item is ArchiveDirectory)
        {
            if (FBD.ShowDialog() == DialogResult.OK)
                ExportDirectory(Path, FBD.SelectedPath);
        }
        else if (Item is ArchiveFile af)
        {
            FileSFD.FileName = af.Name;
            FileSFD.Filter = GetKnownExtensionFilter(af.Extension ?? "");
            if (FileSFD.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(FileSFD.FileName))
                ExportFile(Path, FileSFD.FileName);
        }

    }

    private void ExportAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Archive is null || Archive.Root is null) // Second case should be impossible...
            return;

        if (FBD.ShowDialog() == DialogResult.OK)
            ExportDirectory("/", FBD.SelectedPath);
    }


    private void ThemeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (IsBooting || IsLoadingFile)
        //    return;

        ProgramColors.IsDarkMode = ThemeToolStripComboBox.SelectedIndex == 1;
        Program.ReloadTheme();
        SettingsToolStripMenuItem.HideDropDown();
        ProgramColors.ReloadTheme(ArchiveContextMenuStrip);
        Program.Settings.ThemeIndex = ThemeToolStripComboBox.SelectedIndex;
        ReloadTreeViewIcons();
        Program.Settings.OnChanged(sender, e);
    }

    private void PaddingToolStripColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Program.Settings.PaddingIndex = PaddingToolStripColorComboBox.SelectedIndex;
        Program.Settings.OnChanged(sender, e);
    }

    private void AutoYaz0ToolStripColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Program.Settings.AutoHandleCompression = AutoYaz0ToolStripColorComboBox.SelectedIndex > 0;
        Program.Settings.OnChanged(sender, e);
    }

    private void FilePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using Form? FilePropertiesForm = Archive switch
        {
            RARC rarc => new FilePropertyRARCForm(ArchiveTreeView, rarc),

            _ => null
        };
        FilePropertiesForm?.ShowDialog();
    }

    private void Yaz0ToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Program.Settings.CompressionIndex = Yaz0ToolStripComboBox.SelectedIndex;
        Program.Settings.OnChanged(sender, e);
    }
    #endregion

    #region TreeView

    #region Helper Methods
    private static void DrawLeafTopPlaceholders(TreeNode NodeOver, TreeView Tree)
    {
        using Graphics g = Tree.CreateGraphics();

        int NodeOverImageWidth = Tree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
        int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
        int RightPos = Tree.Width - 4;

        Point[] LeftTriangle = [
            new(LeftPos, NodeOver.Bounds.Top - 4),
            new(LeftPos, NodeOver.Bounds.Top + 4),
            new(LeftPos + 4, NodeOver.Bounds.Y),
            new(LeftPos + 4, NodeOver.Bounds.Top - 1),
            new(LeftPos, NodeOver.Bounds.Top - 5)
        ];

        Point[] RightTriangle = [
            new(RightPos, NodeOver.Bounds.Top - 4),
            new(RightPos, NodeOver.Bounds.Top + 4),
            new(RightPos - 4, NodeOver.Bounds.Y),
            new(RightPos - 4, NodeOver.Bounds.Top - 1),
            new(RightPos, NodeOver.Bounds.Top - 5)
        ];

        var brush = ArchiveTreeViewBrush;
        g.FillPolygon(brush, LeftTriangle);
        g.FillPolygon(brush, RightTriangle);
        g.DrawLine(new Pen(brush, 2), new(LeftPos, NodeOver.Bounds.Top), new(RightPos, NodeOver.Bounds.Top));

    }

    private static void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode? ParentDragDrop, TreeView Tree)
    {
        using Graphics g = Tree.CreateGraphics();

        int NodeOverImageWidth = Tree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
        // Once again, we're not dragging to node over, draw the placeholder using the ParentDragDrop bounds
        int LeftPos, RightPos;
        if (ParentDragDrop is not null)
            LeftPos = ParentDragDrop.Bounds.Left - (Tree.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
        else
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
        RightPos = Tree.Width - 4;

        Point[] LeftTriangle = [
            new(LeftPos, NodeOver.Bounds.Bottom - 4),
            new(LeftPos, NodeOver.Bounds.Bottom + 4),
            new(LeftPos + 4, NodeOver.Bounds.Bottom),
            new(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
            new(LeftPos, NodeOver.Bounds.Bottom - 5)
        ];

        Point[] RightTriangle = [
            new(RightPos, NodeOver.Bounds.Bottom - 4),
            new(RightPos, NodeOver.Bounds.Bottom + 4),
            new(RightPos - 4, NodeOver.Bounds.Bottom),
            new(RightPos - 4, NodeOver.Bounds.Bottom - 1),
            new(RightPos, NodeOver.Bounds.Bottom - 5)
        ];

        var brush = ArchiveTreeViewBrush;
        g.FillPolygon(brush, LeftTriangle);
        g.FillPolygon(brush, RightTriangle);
        g.DrawLine(new Pen(brush, 2), new(LeftPos, NodeOver.Bounds.Bottom), new(RightPos, NodeOver.Bounds.Bottom));
    }

    private static void DrawFolderTopPlaceholders(TreeNode NodeOver, TreeView Tree)
    {
        using Graphics g = Tree.CreateGraphics();
        int NodeOverImageWidth = Tree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

        int LeftPos, RightPos;
        LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
        RightPos = Tree.Width - 4;

        Point[] LeftTriangle = [
            new(LeftPos, NodeOver.Bounds.Top - 4),
            new(LeftPos, NodeOver.Bounds.Top + 4),
            new(LeftPos + 4, NodeOver.Bounds.Y),
            new(LeftPos + 4, NodeOver.Bounds.Top - 1),
            new(LeftPos, NodeOver.Bounds.Top - 5)
        ];

        Point[] RightTriangle = [
            new(RightPos, NodeOver.Bounds.Top - 4),
            new(RightPos, NodeOver.Bounds.Top + 4),
            new(RightPos - 4, NodeOver.Bounds.Y),
            new(RightPos - 4, NodeOver.Bounds.Top - 1),
            new(RightPos, NodeOver.Bounds.Top - 5)
        ];

        var brush = ArchiveTreeViewBrush;
        g.FillPolygon(brush, LeftTriangle);
        g.FillPolygon(brush, RightTriangle);
        g.DrawLine(new Pen(brush, 2), new(LeftPos, NodeOver.Bounds.Top), new(RightPos, NodeOver.Bounds.Top));
    }

    private void DrawAddToFolderPlaceholder(TreeNode NodeOver, TreeView Tree)
    {
        using Graphics g = Tree.CreateGraphics();
        int RightPos = NodeOver.Bounds.Right + 6;
        Point[] RightTriangle = [
            new(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
            new(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
            new(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
            new(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
            new(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)
        ];

        ArchiveTreeView.Refresh();
        g.FillPolygon(ArchiveTreeViewBrush, RightTriangle);
    }

    private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
    {
        NewNodeMap.Length = 0;

        if (boolBelowNode)
            NewNodeMap.Insert(0, tnNode.Index + 1);
        else
            NewNodeMap.Insert(0, tnNode.Index);
        TreeNode tnCurNode = tnNode;

        while (tnCurNode.Parent != null)
        {
            tnCurNode = tnCurNode.Parent;

            if (NewNodeMap.Length == 0 && boolBelowNode == true)
                NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
            else
                NewNodeMap.Insert(0, tnCurNode.Index + "|");
        }
    }

    private bool SetMapsEqual()
    {
        if (NewNodeMap.ToString().Equals(NodeMap))
            return true;
        NodeMap = NewNodeMap.ToString();
        return false;
    }
    #endregion

    // ItemDrag -> DragEnter -> DragOver -> DragDrop

    private void ArchiveTreeView_ItemDrag(object sender, ItemDragEventArgs e)
    {
        NodeMap = null;
        NewNodeMap.Length = 0;
        if (e.Item is null)
            return;

        if (e.Button == MouseButtons.Right)
        {
            if (Archive is null || e.Item is not TreeNode TN)
                return;
            object? Item = Archive[TN.FullPath];
            string TempFile;
            if (Item is ArchiveFile file)
            {
                TempFile = Path.Combine(Path.GetTempPath(), file.Name);
                FileUtil.SaveFile(TempFile, file.Save);
                DDD();
                File.Delete(TempFile);
            }
            else if (Item is ArchiveDirectory dir)
            {
                TempFile = Path.Combine(Path.GetTempPath(), dir.Name);
                dir.Export(TempFile);
                DDD();
                Directory.Delete(TempFile, true);
            }
            else
                throw new Exception("Impossible");

            void DDD()
            {
                string[] files = [TempFile];
                string DataFormat = DataFormats.FileDrop;
                DataObject data = new(DataFormat, files);
                DoDragDrop(data, DragDropEffects.Copy);
            }
        }
        else
        {
            var x = DoDragDrop(e.Item, DragDropEffects.Move);
            if (x == DragDropEffects.None)
                MessageBox.Show("If you are trying to drag an item onto your desktop,\nuse the Right Mouse Button to do so.","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        ArchiveTreeView.Refresh();
    }

    private void ArchiveTreeView_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data is null)
            return;

        if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
        {
            var extracted = e.Data.GetData(DataFormats.FileDrop, false);
            if (extracted is not string[] DiscPaths)
                return; // TODO: Warning here instead?

            if (Archive is null) // Use the dropped information to create a new archive!
            {
                if (DiscPaths.Length == 1 && File.GetAttributes(DiscPaths[0]) == FileAttributes.Directory)
                    NewArchive(DiscPaths[0]); // If a single folder was dropped, we can just create a new archive using that folder.
                else
                {
                    // Otherwise, we have to add everything separately
                    NewArchive(null);
                    InsertItemsToArchive(DiscPaths, null);
                }
            }
            else // Else insert the dropped items where the drop occurred
            {
                if (string.IsNullOrWhiteSpace(NodeMap))
                {
                    InsertItemsToArchive(DiscPaths, null);
                    return;
                }
                string[] NodeIndexes = NodeMap.Split('|');
                TreeNodeCollection InsertCollection = ArchiveTreeView.Nodes;
                TreeNode? target;
                for (int i = 0; i < NodeIndexes.Length - 1; i++)
                {
                    target = InsertCollection[int.Parse(NodeIndexes[i])];
                    InsertCollection = target.Nodes;
                }

                if (InsertCollection != null)
                {
                    int idx = int.Parse(NodeIndexes[^1]);
                    if (idx == InsertCollection.Count)
                    {
                        TreeNode n = InsertCollection[idx - 1];
                        InsertItemsToArchive(DiscPaths, n.Parent, NoInsertDir: NodeIndexes.Length == 1);
                    }
                    else if (idx > 0)
                    {
                        TreeNode n = InsertCollection[idx - 1];
                        InsertItemsToArchive(DiscPaths, n, NoInsertDir: true);
                    }
                    else
                    {
                        TreeNode n = InsertCollection[idx];
                        InsertItemsToArchive(DiscPaths, n, true, true);
                    }
                }
            }
        }
        else if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && !string.IsNullOrWhiteSpace(NodeMap))
        {
            var extracted = e.Data.GetData("System.Windows.Forms.TreeNode");
            if (extracted is TreeNode tn)
            {
                if (ArchiveTreeView.Nodes.FindTreeNodeByReference(tn) is not null)
                    DropTreeNode(tn);
            }
        }

        void DropTreeNode(TreeNode MovingNode)
        {
            if (Archive is null || Archive.Root is null || NodeMap is null)
                return;

            string[] NodeIndexes = NodeMap.Split('|');
            TreeNodeCollection InsertCollection = ArchiveTreeView.Nodes;
            string NewPath = Archive.Root.Name;
            for (int i = 0; i < NodeIndexes.Length - 1; i++)
            {
                NewPath += "/" + InsertCollection[int.Parse(NodeIndexes[i])].Text;
                InsertCollection = InsertCollection[int.Parse(NodeIndexes[i])].Nodes;
            }

            if (InsertCollection != null)
            {
                //We need to make sure this is a valid move
                NewPath += "/" + MovingNode.Text;
                if (Archive.ItemExists(NewPath) && !(Archive.Root.Name + "/" + MovingNode.FullPath).Equals(NewPath))
                {
                    MessageBox.Show($"An item with the name \"{MovingNode.Text}\"\nalready exists at \"{NewPath}\"", "Item Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InsertCollection.Insert(int.Parse(NodeIndexes[^1]), (TreeNode)MovingNode.Clone());
                ArchiveTreeView.SelectedNode = InsertCollection[int.Parse(NodeIndexes[^1])];
                string Old = MovingNode.FullPath, New = InsertCollection[int.Parse(NodeIndexes[^1])].FullPath;

                Archive.MoveItem(Old, New);
                MovingNode.Remove();
            }
        }
    }

    private void ArchiveTreeView_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = e.AllowedEffect;
    }

    private void ArchiveTreeView_DragOver(object sender, DragEventArgs e)
    {
        if (Archive is null)
            return;

        TreeNode NodeOver = ArchiveTreeView.GetNodeAt(ArchiveTreeView.PointToClient(Cursor.Position));
        TreeNode? NodeMoving = (TreeNode?)e.Data?.GetData("System.Windows.Forms.TreeNode") ?? null;

        // This code is used to stop accidentally opening the wrong folder when dragging.
        // That was really annoying in older WiiExplorer versions!
        Point cur = Cursor.Position;
        if (cur.DistanceTo(LastDragPos) > 0)
        {
            LastDragPos = cur;
            DragHeldCounter = 0;
        }
        else
            DragHeldCounter++;

        MainToolStripStatusLabel.Text = NodeOver?.Text ?? "NONE";

        // A bit long, but to summarize, process the following code only if the nodeover is null
        // and either the nodeover is not the same thing as nodemoving UNLESS nodeover happens
        // to be the last node in the branch (so we can allow drag & drop below a parent branch)
        if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
        {
            int OffsetY = ArchiveTreeView.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;

            // There's a weird inconsistent behaviour somewhere in here regarding the detection of folder insertion but idk where it is
            // and I cannot be bothered to find it, due to only being a visual bug

            if (Archive[NodeOver.FullPath] is ArchiveFile)
            {
                #region Standard Node
                if (OffsetY < (NodeOver.Bounds.Height / 2))
                {
                    #region If NodeOver is a child then cancel
                    TreeNode tnParadox = NodeOver;
                    while (tnParadox.Parent != null)
                    {
                        if (tnParadox.Parent == NodeMoving)
                        {
                            NodeMap = "";
                            return;
                        }

                        tnParadox = tnParadox.Parent;
                    }
                    #endregion
                    #region Store the placeholder info into a pipe delimited string
                    SetNewNodeMap(NodeOver, false);
                    if (SetMapsEqual() == true)
                        return;
                    #endregion
                    #region Clear placeholders above and below
                    ArchiveTreeView.Refresh();
                    #endregion
                    #region Draw the placeholders
                    DrawLeafTopPlaceholders(NodeOver, ArchiveTreeView);
                    #endregion
                }
                else
                {
                    #region If NodeOver is a child then cancel
                    TreeNode tnParadox = NodeOver;
                    while (tnParadox.Parent != null)
                    {
                        if (tnParadox.Parent == NodeMoving)
                        {
                            NodeMap = "";
                            return;
                        }

                        tnParadox = tnParadox.Parent;
                    }
                    #endregion
                    #region Allow drag drop to parent branches
                    TreeNode? ParentDragDrop = null;
                    // If the node the mouse is over is the last node of the branch we should allow
                    // the ability to drop the "nodemoving" node BELOW the parent node
                    if (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
                    {
                        int XPos = ArchiveTreeView.PointToClient(Cursor.Position).X;
                        if (XPos < NodeOver.Bounds.Left)
                        {
                            ParentDragDrop = NodeOver.Parent;

                            if (XPos < (ParentDragDrop.Bounds.Left - ArchiveTreeView.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width))
                            {
                                if (ParentDragDrop.Parent != null)
                                    ParentDragDrop = ParentDragDrop.Parent;
                            }
                        }
                    }
                    #endregion
                    #region Store the placeholder info into a pipe delimited string
                    // Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
                    SetNewNodeMap(ParentDragDrop ?? NodeOver, true);
                    if (SetMapsEqual() == true)
                        return;
                    #endregion
                    #region Clear placeholders above and below
                    ArchiveTreeView.Refresh();
                    #endregion
                    #region Draw the placeholders
                    DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop, ArchiveTreeView);
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Folder Node
                if (OffsetY < (NodeOver.Bounds.Height / 3))
                {
                    //this.lblDebug.Text = "folder top";

                    #region If NodeOver is a child then cancel
                    TreeNode tnParadox = NodeOver;
                    while (tnParadox.Parent != null)
                    {
                        if (tnParadox.Parent == NodeMoving)
                        {
                            NodeMap = "";
                            return;
                        }

                        tnParadox = tnParadox.Parent;
                    }
                    #endregion
                    #region Store the placeholder info into a pipe delimited string
                    SetNewNodeMap(NodeOver, false);
                    if (SetMapsEqual() == true)
                        return;
                    #endregion
                    #region Clear placeholders above and below
                    ArchiveTreeView.Refresh();
                    #endregion
                    #region Draw the placeholders
                    DrawFolderTopPlaceholders(NodeOver, ArchiveTreeView);
                    #endregion
                }
                else if ((NodeOver.Parent != null && NodeOver.Index == 0) && (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))))
                {
                    //this.lblDebug.Text = "folder bottom";

                    #region If NodeOver is a child then cancel
                    TreeNode tnParadox = NodeOver;
                    while (tnParadox.Parent != null)
                    {
                        if (tnParadox.Parent == NodeMoving)
                        {
                            NodeMap = "";
                            return;
                        }

                        tnParadox = tnParadox.Parent;
                    }
                    #endregion
                    #region Store the placeholder info into a pipe delimited string
                    SetNewNodeMap(NodeOver, true);
                    if (SetMapsEqual() == true)
                        return;
                    #endregion
                    #region Clear placeholders above and below
                    ArchiveTreeView.Refresh();
                    #endregion
                    #region Draw the placeholders
                    DrawFolderTopPlaceholders(NodeOver, ArchiveTreeView);
                    #endregion
                }
                else
                {
                    if (!NodeOver.IsExpanded && NodeOver.Nodes.Count > 0 && DragHeldCounter > 2)
                        NodeOver.Expand();
                    else
                    {
                        #region Prevent the node from being dragged onto itself
                        if (NodeMoving == NodeOver)
                            return;
                        #endregion
                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, false);
                        NewNodeMap = NewNodeMap.Insert(NewNodeMap.Length, "|0");

                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        ArchiveTreeView.Refresh();
                        #endregion
                        #region Draw the "add to folder" placeholder
                        DrawAddToFolderPlaceholder(NodeOver, ArchiveTreeView);
                        #endregion
                    }
                }
                #endregion
            }
        }
        else
        {
            NodeMap = null;
            NewNodeMap.Length = 0;
        }
    }

    // ------------------------------------------------------------------------------------------

    private void ArchiveTreeView_MouseDown(object sender, MouseEventArgs e)
    {
        TreeViewHitTestInfo x = ArchiveTreeView.HitTest(e.X, e.Y);
        ArchiveTreeView.SelectedNode = x.Location == TreeViewHitTestLocations.RightOfLabel ? null : x.Node;
        if (ArchiveTreeView.SelectedNode is null)
        {
            FilePropertiesToolStripMenuItem.Enabled = false;
            DeleteSelectedToolStripMenuItem.Enabled = ContextDeleteSelectedToolStripMenuItem.Enabled = false;
            RenameSelectedToolStripMenuItem.Enabled = ContextRenameToolStripMenuItem.Enabled = false;
            ReplaceSelectedToolStripMenuItem.Enabled = ContextReplaceSelectedToolStripMenuItem.Enabled = false;
            ExportSelectedToolStripMenuItem.Enabled = ContextExportSelectedToolStripMenuItem.Enabled = false;
            MainToolStripStatusLabel.Text = "No selection.";
        }
    }

    private void ArchiveTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        if (e.Node is not null)
            e.Node.ImageIndex = e.Node.SelectedImageIndex = ArcUtil.IndexOfInternalImageOrDefault(ArcUtil.INTERNAL_FOLDEROPEN, ArchiveTreeView.ImageList);
    }

    private void ArchiveTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
        if (e.Node is not null)
            e.Node.ImageIndex = e.Node.SelectedImageIndex = ArcUtil.IndexOfInternalImageOrDefault(ArcUtil.INTERNAL_FOLDERCLOSE, ArchiveTreeView.ImageList);
    }

    private void ArchiveTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (Archive is null)
            return;

        if (ArchiveTreeView.SelectedNode is null)
            return;

        DeleteSelectedToolStripMenuItem.Enabled = ContextDeleteSelectedToolStripMenuItem.Enabled = true;
        RenameSelectedToolStripMenuItem.Enabled = ContextRenameToolStripMenuItem.Enabled = true;
        ReplaceSelectedToolStripMenuItem.Enabled = ContextReplaceSelectedToolStripMenuItem.Enabled = true;
        ExportSelectedToolStripMenuItem.Enabled = ContextExportSelectedToolStripMenuItem.Enabled = true;

        if (Archive[ArchiveTreeView.SelectedNode.FullPath] is ArchiveDirectory dir)
        {
            MainToolStripStatusLabel.Text = string.Format("{0} - {1} files, {2} subfolders", dir.Name, dir.Items.Count(i => i.Value is ArchiveFile), dir.Items.Count(i => i.Value is ArchiveDirectory));
        }
        else if (Archive[ArchiveTreeView.SelectedNode.FullPath] is ArchiveFile file)
        {
            MainToolStripStatusLabel.Text = string.Format("{0} - {1} bytes. [CRC: {2}]", file.Name, file.Length, HashUtil.CalcCRC32(file.FileData).ToString("X8"));
            if (file is RARC.File)
                FilePropertiesToolStripMenuItem.Enabled = true;
        }
    }

    private void ArchiveTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            ArchiveContextMenuStrip.Show(ArchiveTreeView, e.Location);
        }
    }
    #endregion

    #region BackgroundWorker
    private static readonly Stopwatch EncodingTimer = new();
    private void EncodingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        if (e is null || e.Argument is null)
            return;

        (string Filepath, int CompressionMode, int PaddingMode) = ((string, int, int))e.Argument;

        int OriginalSize = 0;
        long CompressedSize = 0;
        if (CompressionMode < BuiltInCompressions.Count)
        {
            // Built-in Compressors (Hack.io)
            FileInfo FI = new(Filepath);
            byte[] Original = File.ReadAllBytes(FI.FullName);
            OriginalSize = Original.Length;
            EncodingTimer.Start();
            byte[] Compressed = BuiltInCompressions[CompressionMode].Function?.Invoke(Original, EncodingBackgroundWorker) ?? [];
            EncodingTimer.Stop();
            CompressedSize = Compressed.Length;
            if (Compressed.Length > 0)
                File.WriteAllBytes(FI.FullName, Compressed);
            else
                e.Cancel = true;
        }
        else
        {
            // User compressions... which I have no idea if this works on MacOS or Linux...
            string ProcessExeName = "cmd.exe";
            string ProcessPreCommand = "/c";
            string command = "";
            int idx = CompressionMode - BuiltInCompressions.Count;
            try
            {
                OriginalSize = File.ReadAllBytes(Filepath).Length;
                command = string.Format(UserCompressions[idx].CommandFormat, Filepath);
                ProcessStartInfo psi = new(ProcessExeName, ProcessPreCommand + " " + command);
                EncodingTimer.Start();
                Process? process = Process.Start(psi) ?? throw new NullReferenceException();
                while (!process.HasExited)
                {
                    if (EncodingBackgroundWorker.WorkerSupportsCancellation && EncodingBackgroundWorker.CancellationPending)
                    {
                        process.Kill();
                        e.Cancel = true;
                        if (EncodingBackgroundWorker.WorkerReportsProgress)
                            EncodingBackgroundWorker.ReportProgress(0);
                    }
                }
                if (process.ExitCode != 0)
                    throw new InvalidOperationException($"Process exited with code {process.ExitCode}");
                CompressedSize = File.ReadAllBytes(Filepath).Length;
            }
            catch (Exception ex)
            {
                if (command is not null)
                    MessageBox.Show($"An exception occured trying to execute the following command:\n\n{command}\n\n{ex.Message}", "User-Compression Execute Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"An exception occured trying to parse the following command:\n\n{UserCompressions[idx].CommandFormat}\n\n{ex.Message}", "User-Compression Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            finally
            {
                EncodingTimer.Stop();
            }
        }

        if (e.Cancel)
            return;

        if (PaddingMode == 2 || PaddingMode == 4)
            ArcUtil.PadFile(Filepath, PaddingMode == 4 ? 32 : 16);

        if (OriginalSize > 0 && CompressedSize > 0)
            e.Result = MathUtil.GetPercentOf(CompressedSize, OriginalSize);
    }

    private void EncodingBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        //MainToolStripStatusLabel.Text = string.Format("{0} encoding", (Yaz0ToolStripComboBox.SelectedIndex == 2 ? $"Fast " : "") + (Yaz0ToolStripComboBox.SelectedIndex == 3 ? "Yay0" : "Yaz0"), timer.Elapsed.ToString("mm\\:ss"), TimeSpan.FromMilliseconds(ETA).ToString("mm\\:ss"));
        MainToolStripStatusLabel.Text = $"[{Yaz0ToolStripComboBox.Text}] Compressing: {e.ProgressPercentage}%";
        MainToolStripProgressBar.Value = e.ProgressPercentage;
    }

    private void EncodingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled)
            MainToolStripStatusLabel.Text = "Compression Cancelled. (Saved without compression.)";
        else
            MainToolStripStatusLabel.Text = $"Save Complete! [{e.Result}% ratio in {EncodingTimer}]";
    }
    #endregion

    private static bool IsDiscardChanges() => MessageBox.Show("You have Unsaved Changes, are you sure you want to proceed?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
}
