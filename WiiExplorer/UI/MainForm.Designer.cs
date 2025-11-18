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
            resources.ApplyResources(MainFormMenuStrip, "MainFormMenuStrip");
            MainFormMenuStrip.Name = "MainFormMenuStrip";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewToolStripMenuItem, NewFromFolderToolStripMenuItem, OpenToolStripMenuItem, SaveToolStripMenuItem, SaveAsToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            resources.ApplyResources(FileToolStripMenuItem, "FileToolStripMenuItem");
            // 
            // NewToolStripMenuItem
            // 
            NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            resources.ApplyResources(NewToolStripMenuItem, "NewToolStripMenuItem");
            NewToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            // 
            // NewFromFolderToolStripMenuItem
            // 
            NewFromFolderToolStripMenuItem.Name = "NewFromFolderToolStripMenuItem";
            resources.ApplyResources(NewFromFolderToolStripMenuItem, "NewFromFolderToolStripMenuItem");
            NewFromFolderToolStripMenuItem.Click += NewFromFolderToolStripMenuItem_Click;
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            resources.ApplyResources(OpenToolStripMenuItem, "OpenToolStripMenuItem");
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            resources.ApplyResources(SaveToolStripMenuItem, "SaveToolStripMenuItem");
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // SaveAsToolStripMenuItem
            // 
            resources.ApplyResources(SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem");
            SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            SaveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // EditToolStripMenuItem
            // 
            EditToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AddFileToolStripMenuItem, AddFolderToolStripMenuItem, CreateEmptyFolderToolStripMenuItem, DeleteSelectedToolStripMenuItem, RenameSelectedToolStripMenuItem, ReplaceSelectedToolStripMenuItem, ExportSelectedToolStripMenuItem, ExportAllToolStripMenuItem });
            resources.ApplyResources(EditToolStripMenuItem, "EditToolStripMenuItem");
            EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            // 
            // AddFileToolStripMenuItem
            // 
            resources.ApplyResources(AddFileToolStripMenuItem, "AddFileToolStripMenuItem");
            AddFileToolStripMenuItem.Name = "AddFileToolStripMenuItem";
            AddFileToolStripMenuItem.Click += AddFileToolStripMenuItem_Click;
            // 
            // AddFolderToolStripMenuItem
            // 
            resources.ApplyResources(AddFolderToolStripMenuItem, "AddFolderToolStripMenuItem");
            AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            AddFolderToolStripMenuItem.Click += AddFolderToolStripMenuItem_Click;
            // 
            // CreateEmptyFolderToolStripMenuItem
            // 
            resources.ApplyResources(CreateEmptyFolderToolStripMenuItem, "CreateEmptyFolderToolStripMenuItem");
            CreateEmptyFolderToolStripMenuItem.Name = "CreateEmptyFolderToolStripMenuItem";
            CreateEmptyFolderToolStripMenuItem.Click += CreateEmptyFolderToolStripMenuItem_Click;
            // 
            // DeleteSelectedToolStripMenuItem
            // 
            resources.ApplyResources(DeleteSelectedToolStripMenuItem, "DeleteSelectedToolStripMenuItem");
            DeleteSelectedToolStripMenuItem.Name = "DeleteSelectedToolStripMenuItem";
            DeleteSelectedToolStripMenuItem.Click += DeleteSelectedToolStripMenuItem_Click;
            // 
            // RenameSelectedToolStripMenuItem
            // 
            resources.ApplyResources(RenameSelectedToolStripMenuItem, "RenameSelectedToolStripMenuItem");
            RenameSelectedToolStripMenuItem.Name = "RenameSelectedToolStripMenuItem";
            RenameSelectedToolStripMenuItem.Click += RenameSelectedToolStripMenuItem_Click;
            // 
            // ReplaceSelectedToolStripMenuItem
            // 
            resources.ApplyResources(ReplaceSelectedToolStripMenuItem, "ReplaceSelectedToolStripMenuItem");
            ReplaceSelectedToolStripMenuItem.Name = "ReplaceSelectedToolStripMenuItem";
            ReplaceSelectedToolStripMenuItem.Click += ReplaceSelectedToolStripMenuItem_Click;
            // 
            // ExportSelectedToolStripMenuItem
            // 
            resources.ApplyResources(ExportSelectedToolStripMenuItem, "ExportSelectedToolStripMenuItem");
            ExportSelectedToolStripMenuItem.Name = "ExportSelectedToolStripMenuItem";
            ExportSelectedToolStripMenuItem.Click += ExportSelectedToolStripMenuItem_Click;
            // 
            // ExportAllToolStripMenuItem
            // 
            resources.ApplyResources(ExportAllToolStripMenuItem, "ExportAllToolStripMenuItem");
            ExportAllToolStripMenuItem.Name = "ExportAllToolStripMenuItem";
            ExportAllToolStripMenuItem.Click += ExportAllToolStripMenuItem_Click;
            // 
            // Yaz0ToolStripComboBox
            // 
            Yaz0ToolStripComboBox.Alignment = ToolStripItemAlignment.Right;
            Yaz0ToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Yaz0ToolStripComboBox.DropDownWidth = 88;
            resources.ApplyResources(Yaz0ToolStripComboBox, "Yaz0ToolStripComboBox");
            Yaz0ToolStripComboBox.Items.AddRange(new object[] { resources.GetString("Yaz0ToolStripComboBox.Items"), resources.GetString("Yaz0ToolStripComboBox.Items1"), resources.GetString("Yaz0ToolStripComboBox.Items2"), resources.GetString("Yaz0ToolStripComboBox.Items3"), resources.GetString("Yaz0ToolStripComboBox.Items4") });
            Yaz0ToolStripComboBox.Name = "Yaz0ToolStripComboBox";
            Yaz0ToolStripComboBox.SelectedIndexChanged += Yaz0ToolStripComboBox_SelectedIndexChanged;
            // 
            // FilePropertiesToolStripMenuItem
            // 
            FilePropertiesToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            resources.ApplyResources(FilePropertiesToolStripMenuItem, "FilePropertiesToolStripMenuItem");
            FilePropertiesToolStripMenuItem.Name = "FilePropertiesToolStripMenuItem";
            FilePropertiesToolStripMenuItem.Click += FilePropertiesToolStripMenuItem_Click;
            // 
            // SettingsToolStripMenuItem
            // 
            SettingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ThemeToolStripComboBox, PaddingToolStripColorComboBox, AutoYaz0ToolStripColorComboBox });
            SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            resources.ApplyResources(SettingsToolStripMenuItem, "SettingsToolStripMenuItem");
            // 
            // ThemeToolStripComboBox
            // 
            ThemeToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            resources.ApplyResources(ThemeToolStripComboBox, "ThemeToolStripComboBox");
            ThemeToolStripComboBox.Items.AddRange(new object[] { resources.GetString("ThemeToolStripComboBox.Items"), resources.GetString("ThemeToolStripComboBox.Items1") });
            ThemeToolStripComboBox.Name = "ThemeToolStripComboBox";
            ThemeToolStripComboBox.SelectedIndexChanged += ThemeToolStripComboBox_SelectedIndexChanged;
            // 
            // PaddingToolStripColorComboBox
            // 
            PaddingToolStripColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            resources.ApplyResources(PaddingToolStripColorComboBox, "PaddingToolStripColorComboBox");
            PaddingToolStripColorComboBox.Items.AddRange(new object[] { resources.GetString("PaddingToolStripColorComboBox.Items"), resources.GetString("PaddingToolStripColorComboBox.Items1"), resources.GetString("PaddingToolStripColorComboBox.Items2"), resources.GetString("PaddingToolStripColorComboBox.Items3"), resources.GetString("PaddingToolStripColorComboBox.Items4") });
            PaddingToolStripColorComboBox.Name = "PaddingToolStripColorComboBox";
            PaddingToolStripColorComboBox.SelectedIndexChanged += PaddingToolStripColorComboBox_SelectedIndexChanged;
            // 
            // AutoYaz0ToolStripColorComboBox
            // 
            AutoYaz0ToolStripColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            resources.ApplyResources(AutoYaz0ToolStripColorComboBox, "AutoYaz0ToolStripColorComboBox");
            AutoYaz0ToolStripColorComboBox.Items.AddRange(new object[] { resources.GetString("AutoYaz0ToolStripColorComboBox.Items"), resources.GetString("AutoYaz0ToolStripColorComboBox.Items1") });
            AutoYaz0ToolStripColorComboBox.Name = "AutoYaz0ToolStripColorComboBox";
            AutoYaz0ToolStripColorComboBox.SelectedIndexChanged += AutoYaz0ToolStripColorComboBox_SelectedIndexChanged;
            // 
            // RootPanel
            // 
            RootPanel.Controls.Add(RootNameTextBox);
            RootPanel.Controls.Add(KeepIDsSyncedCheckBox);
            RootPanel.Controls.Add(RootNameLabel);
            resources.ApplyResources(RootPanel, "RootPanel");
            RootPanel.Name = "RootPanel";
            // 
            // RootNameTextBox
            // 
            resources.ApplyResources(RootNameTextBox, "RootNameTextBox");
            RootNameTextBox.Name = "RootNameTextBox";
            RootNameTextBox.TextChanged += RootNameTextBox_TextChanged;
            // 
            // KeepIDsSyncedCheckBox
            // 
            resources.ApplyResources(KeepIDsSyncedCheckBox, "KeepIDsSyncedCheckBox");
            KeepIDsSyncedCheckBox.Checked = true;
            KeepIDsSyncedCheckBox.CheckState = CheckState.Checked;
            KeepIDsSyncedCheckBox.Name = "KeepIDsSyncedCheckBox";
            KeepIDsSyncedCheckBox.UseVisualStyleBackColor = true;
            KeepIDsSyncedCheckBox.CheckedChanged += KeepIDsSyncedCheckBox_CheckedChanged;
            // 
            // RootNameLabel
            // 
            resources.ApplyResources(RootNameLabel, "RootNameLabel");
            RootNameLabel.Name = "RootNameLabel";
            // 
            // MainFormStatusStrip
            // 
            MainFormStatusStrip.Items.AddRange(new ToolStripItem[] { MainToolStripProgressBar, MainToolStripStatusLabel });
            resources.ApplyResources(MainFormStatusStrip, "MainFormStatusStrip");
            MainFormStatusStrip.Name = "MainFormStatusStrip";
            // 
            // MainToolStripProgressBar
            // 
            MainToolStripProgressBar.Name = "MainToolStripProgressBar";
            resources.ApplyResources(MainToolStripProgressBar, "MainToolStripProgressBar");
            // 
            // MainToolStripStatusLabel
            // 
            MainToolStripStatusLabel.Name = "MainToolStripStatusLabel";
            resources.ApplyResources(MainToolStripStatusLabel, "MainToolStripStatusLabel");
            MainToolStripStatusLabel.Spring = true;
            // 
            // ArchiveTreeView
            // 
            ArchiveTreeView.AllowDrop = true;
            ArchiveTreeView.BorderStyle = BorderStyle.None;
            resources.ApplyResources(ArchiveTreeView, "ArchiveTreeView");
            ArchiveTreeView.FullRowSelect = true;
            ArchiveTreeView.HideSelection = false;
            ArchiveTreeView.Name = "ArchiveTreeView";
            ArchiveTreeView.PathSeparator = "/";
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
            resources.ApplyResources(ArchiveContextMenuStrip, "ArchiveContextMenuStrip");
            // 
            // ContextAddFileToolStripMenuItem
            // 
            ContextAddFileToolStripMenuItem.Name = "ContextAddFileToolStripMenuItem";
            resources.ApplyResources(ContextAddFileToolStripMenuItem, "ContextAddFileToolStripMenuItem");
            ContextAddFileToolStripMenuItem.Click += AddFileToolStripMenuItem_Click;
            // 
            // ContextAddFolderToolStripMenuItem
            // 
            ContextAddFolderToolStripMenuItem.Name = "ContextAddFolderToolStripMenuItem";
            resources.ApplyResources(ContextAddFolderToolStripMenuItem, "ContextAddFolderToolStripMenuItem");
            ContextAddFolderToolStripMenuItem.Click += AddFolderToolStripMenuItem_Click;
            // 
            // ContextCreateEmptyFolderToolStripMenuItem
            // 
            ContextCreateEmptyFolderToolStripMenuItem.Name = "ContextCreateEmptyFolderToolStripMenuItem";
            resources.ApplyResources(ContextCreateEmptyFolderToolStripMenuItem, "ContextCreateEmptyFolderToolStripMenuItem");
            ContextCreateEmptyFolderToolStripMenuItem.Click += CreateEmptyFolderToolStripMenuItem_Click;
            // 
            // ContextDeleteSelectedToolStripMenuItem
            // 
            ContextDeleteSelectedToolStripMenuItem.Name = "ContextDeleteSelectedToolStripMenuItem";
            resources.ApplyResources(ContextDeleteSelectedToolStripMenuItem, "ContextDeleteSelectedToolStripMenuItem");
            ContextDeleteSelectedToolStripMenuItem.Click += DeleteSelectedToolStripMenuItem_Click;
            // 
            // ContextRenameToolStripMenuItem
            // 
            ContextRenameToolStripMenuItem.Name = "ContextRenameToolStripMenuItem";
            resources.ApplyResources(ContextRenameToolStripMenuItem, "ContextRenameToolStripMenuItem");
            ContextRenameToolStripMenuItem.Click += RenameSelectedToolStripMenuItem_Click;
            // 
            // ContextReplaceSelectedToolStripMenuItem
            // 
            ContextReplaceSelectedToolStripMenuItem.Name = "ContextReplaceSelectedToolStripMenuItem";
            resources.ApplyResources(ContextReplaceSelectedToolStripMenuItem, "ContextReplaceSelectedToolStripMenuItem");
            ContextReplaceSelectedToolStripMenuItem.Click += ReplaceSelectedToolStripMenuItem_Click;
            // 
            // ContextExportSelectedToolStripMenuItem
            // 
            ContextExportSelectedToolStripMenuItem.Name = "ContextExportSelectedToolStripMenuItem";
            resources.ApplyResources(ContextExportSelectedToolStripMenuItem, "ContextExportSelectedToolStripMenuItem");
            ContextExportSelectedToolStripMenuItem.Click += ExportSelectedToolStripMenuItem_Click;
            // 
            // ContextExportAllToolStripMenuItem
            // 
            ContextExportAllToolStripMenuItem.Name = "ContextExportAllToolStripMenuItem";
            resources.ApplyResources(ContextExportAllToolStripMenuItem, "ContextExportAllToolStripMenuItem");
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
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ArchiveTreeView);
            Controls.Add(MainFormStatusStrip);
            Controls.Add(RootPanel);
            Controls.Add(MainFormMenuStrip);
            KeyPreview = true;
            MainMenuStrip = MainFormMenuStrip;
            Name = "MainForm";
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