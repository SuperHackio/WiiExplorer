using Hack.io.RARC;
using Hack.io.YAZ0;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using WiiExplorer.Properties;
using System.Reflection;
using Hack.io.YAY0;
using WiiExplorer.Properties.Languages;
using Hack.io.U8;
using Hack.io;
using Hack.io.Util;

namespace WiiExplorer
{
    public partial class MainForm : Form
    {
        OpenFileDialog ofd = new OpenFileDialog() { Filter = Strings.OpenFileFilter };
        SaveFileDialog sfd = new SaveFileDialog() { Filter = Strings.OpenFileFilter };
        OpenFileDialog Fileofd = new OpenFileDialog() { Multiselect = true };
        SaveFileDialog Exportsfd = new SaveFileDialog();
        FolderBrowserDialog ExportFBD = new FolderBrowserDialog() { ShowNewFolderButton = true };
        Archive Archive;
        bool Edited = false;
        static List<string> KnownExtensions = new List<string>
        {
            "Extensionless File|*"
        };
        string OpenWith = null;
        public MainForm(string Openwith)
        {
            InitializeComponent();
            CenterToScreen();
            Yaz0ToolStripComboBox.SelectedIndex = Program.EncodingMode;
            Text = $"WiiExplorer {Application.ProductVersion}";
            OpenWith = Openwith;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ExtensionList.txt"))
            {
                string[] exts = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "ExtensionList.txt");
                KnownExtensions.AddRange(exts);
                for (int i = 0; i < exts.Length; i++)
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Icons/" + exts[i].Split('|')[0] + ".png"))
                        ArchiveImageList.Images.Add(exts[i].Split('|')[1], new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "/Icons/" + exts[i].Split('|')[0] + ".png"));
            }

            RootNameTextBox.ContextMenu = new ContextMenu();
            ReloadTheme();
            Yaz0ToolStripComboBox.ComboBox.SetDoubleBuffered();
            ArchiveTreeView.SetDoubleBuffered();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value + (Archive is null ? "" : (Archive is U8 ? " (U8)":" (RARC)"));
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (OpenWith != null && (File.Exists(OpenWith) || Directory.Exists(OpenWith)))
            {
                if (File.GetAttributes(OpenWith) == FileAttributes.Directory)
                {
                    NewFromFolder(OpenWith);
                }
                else
                    OpenArchive(OpenWith);
            }
            OpenWith = null;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (RootNameTextBox.Focused)
                    RootNameTextBox.SelectAll();
                else
                    AddFileToolStripMenuItem_Click(sender, EventArgs.Empty);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = EncodingBackgroundWorker.IsBusy || (e.CloseReason == CloseReason.UserClosing & Edited) && MessageBox.Show(Strings.UnsavedChangesOnQuit, Strings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;

        private void EncodingBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (Program.EncodingMode == 3)
                YAY0.Compress((string)e.Argument);
            else
                YAZ0.Compress((string)e.Argument, Program.EncodingMode == 2);
        }

        private void SetControlsEnabled(bool toggle = true, bool affectall = false)
        {
            EditToolStripMenuItem.Enabled = toggle;
            AddFileToolStripMenuItem.Enabled = toggle;
            AddFolderToolStripMenuItem.Enabled = toggle;
            DeleteSelectedToolStripMenuItem.Enabled = toggle;
            RenameSelectedToolStripMenuItem.Enabled = toggle;
            ExportSelectedToolStripMenuItem.Enabled = toggle;
            ExportAllToolStripMenuItem.Enabled = toggle;
            ReplaceSelectedToolStripMenuItem.Enabled = toggle;
            ImportFolderToolStripMenuItem.Enabled = toggle;
            ArchiveTreeView.Enabled = toggle;
            RootNameTextBox.Enabled = toggle;
            if (Archive is RARC)
                KeepIDsSyncedCheckBox.Enabled = toggle;
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
        }
        
        private void RootNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Archive.Root.Name = RootNameTextBox.Text;
            Edited = true;
        }

        private void KeepIDsSyncedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Archive != null && Archive is RARC r)
            {
                r.KeepFileIDsSynced = KeepIDsSyncedCheckBox.Checked;
                Edited = true;
            }
        }

        #region ToolStripMenuItems

        private void MainFormMenuStrip_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < MainFormMenuStrip.Items.Count; i++)
            {
                if (MainFormMenuStrip.Items[i] is ToolStripComboBox cb)
                {
                    Rectangle r = new Rectangle(
                        cb.ComboBox.Location.X - 1,
                        cb.ComboBox.Location.Y - 1,
                        cb.ComboBox.Size.Width + 1,
                        cb.ComboBox.Size.Height + 1);

                    Pen cbBorderPen = new Pen(Program.ProgramColours.BorderColour);
                    e.Graphics.DrawRectangle(cbBorderPen, r);
                }
            }
            if (!Yaz0ToolStripComboBox.ComboBox.DroppedDown && Yaz0ToolStripComboBox.Focused)
                RootNameLabel.Focus();
        }

        #region File
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Edited && MessageBox.Show(Strings.UnsavedChangesNewFile, Strings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            MessageBoxManager.Register();
            MessageBoxManager.No = "RARC";
            MessageBoxManager.Yes = "U8";
            DialogResult dr = MessageBox.Show(Strings.ChooseFormatMessage, Strings.Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
                return;
            if (dr == DialogResult.Yes)
            {
                Archive = new U8();
            }
            else
            {
                Archive = new RARC() { KeepFileIDsSynced = true };
            }
            MessageBoxManager.Unregister();
            ArchiveTreeView.Nodes.Clear();
            Archive[Strings.NewArchive.Replace(" ","")] = null;
            RootNameTextBox.Text = Archive.Root.Name;
            if (Archive is RARC r)
            {
                KeepIDsSyncedCheckBox.Checked = r.KeepFileIDsSynced;
                KeepIDsSyncedCheckBox.Enabled = true;
            }
            else
            {
                KeepIDsSyncedCheckBox.Checked =
                KeepIDsSyncedCheckBox.Enabled = false;
            }

            Edited = false;
            SetControlsEnabled();
            MainToolStripProgressBar.Value = 100;
            MainToolStripStatusLabel.Text = Strings.NewArchiveMessage;
            Text = $"WiiExplorer {Application.ProductVersion} - {Strings.NewArchive}";
        }

        private void NewFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Edited && MessageBox.Show(Strings.UnsavedChangesNewFile, Strings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            CommonOpenFileDialog BFB = new CommonOpenFileDialog() { InitialDirectory = Settings.Default.PreviousAddFilePath, Multiselect = false, IsFolderPicker = true };
            if (BFB.ShowDialog() == CommonFileDialogResult.Ok && !BFB.FileName.Equals(""))
            {
                NewFromFolder(BFB.FileName);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(Edited && MessageBox.Show(Strings.UnsavedChangesOpenFile, Strings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No))
            {
                ofd.InitialDirectory = Settings.Default.PreviousOpenArchivePath;
                string tmp = Settings.Default.PreviousOpenArchivePath;
                if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != "")
                    OpenArchive(ofd.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Archive.FileName is null)
            {
                sfd.InitialDirectory = Settings.Default.PreviousSaveArchivePath;
                if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName != "")
                    SaveArchive(sfd.FileName);
                else
                    return;
            }
            else
                SaveArchive(Archive.FileName);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfd.InitialDirectory = Settings.Default.PreviousSaveArchivePath;
            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName != "")
                SaveArchive(sfd.FileName);
        }
        #endregion

        #region Edit
        private void AddFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tmp = ArchiveTreeView.SelectedNode;
            Fileofd.InitialDirectory = Settings.Default.PreviousAddFilePath;
            if (Fileofd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(Fileofd.FileName))
            {
                ArchiveTreeView.SelectedNode = tmp;
                AddItemsToArchive(Fileofd.FileNames);
                MainToolStripStatusLabel.Text = string.Format(Strings.AddedFilesMessage, Fileofd.FileNames.Length);
                Settings.Default.PreviousAddFilePath = new FileInfo(Fileofd.FileName).DirectoryName;
                Settings.Default.Save();
            }
            ArchiveTreeView.SelectedNode = tmp;

            Edited = true;
        }

        private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode NewTreeNode = new TreeNode(Strings.NewFolder) { ImageIndex = 0, SelectedImageIndex = 0 };

            //SelectedNode is NULL, put the new file on the root
            if (ArchiveTreeView.SelectedNode == null)
                ArchiveTreeView.Nodes.Add(NewTreeNode);
            //Determine where to put it otherwise
            else
            {
                if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory)
                    ArchiveTreeView.SelectedNode.Nodes.Add(NewTreeNode);
                else if (ArchiveTreeView.SelectedNode.Parent == null)
                    ArchiveTreeView.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                else
                    ArchiveTreeView.SelectedNode.Parent.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
            }

            ArchiveTreeView.SelectedNode = NewTreeNode;
            int y = 2;
            string folderstring = Strings.NewFolder;
            while (Archive.ItemExists(NewTreeNode.FullPath))
                NewTreeNode.Text = folderstring + $" ({y++})";
            Archive[NewTreeNode.FullPath] = new RARC.Directory() { Name = NewTreeNode.Text };

            Edited = true;
            MainToolStripStatusLabel.Text = Strings.AddedNewFolderMessage;
            RenameSelectedToolStripMenuItem_Click(sender, e);
        }

        private void ImportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog BFB = new CommonOpenFileDialog() { InitialDirectory = Settings.Default.PreviousAddFilePath, Multiselect = false, IsFolderPicker = true };
            if (BFB.ShowDialog() == CommonFileDialogResult.Ok && !BFB.FileName.Equals(""))
            {
                string ogname = new DirectoryInfo(BFB.FileName).Name;
                TreeNode NewTreeNode = new TreeNode(ogname) { ImageIndex = 0, SelectedImageIndex = 0 };

                //SelectedNode is NULL, put the new file on the root
                if (ArchiveTreeView.SelectedNode == null)
                    ArchiveTreeView.Nodes.Add(NewTreeNode);
                //Determine where to put it otherwise
                else
                {
                    if (Archive[ArchiveTreeView.SelectedNode.FullPath] is ArchiveDirectory)
                        ArchiveTreeView.SelectedNode.Nodes.Add(NewTreeNode);
                    else if (ArchiveTreeView.SelectedNode.Parent == null)
                        ArchiveTreeView.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                    else
                        ArchiveTreeView.SelectedNode.Parent.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                }

                ArchiveTreeView.SelectedNode = NewTreeNode;
                ArchiveDirectory dir;
                if (Archive is RARC r)
                {
                    dir = new RARC.Directory(BFB.FileName, r);
                }
                else
                {
                    dir = new ArchiveDirectory(BFB.FileName, Archive); //U8 just uses the default base class
                }
                int y = 2;
                while (Archive.ItemExists(NewTreeNode.FullPath))
                {
                    NewTreeNode.Text = ogname + $" ({y++})";
                }
                dir.Name = NewTreeNode.Text;
                Archive[NewTreeNode.FullPath] = dir;

                ArchiveTreeView.Nodes.Clear();
                ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                ArchiveTreeView.SelectedNode = NewTreeNode;

                Settings.Default.PreviousAddFilePath = new DirectoryInfo(BFB.FileName).Parent.FullName;
                Settings.Default.Save();
                Edited = true;
                MainToolStripStatusLabel.Text = string.Format(Strings.ImportedFolderMessage, BFB.FileName);
            }
        }

        private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;

            MainToolStripStatusLabel.Text = string.Format(Strings.RemoveItemMessage, ArchiveTreeView.SelectedNode.Text);
            Archive[ArchiveTreeView.SelectedNode.FullPath] = null;
            ArchiveTreeView.SelectedNode.Remove();

            Edited = true;
        }

        private void RenameSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;
            string tmp = ArchiveTreeView.SelectedNode.Text;
            RenameForm RN = new RenameForm(ArchiveTreeView, Archive);
            if (RN.ShowDialog() != DialogResult.OK)
                return;

            if (RN.ExtensionTextBox.Enabled)
            {
                int imageindex = 2;
                if (ArchiveImageList.Images.ContainsKey("*" + RN.ExtensionTextBox.Text))
                    imageindex = ArchiveImageList.Images.IndexOfKey("*" + RN.ExtensionTextBox.Text);

                ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = imageindex;
            }
            Edited = true;
            MainToolStripStatusLabel.Text = string.Format(Strings.RenameItemMessage, tmp, RN.NameTextBox.Text + RN.ExtensionTextBox.Text);
        }

        private void ExportSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;
            ExportArchiveFile(Archive[ArchiveTreeView.SelectedNode.FullPath]);
        }

        private void ExportAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exportsfd.Filter = "Directory|directory";
            //Exportsfd.FileName = RootNameTextBox.Text;
            ExportFBD.SelectedPath = Settings.Default.PreviousExportPath;
            if (ExportFBD.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ExportFBD.SelectedPath))
            {
                MainToolStripProgressBar.Value = 0;
                Archive.Export(ExportFBD.SelectedPath, true);
                MainToolStripProgressBar.Value = 100;
                MainToolStripStatusLabel.Text = string.Format(Strings.ExportAllMessage, Archive.Root.Name);
                Settings.Default.PreviousExportPath = ExportFBD.SelectedPath;
                Settings.Default.Save();
            }
        }

        private void ReplaceSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;

            string OldPath = ArchiveTreeView.SelectedNode.FullPath;
            dynamic Item = Archive[OldPath];
            if (Item is RARC.Directory)
            {
                CommonOpenFileDialog BFB = new CommonOpenFileDialog() { InitialDirectory = Settings.Default.PreviousAddFilePath, Multiselect = false, IsFolderPicker = true };
                if (BFB.ShowDialog() == CommonFileDialogResult.Ok && !BFB.FileName.Equals(""))
                {
                    string oldname = ArchiveTreeView.SelectedNode.Text;
                    DirectoryInfo fi = new DirectoryInfo(BFB.FileName);

                    ArchiveTreeView.SelectedNode.Text = fi.Name;
                    int y = 2;
                    string ogname = Path.GetFileNameWithoutExtension(fi.Name);
                    while (Archive.ItemExists(ArchiveTreeView.SelectedNode.FullPath) && Archive[ArchiveTreeView.SelectedNode.FullPath] != Archive[OldPath])
                        ArchiveTreeView.SelectedNode.Text = ogname + $" ({y++})";
                    ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = 0;
                    Archive[OldPath] = null;
                    RARC.Directory dir = new RARC.Directory() { Name = ArchiveTreeView.SelectedNode.Text };
                    Archive[ArchiveTreeView.SelectedNode.FullPath] = dir;
                    dir.CreateFromFolder(BFB.FileName);
                    string currentpath = ArchiveTreeView.SelectedNode.FullPath;
                    int previndex = ArchiveTreeView.SelectedNode.Index;
                    ArchiveTreeView.Nodes.Clear();
                    ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                    ArchiveTreeView.SelectedNode = ArchiveTreeView.Nodes.FindTreeNodeByFullPath(currentpath);
                    TreeNode tmp = ArchiveTreeView.SelectedNode;
                    if (tmp.Parent == null)
                    {
                        ArchiveTreeView.Nodes.Remove(tmp);
                        ArchiveTreeView.Nodes.Insert(previndex, tmp);
                    }
                    else
                    {
                        TreeNode Parent = tmp.Parent;
                        Parent.Nodes.Remove(tmp);
                        Parent.Nodes.Insert(previndex, tmp);
                    }

                    Settings.Default.PreviousAddFilePath = new DirectoryInfo(BFB.FileName).Parent.FullName;
                    Settings.Default.Save();
                    Edited = true;
                    MainToolStripStatusLabel.Text = string.Format(Strings.ReplaceItemMessage, new DirectoryInfo(BFB.FileName).Name, oldname);
                }
            }
            else if (Item is RARC.File file)
            {
                Fileofd.InitialDirectory = Settings.Default.PreviousAddFilePath;
                if (Fileofd.ShowDialog() == DialogResult.OK && Fileofd.FileName != "")
                {
                    string oldname = ArchiveTreeView.SelectedNode.Text;
                    FileInfo fi = new FileInfo(Fileofd.FileName);
                    int imageindex = 2;
                    if (ArchiveImageList.Images.ContainsKey("*" + fi.Extension))
                        imageindex = ArchiveImageList.Images.IndexOfKey("*" + fi.Extension);

                    ArchiveTreeView.SelectedNode.Text = fi.Name;
                    int y = 2;
                    string ogname = Path.GetFileNameWithoutExtension(fi.Name);
                    string ogextension = fi.Extension;
                    while (Archive.ItemExists(ArchiveTreeView.SelectedNode.FullPath) && Archive[ArchiveTreeView.SelectedNode.FullPath] != Archive[OldPath])
                        ArchiveTreeView.SelectedNode.Text = ogname + $" ({y++})" + ogextension;
                    ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = imageindex;
                    Archive[OldPath] = null;
                    Archive[ArchiveTreeView.SelectedNode.FullPath] = new RARC.File(Fileofd.FileName) { Name = ArchiveTreeView.SelectedNode.Text };

                    Settings.Default.PreviousAddFilePath = new FileInfo(Fileofd.FileName).DirectoryName;
                    Settings.Default.Save();
                    Edited = true;
                    MainToolStripStatusLabel.Text = string.Format(Strings.ReplaceItemMessage, new FileInfo(Fileofd.FileName).Name, oldname);
                }
            }
        }
        #endregion

        private void SwitchThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.IsDarkMode = !Settings.Default.IsDarkMode;
            ReloadTheme();
            Settings.Default.Save();
        }

        private void ItemPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode is null || Archive is U8)
            {
                ItemPropertiesToolStripMenuItem.Enabled = false;
                return;
            }
            FilePropertyForm FPF = new FilePropertyForm(ArchiveTreeView, Archive as RARC);
            if (FPF.ShowDialog() == DialogResult.OK)
            {
                if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.File file)
                {
                    int imageindex = 2;
                    if (ArchiveImageList.Images.ContainsKey("*" + file.Extension))
                        imageindex = ArchiveImageList.Images.IndexOfKey("*" + file.Extension);

                    ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = imageindex;
                }
                Edited = true;
                MainToolStripStatusLabel.Text = string.Format(Strings.ItemPropertyUpdateMessage, ArchiveTreeView.SelectedNode.Text);
            }
        }

        private void Yaz0ToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e) => Program.EncodingMode = (byte)Yaz0ToolStripComboBox.SelectedIndex;

        #region MenuStrip Managers
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                if (e.Item is ToolStripMenuItem)
                    e.ArrowColor = Program.ProgramColours.TextColour;
                base.OnRenderArrow(e);
            }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color ButtonSelectedHighlight => Color.Black;

            public override Color ButtonSelectedHighlightBorder => Color.Black;

            public override Color ButtonPressedHighlight => Color.Black;

            public override Color ButtonPressedHighlightBorder => Color.Black;

            public override Color ButtonCheckedHighlight => Color.Black;

            public override Color ButtonCheckedHighlightBorder => Color.Black;

            public override Color ButtonPressedBorder => Color.Black;

            public override Color ButtonSelectedBorder => Color.Black;

            public override Color ButtonCheckedGradientBegin => Color.Black;

            public override Color ButtonCheckedGradientMiddle => Color.Black;

            public override Color ButtonCheckedGradientEnd => Color.Black;

            public override Color ButtonSelectedGradientBegin => Color.Black;

            public override Color ButtonSelectedGradientMiddle => Color.Black;

            public override Color ButtonSelectedGradientEnd => Color.Black;

            public override Color ButtonPressedGradientBegin => Color.Black;

            public override Color ButtonPressedGradientMiddle => Color.Black;

            public override Color ButtonPressedGradientEnd => Color.Black;

            public override Color CheckBackground => Color.Black;

            public override Color CheckSelectedBackground => Color.Black;

            public override Color CheckPressedBackground => Color.Black;

            public override Color GripDark => Color.Black;

            public override Color GripLight => Color.Black;

            public override Color ImageMarginGradientBegin => Color.Black;

            public override Color ImageMarginGradientMiddle => Color.Black;

            public override Color ImageMarginGradientEnd => Color.Black;

            public override Color ImageMarginRevealedGradientBegin => Color.Black;

            public override Color ImageMarginRevealedGradientMiddle => Color.Black;

            public override Color ImageMarginRevealedGradientEnd => Color.Black;

            public override Color MenuStripGradientBegin => Color.Black;

            public override Color MenuStripGradientEnd => Color.Black;

            public override Color MenuItemSelected => Color.Black;

            public override Color MenuItemBorder => Color.Black;

            public override Color MenuBorder => Color.Black;

            public override Color MenuItemSelectedGradientBegin => Color.Black;

            public override Color MenuItemSelectedGradientEnd => Color.Black;

            public override Color MenuItemPressedGradientBegin => Color.Black;

            public override Color MenuItemPressedGradientMiddle => Color.White;

            public override Color MenuItemPressedGradientEnd => Color.Black;

            public override Color RaftingContainerGradientBegin => Color.Black;

            public override Color RaftingContainerGradientEnd => Color.Black;

            public override Color SeparatorDark => Color.Black;

            public override Color SeparatorLight => Color.Black;

            public override Color StatusStripGradientBegin => Color.Black;

            public override Color StatusStripGradientEnd => Color.Black;

            public override Color ToolStripBorder => Color.Black;

            public override Color ToolStripDropDownBackground => Color.Black;

            public override Color ToolStripGradientBegin => Color.Black;

            public override Color ToolStripGradientMiddle => Color.Black;

            public override Color ToolStripGradientEnd => Color.Black;

            public override Color ToolStripContentPanelGradientBegin => Color.Black;

            public override Color ToolStripContentPanelGradientEnd => Color.Black;

            public override Color ToolStripPanelGradientBegin => Color.Black;

            public override Color ToolStripPanelGradientEnd => Color.Black;

            public override Color OverflowButtonGradientBegin => Color.Black;

            public override Color OverflowButtonGradientMiddle => Color.Black;

            public override Color OverflowButtonGradientEnd => Color.Black;
        }
        #endregion

        #endregion

        #region ArchiveTreeview

        #region Private Fields
        private string NodeMap;
        private const int MAPSIZE = 128;
        private StringBuilder NewNodeMap = new StringBuilder(MAPSIZE);
        private readonly SolidBrush ArchiveTreeViewBrush = new SolidBrush(Program.ProgramColours.TextColour);
        #endregion

        #region Helper Methods
        private void DrawLeafTopPlaceholders(TreeNode NodeOver, TreeView Tree)
        {
            Graphics g = Tree.CreateGraphics();

            int NodeOverImageWidth = Tree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            int RightPos = Tree.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};

            g.FillPolygon(ArchiveTreeViewBrush, LeftTriangle);
            g.FillPolygon(ArchiveTreeViewBrush, RightTriangle);
            g.DrawLine(new Pen(Program.ProgramColours.TextColour, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }//eom

        private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop, TreeView Tree)
        {
            Graphics g = Tree.CreateGraphics();

            int NodeOverImageWidth = Tree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            // Once again, we are not dragging to node over, draw the placeholder using the ParentDragDrop bounds
            int LeftPos, RightPos;
            if (ParentDragDrop != null)
                LeftPos = ParentDragDrop.Bounds.Left - (Tree.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
            else
                LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = Tree.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 5)};

            g.FillPolygon(ArchiveTreeViewBrush, LeftTriangle);
            g.FillPolygon(ArchiveTreeViewBrush, RightTriangle);
            g.DrawLine(new Pen(Program.ProgramColours.TextColour, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
        }//eom

        private void DrawFolderTopPlaceholders(TreeNode NodeOver, TreeView Tree)
        {
            Graphics g = Tree.CreateGraphics();
            int NodeOverImageWidth = Tree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

            int LeftPos, RightPos;
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = this.ArchiveTreeView.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(ArchiveTreeViewBrush, LeftTriangle);
            g.FillPolygon(ArchiveTreeViewBrush, RightTriangle);
            g.DrawLine(new Pen(Program.ProgramColours.TextColour, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }//eom
        private void DrawAddToFolderPlaceholder(TreeNode NodeOver, TreeView Tree)
        {
            Graphics g = Tree.CreateGraphics();
            int RightPos = NodeOver.Bounds.Right + 6;
            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

            this.Refresh();
            g.FillPolygon(ArchiveTreeViewBrush, RightTriangle);
        }//eom

        private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
        {
            NewNodeMap.Length = 0;

            if (boolBelowNode)
                NewNodeMap.Insert(0, (int)tnNode.Index + 1);
            else
                NewNodeMap.Insert(0, (int)tnNode.Index);
            TreeNode tnCurNode = tnNode;

            while (tnCurNode.Parent != null)
            {
                tnCurNode = tnCurNode.Parent;

                if (NewNodeMap.Length == 0 && boolBelowNode == true)
                {
                    NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
                }
                else
                {
                    NewNodeMap.Insert(0, tnCurNode.Index + "|");
                }
            }
        }//oem

        private bool SetMapsEqual()
        {
            if (this.NewNodeMap.ToString() == this.NodeMap)
                return true;
            else
            {
                this.NodeMap = this.NewNodeMap.ToString();
                return false;
            }
        }//oem
        #endregion

        private void ArchiveTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!(e.Item is TreeNode TN))
                    return;
                object Item = Archive[TN.FullPath];
                string TempFile = null;
                if (Item is ArchiveFile file)
                {
                    TempFile = Path.Combine(Path.GetTempPath(), file.Name);
                    file.Save(TempFile);
                    DataObject data = new DataObject(DataFormats.FileDrop, new string[1] { TempFile });
                    DoDragDrop(new DataObject(DataFormats.FileDrop, new string[1] { TempFile }), DragDropEffects.Copy);
                    File.Delete(TempFile);
                }
                else if (Item is ArchiveDirectory dir)
                {
                    TempFile = Path.Combine(Path.GetTempPath(), dir.Name);
                    dir.Export(TempFile);
                    DataObject data = new DataObject(DataFormats.FileDrop, new string[1] { TempFile });
                    DoDragDrop(new DataObject(DataFormats.FileDrop, new string[1] { TempFile }), DragDropEffects.Copy);
                    Directory.Delete(TempFile, true);
                }
            }
            else
                DoDragDrop(e.Item, DragDropEffects.Move);

            ArchiveTreeView.Invalidate();
        }

        private void ArchiveTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                if (Archive is null && ((string[])e.Data.GetData(DataFormats.FileDrop, false)).Length == 1)
                {
                    FileAttributes attr = File.GetAttributes(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]);
                    if (attr == FileAttributes.Directory)
                    {
                        NewFromFolder(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]);
                        return;
                    }
                }
                if (Archive is null)
                    return;
                if (string.IsNullOrWhiteSpace(NodeMap))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    AddItemsToArchive(files);
                    MainToolStripStatusLabel.Text = string.Format(Strings.AddedFilesMessage, files.Length);
                    return;
                }
                string[] NodeIndexes = this.NodeMap.Split('|');
                TreeNodeCollection InsertCollection = this.ArchiveTreeView.Nodes;
                for (int i = 0; i < NodeIndexes.Length - 1; i++)
                {
                    InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
                }

                if (InsertCollection != null)
                {
                    AddItemsToArchive((string[])e.Data.GetData(DataFormats.FileDrop, false), Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), InsertCollection);
                }
            }
            else if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && !string.IsNullOrWhiteSpace(NodeMap))
            {
                TreeNode MovingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                string[] NodeIndexes = this.NodeMap.Split('|');
                TreeNodeCollection InsertCollection = this.ArchiveTreeView.Nodes;
                string NewPath = Archive.Root.Name;
                for (int i = 0; i < NodeIndexes.Length - 1; i++)
                {
                    NewPath += "/" + InsertCollection[Int32.Parse(NodeIndexes[i])].Text;
                    InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
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

                    InsertCollection.Insert(Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), (TreeNode)MovingNode.Clone());
                    this.ArchiveTreeView.SelectedNode = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])];
                    string Old = MovingNode.FullPath, New = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])].FullPath;

                    Archive.MoveItem(Old, New);
                    MovingNode.Remove();
                }
            }
        }

        private void ArchiveTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ArchiveTreeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode NodeOver = this.ArchiveTreeView.GetNodeAt(this.ArchiveTreeView.PointToClient(Cursor.Position));
            TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


            // A bit long, but to summarize, process the following code only if the nodeover is null
            // and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
            // to be the last node in the branch (so we can allow drag & drop below a parent branch)
            if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
            {
                int OffsetY = this.ArchiveTreeView.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
                int NodeOverImageWidth = this.ArchiveTreeView.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
                Graphics g = this.ArchiveTreeView.CreateGraphics();

                // Image index of 0 is the Folder Icon
                if (NodeOver.ImageIndex != 0 && NodeOver.ImageIndex != 1)
                {
                    #region Standard Node
                    if (OffsetY < (NodeOver.Bounds.Height / 2))
                    {
                        //this.lblDebug.Text = "top";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.NodeMap = "";
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
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        this.DrawLeafTopPlaceholders(NodeOver, ArchiveTreeView);
                        #endregion
                    }
                    else
                    {
                        //this.lblDebug.Text = "bottom";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Allow drag drop to parent branches
                        TreeNode ParentDragDrop = null;
                        // If the node the mouse is over is the last node of the branch we should allow
                        // the ability to drop the "nodemoving" node BELOW the parent node
                        if (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
                        {
                            int XPos = this.ArchiveTreeView.PointToClient(Cursor.Position).X;
                            if (XPos < NodeOver.Bounds.Left)
                            {
                                ParentDragDrop = NodeOver.Parent;

                                if (XPos < (ParentDragDrop.Bounds.Left - this.ArchiveTreeView.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width))
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
                        this.Refresh();
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
                                this.NodeMap = "";
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
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        this.DrawFolderTopPlaceholders(NodeOver, ArchiveTreeView);
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
                                this.NodeMap = "";
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
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        DrawFolderTopPlaceholders(NodeOver, ArchiveTreeView);
                        #endregion
                    }
                    else
                    {
                        //this.lblDebug.Text = "folder over";

                        if (NodeOver.Nodes.Count > 0)
                        {
                            NodeOver.Expand();
                            //this.Refresh();
                        }
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
                                    this.NodeMap = "";
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
                            this.Refresh();
                            #endregion
                            #region Draw the "add to folder" placeholder
                            DrawAddToFolderPlaceholder(NodeOver, ArchiveTreeView);
                            #endregion
                        }
                    }
                    #endregion
                }
            }
        }

        private void ArchiveTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            this.ArchiveTreeView.SelectedNode = this.ArchiveTreeView.GetNodeAt(e.X, e.Y);
            if (ArchiveTreeView.SelectedNode is null)
                ItemPropertiesToolStripMenuItem.Enabled = false;
        }

        private void ArchiveTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }

        private void ArchiveTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
        }

        private void ArchiveTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Archive is null)
                return;
            NodeMap = null;
            ItemPropertiesToolStripMenuItem.Enabled = false;
            if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory dir)
                MainToolStripStatusLabel.Text = string.Format(Strings.FolderClick, dir.Name, dir.Items.Count);
            else if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.File file)
            {
                MainToolStripStatusLabel.Text = string.Format(Strings.FileClick, file.Name, file.FileData.Length);
                ItemPropertiesToolStripMenuItem.Enabled = true;
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
        
        private void ReloadTheme()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].BackColor = Program.ProgramColours.ControlBackColor;
                Controls[i].ForeColor = Program.ProgramColours.TextColour;
            }
            ForeColor = Program.ProgramColours.TextColour;
            BackColor = Program.ProgramColours.ControlBackColor;
            MainFormMenuStrip.Renderer = Settings.Default.IsDarkMode ? new MyRenderer() : default;
            ArchiveContextMenuStrip.Renderer = Settings.Default.IsDarkMode ? new MyRenderer() : default;
            for (int i = 0; i < FileToolStripMenuItem.DropDownItems.Count; i++)
            {
                FileToolStripMenuItem.DropDownItems[i].BackColor = Program.ProgramColours.WindowColour;
                FileToolStripMenuItem.DropDownItems[i].ForeColor = Program.ProgramColours.TextColour;
            }
            for (int i = 0; i < EditToolStripMenuItem.DropDownItems.Count; i++)
            {
                EditToolStripMenuItem.DropDownItems[i].BackColor = Program.ProgramColours.WindowColour;
                EditToolStripMenuItem.DropDownItems[i].ForeColor = Program.ProgramColours.TextColour;
            }
            for (int i = 0; i < ArchiveContextMenuStrip.Items.Count; i++)
            {
                ArchiveContextMenuStrip.Items[i].BackColor = Program.ProgramColours.WindowColour;
                ArchiveContextMenuStrip.Items[i].ForeColor = Program.ProgramColours.TextColour;
            }
            MainToolStripProgressBar.BackColor = RootNameTextBox.BackColor = Yaz0ToolStripComboBox.BackColor = ArchiveTreeView.BackColor = Program.ProgramColours.WindowColour;
            RootNameTextBox.BorderColor = Program.ProgramColours.BorderColour;
            Yaz0ToolStripComboBox.ForeColor = RootNameTextBox.ForeColor = KeepIDsSyncedCheckBox.ForeColor = Program.ProgramColours.TextColour;
        }

        private void NewFromFolder(string foldername)
        {
            MessageBoxManager.Register();
            MessageBoxManager.No = "RARC";
            MessageBoxManager.Yes = "U8";
            DialogResult dr = MessageBox.Show(Strings.ChooseFormatMessage, Strings.Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
                return;
            if (dr == DialogResult.Yes)
            {
                Archive = new U8();
            }
            else
            {
                Archive = new RARC() { KeepFileIDsSynced = true };
            }
            MessageBoxManager.Unregister();
            Archive.Import(foldername);
            ArchiveTreeView.Nodes.Clear();
            RootNameTextBox.Text = Archive.Root.Name;
            if (Archive is RARC r)
            {
                KeepIDsSyncedCheckBox.Checked = r.KeepFileIDsSynced;
                KeepIDsSyncedCheckBox.Enabled = true;
            }
            else
            {
                KeepIDsSyncedCheckBox.Checked =
                KeepIDsSyncedCheckBox.Enabled = false;
            }
            ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
            Settings.Default.PreviousAddFilePath = new DirectoryInfo(foldername).Parent.FullName;
            Settings.Default.Save();

            Edited = true;
            SetControlsEnabled();
            MainToolStripProgressBar.Value = 100;
            MainToolStripStatusLabel.Text = string.Format(Strings.NewArchiveFromMessage, foldername);
            Text = $"WiiExplorer {Application.ProductVersion} - {Strings.NewArchive}";
        }

        private void OpenArchive(string Filename)
        {
            ofd.FileName = Filename;
            MainToolStripProgressBar.Value = 0;
            bool IsYaz0 = YAZ0.Check(Filename);
            bool IsYay0 = YAY0.Check(Filename);
            if (IsU8())
                Archive = IsYaz0 ? new U8(YAZ0.DecompressToMemoryStream(Filename), Filename) : (IsYay0 ? new U8(YAY0.DecompressToMemoryStream(Filename), Filename) : new U8(Filename));
            else if (IsRARC())
                Archive = IsYaz0 ? new RARC(YAZ0.DecompressToMemoryStream(Filename), Filename) : (IsYay0 ? new RARC(YAY0.DecompressToMemoryStream(Filename), Filename) : new RARC(Filename));
            else
            {
                MessageBox.Show(Strings.InvalidFile, Strings.Warning, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MainToolStripProgressBar.Value = 20;
            ArchiveTreeView.Nodes.Clear();
            ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
            RootNameTextBox.Text = Archive.Root.Name;
            if (Archive is RARC r)
            {
                KeepIDsSyncedCheckBox.Checked = r.KeepFileIDsSynced;
                KeepIDsSyncedCheckBox.Enabled = true;
            }
            else
            {
                KeepIDsSyncedCheckBox.Checked = false;
                KeepIDsSyncedCheckBox.Enabled = false;
            }
            Edited = false;
            SetControlsEnabled();
            MainToolStripProgressBar.Value = 100;
            int Count = Archive.TotalFileCount; //do it here so we don't need to do it twice, as that would be taxing for large archives
            MainToolStripStatusLabel.Text = string.Format(Strings.ArchiveLoadedMessage, Count);
            Text = $"WiiExplorer {Application.ProductVersion} - {new FileInfo(Filename).Name}";
            Settings.Default.PreviousOpenArchivePath = new FileInfo(Filename).DirectoryName;
            Settings.Default.Save();
            if (IsYay0 && Program.EncodingMode != 0x03)
                Program.EncodingMode = 0x03;
            else if (!IsYaz0 && !IsYay0 && Program.EncodingMode != 0x00)
                Program.EncodingMode = 0x00;
            else if (IsYaz0 && Program.EncodingMode != 0x02)
                Program.EncodingMode = 0x01;
            Yaz0ToolStripComboBox.SelectedIndex = Program.EncodingMode;

            bool IsU8()
            {
                Stream arc;
                if (IsYaz0)
                {
                    arc = YAZ0.DecompressToMemoryStream(Filename);
                }
                else if (IsYay0)
                {
                    arc = YAY0.DecompressToMemoryStream(Filename);
                }
                else
                {
                    arc = new FileStream(Filename, FileMode.Open);
                }
                bool Check = arc.ReadString(4) == U8.Magic;
                arc.Close();
                return Check;
            }
            bool IsRARC()
            {
                Stream arc;
                if (IsYaz0)
                {
                    arc = YAZ0.DecompressToMemoryStream(Filename);
                }
                else if (IsYay0)
                {
                    arc = YAY0.DecompressToMemoryStream(Filename);
                }
                else
                {
                    arc = new FileStream(Filename, FileMode.Open);
                }
                bool Check = arc.ReadString(4) == RARC.Magic;
                arc.Close();
                return Check;
            }
        }

        private void SaveArchive(string Filename)
        {
            FileInfo fi = new FileInfo(Filename);
            byte prevencoding = Program.EncodingMode;
            if (fi.Extension.Equals(".szp") && Program.EncodingMode != 0x03 && MessageBox.Show(Strings.SZPMismatch, Strings.EncodingMismatch, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Program.EncodingMode = 0x03;
            else if (fi.Extension.Equals(".szs") && Program.EncodingMode != 0x01 && Program.EncodingMode != 0x02 && MessageBox.Show(Strings.SZSMismatch, Strings.EncodingMismatch, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Program.EncodingMode = 0x01;

            Yaz0ToolStripComboBox.SelectedIndex = Program.EncodingMode;
            MainToolStripProgressBar.Value = 0;
            Archive.FromTreeNode(ArchiveTreeView);
            MainToolStripProgressBar.Value = 70;
            Archive.Save(Filename);
            long UncompressedFilesize = File.ReadAllBytes(Filename).Length;
            double ETA = UncompressedFilesize * (Program.EncodingMode == 0x02 ? Settings.Default.ElapsedTimeFast : (Program.EncodingMode == 0x03 ? Settings.Default.ElapsedTimeYAY0 : Settings.Default.ElapsedTimeStrong));
            Stopwatch timer = new Stopwatch();
            timer.Start();
            if (Program.EncodingMode != 0x00)
                EncodingBackgroundWorker.RunWorkerAsync(Filename);
            SetControlsEnabled(false, true);
            ItemPropertiesToolStripMenuItem.Enabled = false;
            while (EncodingBackgroundWorker.IsBusy)
            {
                MainToolStripStatusLabel.Text = string.Format(Strings.EncodingMessage, (Program.EncodingMode == 2 ? $"{Strings.Fast} " : "") + (Program.EncodingMode == 3 ? "Yay0":"Yaz0"), timer.Elapsed.ToString("mm\\:ss"), TimeSpan.FromMilliseconds(ETA).ToString("mm\\:ss"));
                Application.DoEvents();
            }
            timer.Stop();

            if (Program.EncodingMode == 0x01)
                Settings.Default.ElapsedTimeStrong = (double)timer.ElapsedMilliseconds / (double)UncompressedFilesize;
            else if (Program.EncodingMode == 0x02)
                Settings.Default.ElapsedTimeFast = (double)timer.ElapsedMilliseconds / (double)UncompressedFilesize;
            else if (Program.EncodingMode == 0x03)
                Settings.Default.ElapsedTimeYAY0 = (double)timer.ElapsedMilliseconds / (double)UncompressedFilesize;

            Edited = false;
            MainToolStripProgressBar.Value = 100;
            MainToolStripStatusLabel.Text = string.Format(Strings.SaveCompleteMessage, $"{(Program.EncodingMode != 0 ? $" ({timer.Elapsed.ToString("mm\\:ss")})" : "")}");
            SetControlsEnabled(affectall:true);
            Text = $"WiiExplorer {Application.ProductVersion} - {new FileInfo(Filename).Name}";
            Settings.Default.PreviousSaveArchivePath = new FileInfo(Filename).DirectoryName;
            Settings.Default.Save();
            Program.EncodingMode = prevencoding;
            Yaz0ToolStripComboBox.SelectedIndex = Program.EncodingMode;
        }
        
        private void AddItemsToArchive(string[] FileNames)
        {
            Edited = true;
            for (int i = 0; i < FileNames.Length; i++)
            {
                if (!File.Exists(FileNames[i]) && !Directory.Exists(FileNames[i]))
                    continue;
                FileAttributes attr = File.GetAttributes(FileNames[i]);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string ogname = new DirectoryInfo(FileNames[i]).Name;
                    TreeNode NewTreeNode = new TreeNode(ogname) { ImageIndex = 0, SelectedImageIndex = 0 };

                    //SelectedNode is NULL, put the new file on the root
                    if (ArchiveTreeView.SelectedNode == null)
                        ArchiveTreeView.Nodes.Add(NewTreeNode);
                    //Determine where to put it otherwise
                    else
                    {
                        if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory)
                            ArchiveTreeView.SelectedNode.Nodes.Add(NewTreeNode);
                        else if (ArchiveTreeView.SelectedNode.Parent == null)
                            ArchiveTreeView.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                        else
                            ArchiveTreeView.SelectedNode.Parent.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                    }

                    ArchiveTreeView.SelectedNode = NewTreeNode;
                    ArchiveDirectory dir;
                    if (Archive is RARC r)
                    {
                        dir = new RARC.Directory(FileNames[i], r);
                    }
                    else
                    {
                        dir = new ArchiveDirectory(FileNames[i], Archive); //U8 just uses the default base class
                    }
                    int y = 2;
                    while (Archive.ItemExists(NewTreeNode.FullPath))
                    {
                        NewTreeNode.Text = ogname + $" ({y++})";
                    }
                    dir.Name = NewTreeNode.Text;
                    Archive[NewTreeNode.FullPath] = dir;

                    ArchiveTreeView.Nodes.Clear();
                    ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                    ArchiveTreeView.SelectedNode = NewTreeNode;
                }
                else
                {
                    FileInfo fi = new FileInfo(FileNames[i]);
                    int imageindex = 2;
                    if (ArchiveImageList.Images.ContainsKey("*" + fi.Extension))
                        imageindex = ArchiveImageList.Images.IndexOfKey("*" + fi.Extension);

                    RARC.File CurrentFile = new RARC.File(FileNames[i]);
                    TreeNode NewTreeNode = new TreeNode(fi.Name) { ImageIndex = imageindex, SelectedImageIndex = imageindex };

                    //SelectedNode is NULL, put the new file on the root
                    if (ArchiveTreeView.SelectedNode == null)
                        ArchiveTreeView.Nodes.Add(NewTreeNode);
                    //Determine where to put it otherwise
                    else
                    {
                        if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory dir)
                            ArchiveTreeView.SelectedNode.Nodes.Add(NewTreeNode);
                        else if (ArchiveTreeView.SelectedNode.Parent == null)
                            ArchiveTreeView.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                        else
                            ArchiveTreeView.SelectedNode.Parent.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, NewTreeNode);
                    }
                    int y = 2;
                    string ogname = Path.GetFileNameWithoutExtension(FileNames[i]);
                    string ogextension = fi.Extension;
                    while (Archive.ItemExists(NewTreeNode.FullPath))
                        NewTreeNode.Text = ogname+$" ({y++})"+ogextension;
                    CurrentFile.Name = NewTreeNode.Text;
                    Archive[NewTreeNode.FullPath] = CurrentFile;
                }
            }
        }

        private void AddItemsToArchive(string[] FileNames, int Index, TreeNodeCollection InsertCollection)
        {
            Edited = true;
            for (int i = 0; i < FileNames.Length; i++)
            {
                if (!File.Exists(FileNames[i]) && !Directory.Exists(FileNames[i]))
                    continue;

                //detect whether its a directory or file
                FileAttributes attr = File.GetAttributes(FileNames[i]);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string ogname = new DirectoryInfo(FileNames[i]).Name;
                    TreeNode NewTreeNode = new TreeNode(ogname) { ImageIndex = 0, SelectedImageIndex = 0 };
                    
                    InsertCollection.Insert(Index++, NewTreeNode);

                    ArchiveTreeView.SelectedNode = NewTreeNode;
                    ArchiveDirectory dir;
                    if (Archive is RARC r)
                    {
                        dir = new RARC.Directory(FileNames[i], r);
                    }
                    else
                    {
                        dir = new ArchiveDirectory(FileNames[i], Archive); //U8 just uses the default base class
                    }
                    int y = 2;
                    while (Archive.ItemExists(NewTreeNode.FullPath))
                    {
                        NewTreeNode.Text = ogname + $" ({y++})";
                    }
                    dir.Name = NewTreeNode.Text;
                    Archive[NewTreeNode.FullPath] = dir;

                    dir.Parent.ReOrderDirectory(InsertCollection);
                    ArchiveTreeView.Nodes.Clear();
                    ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                    ArchiveTreeView.SelectedNode = NewTreeNode;
                }
                else
                {
                    FileInfo fi = new FileInfo(FileNames[i]);
                    int imageindex = 2;
                    if (ArchiveImageList.Images.ContainsKey("*" + fi.Extension))
                        imageindex = ArchiveImageList.Images.IndexOfKey("*" + fi.Extension);

                    RARC.File CurrentFile = new RARC.File(FileNames[i]);
                    TreeNode NewTreeNode = new TreeNode(fi.Name) { ImageIndex = imageindex, SelectedImageIndex = imageindex };

                    InsertCollection.Insert(Index++, NewTreeNode);
                    int y = 2;
                    string ogname = Path.GetFileNameWithoutExtension(FileNames[i]);
                    string ogextension = fi.Extension;
                    while (Archive.ItemExists(NewTreeNode.FullPath))
                        NewTreeNode.Text = ogname + $" ({y++})" + ogextension;
                    CurrentFile.Name = NewTreeNode.Text;
                    Archive[NewTreeNode.FullPath] = CurrentFile;
                }
            }
        }

        private void ExportArchiveFile(dynamic Item)
        {
            if (Item == null)
                return;

            ExportFBD.SelectedPath = Item.Name;
            if (Item is RARC.Directory)
            {
                //Exportsfd.Filter = "Directory|directory";

                if (ExportFBD.ShowDialog() == DialogResult.OK && ExportFBD.SelectedPath != "")
                {
                    FileInfoEx.CreateDirectoryIfNotExist(ExportFBD.SelectedPath);

                    Item.Export(ExportFBD.SelectedPath);
                    MainToolStripStatusLabel.Text = string.Format(Strings.ItemSavedMessage, Item.Name);
                    Settings.Default.PreviousExportPath = new DirectoryInfo(ExportFBD.SelectedPath).Parent.FullName;
                    Settings.Default.Save();
                }
            }
            else
            {
                string ext = Item.Extension;
                ext = ext + "|*" + ext;
                for (int i = 0; i < KnownExtensions.Count; i++)
                {
                    if (KnownExtensions[i].Split('|')[1].Equals(ext.Split('|')[1]))
                    {
                        ext = KnownExtensions[i];
                        break;
                    }
                }
                Exportsfd.Filter = ext;
                Exportsfd.InitialDirectory = Settings.Default.PreviousExportPath;
                if (Exportsfd.ShowDialog() == DialogResult.OK && Exportsfd.FileName != "")
                {
                    Item.Save(Exportsfd.FileName);
                    MainToolStripStatusLabel.Text = string.Format(Strings.ItemSavedMessage, Item.Name);

                    Settings.Default.PreviousExportPath = new FileInfo(Exportsfd.FileName).DirectoryName;
                    Settings.Default.Save();
                }
            }
        }
    }

    public static class ControlEx
    {
        [DebuggerStepThrough]
        public static void SetDoubleBuffered(this Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
    }
}