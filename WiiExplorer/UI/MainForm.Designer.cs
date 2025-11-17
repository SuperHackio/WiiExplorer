using DarkModeForms;
using System.ComponentModel;

namespace WiiExplorer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            MainFormMenuStrip = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            NewToolStripMenuItem = new ToolStripMenuItem();
            NewFromFolderToolStripMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            SaveAsToolStripMenuItem = new ToolStripMenuItem();
            EditToolStripMenuItem = new ToolStripMenuItem();
            AddFileToolStripMenuItem = new ToolStripMenuItem();
            AddFolderToolStripMenuItem = new ToolStripMenuItem();
            CreateEmptyFolderToolStripMenuItem = new ToolStripMenuItem();
            DeleteSelectedToolStripMenuItem = new ToolStripMenuItem();
            RenameSelectedToolStripMenuItem = new ToolStripMenuItem();
            ReplaceSelectedToolStripMenuItem = new ToolStripMenuItem();
            ExportSelectedToolStripMenuItem = new ToolStripMenuItem();
            ExportAllToolStripMenuItem = new ToolStripMenuItem();
            Yaz0ToolStripComboBox = new ToolStripComboBox();
            FilePropertiesToolStripMenuItem = new ToolStripMenuItem();
            SettingsToolStripMenuItem = new ToolStripMenuItem();
            ThemeToolStripComboBox = new ToolStripColorComboBox();
            PaddingToolStripColorComboBox = new ToolStripColorComboBox();
            AutoYaz0ToolStripColorComboBox = new ToolStripColorComboBox();
            RootPanel = new Panel();
            RootNameTextBox = new ColorTextBox();
            KeepIDsSyncedCheckBox = new CheckBox();
            RootNameLabel = new Label();
            MainFormStatusStrip = new StatusStrip();
            MainToolStripProgressBar = new ToolStripProgressBar();
            MainToolStripStatusLabel = new ToolStripStatusLabel();
            ArchiveTreeView = new TreeView();
            ArchiveContextMenuStrip = new ContextMenuStrip(components);
            ContextAddFileToolStripMenuItem = new ToolStripMenuItem();
            ContextAddFolderToolStripMenuItem = new ToolStripMenuItem();
            ContextCreateEmptyFolderToolStripMenuItem = new ToolStripMenuItem();
            ContextDeleteSelectedToolStripMenuItem = new ToolStripMenuItem();
            ContextRenameToolStripMenuItem = new ToolStripMenuItem();
            ContextReplaceSelectedToolStripMenuItem = new ToolStripMenuItem();
            ContextExportSelectedToolStripMenuItem = new ToolStripMenuItem();
            ContextExportAllToolStripMenuItem = new ToolStripMenuItem();
            EncodingBackgroundWorker = new BackgroundWorker();
            MainFormMenuStrip.SuspendLayout();
            RootPanel.SuspendLayout();
            MainFormStatusStrip.SuspendLayout();
            ArchiveContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            MainFormMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, EditToolStripMenuItem, Yaz0ToolStripComboBox, FilePropertiesToolStripMenuItem, SettingsToolStripMenuItem });
            MainFormMenuStrip.Location = new Point(0, 0);
            MainFormMenuStrip.Name = "MainFormMenuStrip";
            MainFormMenuStrip.Size = new Size(464, 27);
            MainFormMenuStrip.TabIndex = 0;
            MainFormMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewToolStripMenuItem, NewFromFolderToolStripMenuItem, OpenToolStripMenuItem, SaveToolStripMenuItem, SaveAsToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(37, 23);
            FileToolStripMenuItem.Text = "&File";
            // 
            // NewToolStripMenuItem
            // 
            NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            NewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            NewToolStripMenuItem.Size = new Size(240, 22);
            NewToolStripMenuItem.Text = "New";
            NewToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            // 
            // NewFromFolderToolStripMenuItem
            // 
            NewFromFolderToolStripMenuItem.Name = "NewFromFolderToolStripMenuItem";
            NewFromFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.N;
            NewFromFolderToolStripMenuItem.Size = new Size(240, 22);
            NewFromFolderToolStripMenuItem.Text = "New From Folder";
            NewFromFolderToolStripMenuItem.Click += NewFromFolderToolStripMenuItem_Click;
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            OpenToolStripMenuItem.Size = new Size(240, 22);
            OpenToolStripMenuItem.Text = "Open";
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Enabled = false;
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SaveToolStripMenuItem.Size = new Size(240, 22);
            SaveToolStripMenuItem.Text = "Save";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // SaveAsToolStripMenuItem
            // 
            SaveAsToolStripMenuItem.Enabled = false;
            SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            SaveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            SaveAsToolStripMenuItem.Size = new Size(240, 22);
            SaveAsToolStripMenuItem.Text = "Save As";
            SaveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // EditToolStripMenuItem
            // 
            EditToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AddFileToolStripMenuItem, AddFolderToolStripMenuItem, CreateEmptyFolderToolStripMenuItem, DeleteSelectedToolStripMenuItem, RenameSelectedToolStripMenuItem, ReplaceSelectedToolStripMenuItem, ExportSelectedToolStripMenuItem, ExportAllToolStripMenuItem });
            EditToolStripMenuItem.Enabled = false;
            EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            EditToolStripMenuItem.Size = new Size(39, 23);
            EditToolStripMenuItem.Text = "&Edit";
            // 
            // AddFileToolStripMenuItem
            // 
            AddFileToolStripMenuItem.Enabled = false;
            AddFileToolStripMenuItem.Name = "AddFileToolStripMenuItem";
            AddFileToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            AddFileToolStripMenuItem.Size = new Size(243, 22);
            AddFileToolStripMenuItem.Text = "Add File(s)";
            AddFileToolStripMenuItem.Click += AddFileToolStripMenuItem_Click;
            // 
            // AddFolderToolStripMenuItem
            // 
            AddFolderToolStripMenuItem.Enabled = false;
            AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            AddFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.A;
            AddFolderToolStripMenuItem.Size = new Size(243, 22);
            AddFolderToolStripMenuItem.Text = "Add Empty Folder";
            AddFolderToolStripMenuItem.Click += AddFolderToolStripMenuItem_Click;
            // 
            // CreateEmptyFolderToolStripMenuItem
            // 
            CreateEmptyFolderToolStripMenuItem.Enabled = false;
            CreateEmptyFolderToolStripMenuItem.Name = "CreateEmptyFolderToolStripMenuItem";
            CreateEmptyFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
            CreateEmptyFolderToolStripMenuItem.Size = new Size(243, 22);
            CreateEmptyFolderToolStripMenuItem.Text = "Import Folder";
            CreateEmptyFolderToolStripMenuItem.Click += CreateEmptyFolderToolStripMenuItem_Click;
            // 
            // DeleteSelectedToolStripMenuItem
            // 
            DeleteSelectedToolStripMenuItem.Enabled = false;
            DeleteSelectedToolStripMenuItem.Name = "DeleteSelectedToolStripMenuItem";
            DeleteSelectedToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Delete;
            DeleteSelectedToolStripMenuItem.Size = new Size(243, 22);
            DeleteSelectedToolStripMenuItem.Text = "Delete Selected";
            DeleteSelectedToolStripMenuItem.Click += DeleteSelectedToolStripMenuItem_Click;
            // 
            // RenameSelectedToolStripMenuItem
            // 
            RenameSelectedToolStripMenuItem.Enabled = false;
            RenameSelectedToolStripMenuItem.Name = "RenameSelectedToolStripMenuItem";
            RenameSelectedToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            RenameSelectedToolStripMenuItem.Size = new Size(243, 22);
            RenameSelectedToolStripMenuItem.Text = "Rename Selected";
            RenameSelectedToolStripMenuItem.Click += RenameSelectedToolStripMenuItem_Click;
            // 
            // ReplaceSelectedToolStripMenuItem
            // 
            ReplaceSelectedToolStripMenuItem.Enabled = false;
            ReplaceSelectedToolStripMenuItem.Name = "ReplaceSelectedToolStripMenuItem";
            ReplaceSelectedToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.R;
            ReplaceSelectedToolStripMenuItem.Size = new Size(243, 22);
            ReplaceSelectedToolStripMenuItem.Text = "Replace Selected";
            ReplaceSelectedToolStripMenuItem.Click += ReplaceSelectedToolStripMenuItem_Click;
            // 
            // ExportSelectedToolStripMenuItem
            // 
            ExportSelectedToolStripMenuItem.Enabled = false;
            ExportSelectedToolStripMenuItem.Name = "ExportSelectedToolStripMenuItem";
            ExportSelectedToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.S;
            ExportSelectedToolStripMenuItem.Size = new Size(243, 22);
            ExportSelectedToolStripMenuItem.Text = "Export Selected";
            ExportSelectedToolStripMenuItem.Click += ExportSelectedToolStripMenuItem_Click;
            // 
            // ExportAllToolStripMenuItem
            // 
            ExportAllToolStripMenuItem.Enabled = false;
            ExportAllToolStripMenuItem.Name = "ExportAllToolStripMenuItem";
            ExportAllToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Shift | Keys.S;
            ExportAllToolStripMenuItem.Size = new Size(243, 22);
            ExportAllToolStripMenuItem.Text = "Export All";
            ExportAllToolStripMenuItem.Click += ExportAllToolStripMenuItem_Click;
            // 
            // Yaz0ToolStripComboBox
            // 
            Yaz0ToolStripComboBox.Alignment = ToolStripItemAlignment.Right;
            Yaz0ToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Yaz0ToolStripComboBox.DropDownWidth = 88;
            Yaz0ToolStripComboBox.FlatStyle = FlatStyle.Flat;
            Yaz0ToolStripComboBox.Items.AddRange(new object[] { "Off", "Yaz0 Strong", "Yaz0 Fast", "Yaz0 Official", "Yay0 Strong" });
            Yaz0ToolStripComboBox.Name = "Yaz0ToolStripComboBox";
            Yaz0ToolStripComboBox.Size = new Size(100, 23);
            Yaz0ToolStripComboBox.SelectedIndexChanged += Yaz0ToolStripComboBox_SelectedIndexChanged;
            // 
            // FilePropertiesToolStripMenuItem
            // 
            FilePropertiesToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            FilePropertiesToolStripMenuItem.Enabled = false;
            FilePropertiesToolStripMenuItem.Name = "FilePropertiesToolStripMenuItem";
            FilePropertiesToolStripMenuItem.ShowShortcutKeys = false;
            FilePropertiesToolStripMenuItem.Size = new Size(93, 23);
            FilePropertiesToolStripMenuItem.Text = "File Properties";
            FilePropertiesToolStripMenuItem.Click += FilePropertiesToolStripMenuItem_Click;
            // 
            // SettingsToolStripMenuItem
            // 
            SettingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ThemeToolStripComboBox, PaddingToolStripColorComboBox, AutoYaz0ToolStripColorComboBox });
            SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            SettingsToolStripMenuItem.Size = new Size(61, 23);
            SettingsToolStripMenuItem.Text = "&Settings";
            // 
            // ThemeToolStripComboBox
            // 
            ThemeToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ThemeToolStripComboBox.FlatStyle = FlatStyle.Flat;
            ThemeToolStripComboBox.Items.AddRange(new object[] { "Light Theme", "Dark Theme" });
            ThemeToolStripComboBox.Name = "ThemeToolStripComboBox";
            ThemeToolStripComboBox.Size = new Size(150, 23);
            ThemeToolStripComboBox.ToolTipText = "Changes the program's visuals.";
            ThemeToolStripComboBox.SelectedIndexChanged += ThemeToolStripComboBox_SelectedIndexChanged;
            // 
            // PaddingToolStripColorComboBox
            // 
            PaddingToolStripColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PaddingToolStripColorComboBox.FlatStyle = FlatStyle.Flat;
            PaddingToolStripColorComboBox.Items.AddRange(new object[] { "No Padding", "16 Before Encode", "16 After Encode", "32 Before Encode", "32 After Encode" });
            PaddingToolStripColorComboBox.Name = "PaddingToolStripColorComboBox";
            PaddingToolStripColorComboBox.Size = new Size(150, 23);
            PaddingToolStripColorComboBox.ToolTipText = resources.GetString("PaddingToolStripColorComboBox.ToolTipText");
            PaddingToolStripColorComboBox.SelectedIndexChanged += PaddingToolStripColorComboBox_SelectedIndexChanged;
            // 
            // AutoYaz0ToolStripColorComboBox
            // 
            AutoYaz0ToolStripColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            AutoYaz0ToolStripColorComboBox.FlatStyle = FlatStyle.Flat;
            AutoYaz0ToolStripColorComboBox.Items.AddRange(new object[] { "Do Not Auto-Select", "Auto-Select Encoding" });
            AutoYaz0ToolStripColorComboBox.Name = "AutoYaz0ToolStripColorComboBox";
            AutoYaz0ToolStripColorComboBox.Size = new Size(150, 23);
            AutoYaz0ToolStripColorComboBox.ToolTipText = "Determines if the program will try to\r\nautomatically decide what encoding\r\nthe current file should be using.\r\n\r\nDefault is Auto-Select Encoding";
            AutoYaz0ToolStripColorComboBox.SelectedIndexChanged += AutoYaz0ToolStripColorComboBox_SelectedIndexChanged;
            // 
            // RootPanel
            // 
            RootPanel.Controls.Add(RootNameTextBox);
            RootPanel.Controls.Add(KeepIDsSyncedCheckBox);
            RootPanel.Controls.Add(RootNameLabel);
            RootPanel.Dock = DockStyle.Top;
            RootPanel.Location = new Point(0, 27);
            RootPanel.Name = "RootPanel";
            RootPanel.Size = new Size(464, 20);
            RootPanel.TabIndex = 1;
            // 
            // RootNameTextBox
            // 
            RootNameTextBox.Dock = DockStyle.Fill;
            RootNameTextBox.Enabled = false;
            RootNameTextBox.Location = new Point(64, 0);
            RootNameTextBox.Name = "RootNameTextBox";
            RootNameTextBox.Size = new Size(267, 20);
            RootNameTextBox.TabIndex = 0;
            RootNameTextBox.TextChanged += RootNameTextBox_TextChanged;
            // 
            // KeepIDsSyncedCheckBox
            // 
            KeepIDsSyncedCheckBox.AutoSize = true;
            KeepIDsSyncedCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            KeepIDsSyncedCheckBox.Checked = true;
            KeepIDsSyncedCheckBox.CheckState = CheckState.Checked;
            KeepIDsSyncedCheckBox.Dock = DockStyle.Right;
            KeepIDsSyncedCheckBox.Enabled = false;
            KeepIDsSyncedCheckBox.Location = new Point(331, 0);
            KeepIDsSyncedCheckBox.Name = "KeepIDsSyncedCheckBox";
            KeepIDsSyncedCheckBox.Size = new Size(133, 20);
            KeepIDsSyncedCheckBox.TabIndex = 2;
            KeepIDsSyncedCheckBox.Text = "Auto-Calculate File IDs";
            KeepIDsSyncedCheckBox.UseVisualStyleBackColor = true;
            // 
            // RootNameLabel
            // 
            RootNameLabel.AutoSize = true;
            RootNameLabel.Dock = DockStyle.Left;
            RootNameLabel.Location = new Point(0, 0);
            RootNameLabel.Name = "RootNameLabel";
            RootNameLabel.Size = new Size(64, 13);
            RootNameLabel.TabIndex = 0;
            RootNameLabel.Text = "Root Name:";
            // 
            // MainFormStatusStrip
            // 
            MainFormStatusStrip.Items.AddRange(new ToolStripItem[] { MainToolStripProgressBar, MainToolStripStatusLabel });
            MainFormStatusStrip.Location = new Point(0, 329);
            MainFormStatusStrip.Name = "MainFormStatusStrip";
            MainFormStatusStrip.Size = new Size(464, 22);
            MainFormStatusStrip.TabIndex = 2;
            MainFormStatusStrip.Text = "statusStrip1";
            // 
            // MainToolStripProgressBar
            // 
            MainToolStripProgressBar.Name = "MainToolStripProgressBar";
            MainToolStripProgressBar.Size = new Size(100, 16);
            // 
            // MainToolStripStatusLabel
            // 
            MainToolStripStatusLabel.Name = "MainToolStripStatusLabel";
            MainToolStripStatusLabel.Size = new Size(347, 17);
            MainToolStripStatusLabel.Spring = true;
            MainToolStripStatusLabel.Text = "No File Loaded.";
            MainToolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ArchiveTreeView
            // 
            ArchiveTreeView.AllowDrop = true;
            ArchiveTreeView.BorderStyle = BorderStyle.None;
            ArchiveTreeView.Dock = DockStyle.Fill;
            ArchiveTreeView.FullRowSelect = true;
            ArchiveTreeView.HideSelection = false;
            ArchiveTreeView.Location = new Point(0, 47);
            ArchiveTreeView.Name = "ArchiveTreeView";
            ArchiveTreeView.PathSeparator = "/";
            ArchiveTreeView.Size = new Size(464, 282);
            ArchiveTreeView.TabIndex = 3;
            ArchiveTreeView.BeforeCollapse += ArchiveTreeView_BeforeCollapse;
            ArchiveTreeView.BeforeExpand += ArchiveTreeView_BeforeExpand;
            ArchiveTreeView.ItemDrag += ArchiveTreeView_ItemDrag;
            ArchiveTreeView.AfterSelect += ArchiveTreeView_AfterSelect;
            ArchiveTreeView.NodeMouseClick += ArchiveTreeView_NodeMouseClick;
            ArchiveTreeView.DragDrop += ArchiveTreeView_DragDrop;
            ArchiveTreeView.DragEnter += ArchiveTreeView_DragEnter;
            ArchiveTreeView.DragOver += ArchiveTreeView_DragOver;
            ArchiveTreeView.MouseDown += ArchiveTreeView_MouseDown;
            // 
            // ArchiveContextMenuStrip
            // 
            ArchiveContextMenuStrip.Items.AddRange(new ToolStripItem[] { ContextAddFileToolStripMenuItem, ContextAddFolderToolStripMenuItem, ContextCreateEmptyFolderToolStripMenuItem, ContextDeleteSelectedToolStripMenuItem, ContextRenameToolStripMenuItem, ContextReplaceSelectedToolStripMenuItem, ContextExportSelectedToolStripMenuItem, ContextExportAllToolStripMenuItem });
            ArchiveContextMenuStrip.Name = "ArchiveContextMenuStrip";
            ArchiveContextMenuStrip.Size = new Size(244, 180);
            // 
            // ContextAddFileToolStripMenuItem
            // 
            ContextAddFileToolStripMenuItem.Name = "ContextAddFileToolStripMenuItem";
            ContextAddFileToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            ContextAddFileToolStripMenuItem.Size = new Size(243, 22);
            ContextAddFileToolStripMenuItem.Text = "Add File(s)";
            ContextAddFileToolStripMenuItem.Click += AddFileToolStripMenuItem_Click;
            // 
            // ContextAddFolderToolStripMenuItem
            // 
            ContextAddFolderToolStripMenuItem.Name = "ContextAddFolderToolStripMenuItem";
            ContextAddFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.A;
            ContextAddFolderToolStripMenuItem.Size = new Size(243, 22);
            ContextAddFolderToolStripMenuItem.Text = "Add Empty Folder";
            ContextAddFolderToolStripMenuItem.Click += AddFolderToolStripMenuItem_Click;
            // 
            // ContextCreateEmptyFolderToolStripMenuItem
            // 
            ContextCreateEmptyFolderToolStripMenuItem.Name = "ContextCreateEmptyFolderToolStripMenuItem";
            ContextCreateEmptyFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
            ContextCreateEmptyFolderToolStripMenuItem.Size = new Size(243, 22);
            ContextCreateEmptyFolderToolStripMenuItem.Text = "Import Folder";
            ContextCreateEmptyFolderToolStripMenuItem.Click += CreateEmptyFolderToolStripMenuItem_Click;
            // 
            // ContextDeleteSelectedToolStripMenuItem
            // 
            ContextDeleteSelectedToolStripMenuItem.Name = "ContextDeleteSelectedToolStripMenuItem";
            ContextDeleteSelectedToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Delete;
            ContextDeleteSelectedToolStripMenuItem.Size = new Size(243, 22);
            ContextDeleteSelectedToolStripMenuItem.Text = "Delete Selected";
            ContextDeleteSelectedToolStripMenuItem.Click += DeleteSelectedToolStripMenuItem_Click;
            // 
            // ContextRenameToolStripMenuItem
            // 
            ContextRenameToolStripMenuItem.Name = "ContextRenameToolStripMenuItem";
            ContextRenameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            ContextRenameToolStripMenuItem.Size = new Size(243, 22);
            ContextRenameToolStripMenuItem.Text = "Rename Selected";
            ContextRenameToolStripMenuItem.Click += RenameSelectedToolStripMenuItem_Click;
            // 
            // ContextReplaceSelectedToolStripMenuItem
            // 
            ContextReplaceSelectedToolStripMenuItem.Name = "ContextReplaceSelectedToolStripMenuItem";
            ContextReplaceSelectedToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.R;
            ContextReplaceSelectedToolStripMenuItem.Size = new Size(243, 22);
            ContextReplaceSelectedToolStripMenuItem.Text = "Replace Selected";
            ContextReplaceSelectedToolStripMenuItem.Click += ReplaceSelectedToolStripMenuItem_Click;
            // 
            // ContextExportSelectedToolStripMenuItem
            // 
            ContextExportSelectedToolStripMenuItem.Name = "ContextExportSelectedToolStripMenuItem";
            ContextExportSelectedToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.S;
            ContextExportSelectedToolStripMenuItem.Size = new Size(243, 22);
            ContextExportSelectedToolStripMenuItem.Text = "Export Selected";
            ContextExportSelectedToolStripMenuItem.Click += ExportSelectedToolStripMenuItem_Click;
            // 
            // ContextExportAllToolStripMenuItem
            // 
            ContextExportAllToolStripMenuItem.Name = "ContextExportAllToolStripMenuItem";
            ContextExportAllToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Shift | Keys.S;
            ContextExportAllToolStripMenuItem.Size = new Size(243, 22);
            ContextExportAllToolStripMenuItem.Text = "Export All";
            ContextExportAllToolStripMenuItem.Click += ExportAllToolStripMenuItem_Click;
            // 
            // EncodingBackgroundWorker
            // 
            EncodingBackgroundWorker.WorkerReportsProgress = true;
            EncodingBackgroundWorker.WorkerSupportsCancellation = true;
            EncodingBackgroundWorker.DoWork += EncodingBackgroundWorker_DoWork;
            EncodingBackgroundWorker.ProgressChanged += EncodingBackgroundWorker_ProgressChanged;
            EncodingBackgroundWorker.RunWorkerCompleted += EncodingBackgroundWorker_RunWorkerCompleted;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 351);
            Controls.Add(ArchiveTreeView);
            Controls.Add(MainFormStatusStrip);
            Controls.Add(RootPanel);
            Controls.Add(MainFormMenuStrip);
            Font = new Font("Microsoft Sans Serif", 8.25F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = MainFormMenuStrip;
            MinimumSize = new Size(480, 390);
            Name = "MainForm";
            Text = "WiiExplorer";
            FormClosing += MainForm_FormClosing;
            MainFormMenuStrip.ResumeLayout(false);
            MainFormMenuStrip.PerformLayout();
            RootPanel.ResumeLayout(false);
            RootPanel.PerformLayout();
            MainFormStatusStrip.ResumeLayout(false);
            MainFormStatusStrip.PerformLayout();
            ArchiveContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainFormMenuStrip;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem NewToolStripMenuItem;
        private ToolStripMenuItem NewFromFolderToolStripMenuItem;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem SaveAsToolStripMenuItem;
        private ToolStripMenuItem EditToolStripMenuItem;
        private ToolStripMenuItem AddFileToolStripMenuItem;
        private ToolStripMenuItem AddFolderToolStripMenuItem;
        private ToolStripMenuItem CreateEmptyFolderToolStripMenuItem;
        private ToolStripMenuItem DeleteSelectedToolStripMenuItem;
        private ToolStripMenuItem RenameSelectedToolStripMenuItem;
        private ToolStripMenuItem ExportSelectedToolStripMenuItem;
        private ToolStripMenuItem ExportAllToolStripMenuItem;
        private ToolStripMenuItem ReplaceSelectedToolStripMenuItem;
        private ToolStripComboBox Yaz0ToolStripComboBox;
        private ToolStripMenuItem FilePropertiesToolStripMenuItem;
        private Panel RootPanel;
        private ColorTextBox RootNameTextBox;
        private CheckBox KeepIDsSyncedCheckBox;
        private Label RootNameLabel;
        private StatusStrip MainFormStatusStrip;
        private ToolStripProgressBar MainToolStripProgressBar;
        private ToolStripStatusLabel MainToolStripStatusLabel;
        private TreeView ArchiveTreeView;
        private ToolStripMenuItem SettingsToolStripMenuItem;
        private ToolStripColorComboBox ThemeToolStripComboBox;
        private ContextMenuStrip ArchiveContextMenuStrip;
        private ToolStripMenuItem ContextAddFileToolStripMenuItem;
        private ToolStripMenuItem ContextAddFolderToolStripMenuItem;
        private ToolStripMenuItem ContextCreateEmptyFolderToolStripMenuItem;
        private ToolStripMenuItem ContextDeleteSelectedToolStripMenuItem;
        private ToolStripMenuItem ContextRenameToolStripMenuItem;
        private ToolStripMenuItem ContextExportSelectedToolStripMenuItem;
        private ToolStripMenuItem ContextExportAllToolStripMenuItem;
        private ToolStripMenuItem ContextReplaceSelectedToolStripMenuItem;
        private BackgroundWorker EncodingBackgroundWorker;
        private ToolStripColorComboBox PaddingToolStripColorComboBox;
        private ToolStripColorComboBox AutoYaz0ToolStripColorComboBox;
    }
}