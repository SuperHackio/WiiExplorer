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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewFromFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplaceSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Yaz0ToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ItemPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SwitchThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.RootPanel = new System.Windows.Forms.Panel();
            this.RootNameTextBox = new WiiExplorer.ColourTextBox();
            this.KeepIDsSyncedCheckBox = new System.Windows.Forms.CheckBox();
            this.RootNameLabel = new System.Windows.Forms.Label();
            this.ArchiveTreeView = new System.Windows.Forms.TreeView();
            this.ArchiveContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextAddFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextAddFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextImportFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextDeleteSelectedToolStripMenuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextExportSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextExportAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextReplaceSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EncodingBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainFormMenuStrip.SuspendLayout();
            this.MainFormStatusStrip.SuspendLayout();
            this.RootPanel.SuspendLayout();
            this.ArchiveContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            resources.ApplyResources(this.MainFormMenuStrip, "MainFormMenuStrip");
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.Yaz0ToolStripComboBox,
            this.ItemPropertiesToolStripMenuItem,
            this.SwitchThemeToolStripMenuItem});
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFormMenuStrip_Paint);
            // 
            // FileToolStripMenuItem
            // 
            resources.ApplyResources(this.FileToolStripMenuItem, "FileToolStripMenuItem");
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.NewFromFolderToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            // 
            // NewToolStripMenuItem
            // 
            resources.ApplyResources(this.NewToolStripMenuItem, "NewToolStripMenuItem");
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // NewFromFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.NewFromFolderToolStripMenuItem, "NewFromFolderToolStripMenuItem");
            this.NewFromFolderToolStripMenuItem.Name = "NewFromFolderToolStripMenuItem";
            this.NewFromFolderToolStripMenuItem.Click += new System.EventHandler(this.NewFromFolderToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            resources.ApplyResources(this.OpenToolStripMenuItem, "OpenToolStripMenuItem");
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            resources.ApplyResources(this.SaveToolStripMenuItem, "SaveToolStripMenuItem");
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            resources.ApplyResources(this.SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem");
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            resources.ApplyResources(this.EditToolStripMenuItem, "EditToolStripMenuItem");
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFileToolStripMenuItem,
            this.AddFolderToolStripMenuItem,
            this.ImportFolderToolStripMenuItem,
            this.DeleteSelectedToolStripMenuItem,
            this.RenameSelectedToolStripMenuItem,
            this.ExportSelectedToolStripMenuItem,
            this.ExportAllToolStripMenuItem,
            this.ReplaceSelectedToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            // 
            // AddFileToolStripMenuItem
            // 
            resources.ApplyResources(this.AddFileToolStripMenuItem, "AddFileToolStripMenuItem");
            this.AddFileToolStripMenuItem.Name = "AddFileToolStripMenuItem";
            this.AddFileToolStripMenuItem.Click += new System.EventHandler(this.AddFileToolStripMenuItem_Click);
            // 
            // AddFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.AddFolderToolStripMenuItem, "AddFolderToolStripMenuItem");
            this.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            this.AddFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolderToolStripMenuItem_Click);
            // 
            // ImportFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.ImportFolderToolStripMenuItem, "ImportFolderToolStripMenuItem");
            this.ImportFolderToolStripMenuItem.Name = "ImportFolderToolStripMenuItem";
            this.ImportFolderToolStripMenuItem.Click += new System.EventHandler(this.ImportFolderToolStripMenuItem_Click);
            // 
            // DeleteSelectedToolStripMenuItem
            // 
            resources.ApplyResources(this.DeleteSelectedToolStripMenuItem, "DeleteSelectedToolStripMenuItem");
            this.DeleteSelectedToolStripMenuItem.Name = "DeleteSelectedToolStripMenuItem";
            this.DeleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
            // 
            // RenameSelectedToolStripMenuItem
            // 
            resources.ApplyResources(this.RenameSelectedToolStripMenuItem, "RenameSelectedToolStripMenuItem");
            this.RenameSelectedToolStripMenuItem.Name = "RenameSelectedToolStripMenuItem";
            this.RenameSelectedToolStripMenuItem.Click += new System.EventHandler(this.RenameSelectedToolStripMenuItem_Click);
            // 
            // ExportSelectedToolStripMenuItem
            // 
            resources.ApplyResources(this.ExportSelectedToolStripMenuItem, "ExportSelectedToolStripMenuItem");
            this.ExportSelectedToolStripMenuItem.Name = "ExportSelectedToolStripMenuItem";
            this.ExportSelectedToolStripMenuItem.Click += new System.EventHandler(this.ExportSelectedToolStripMenuItem_Click);
            // 
            // ExportAllToolStripMenuItem
            // 
            resources.ApplyResources(this.ExportAllToolStripMenuItem, "ExportAllToolStripMenuItem");
            this.ExportAllToolStripMenuItem.Name = "ExportAllToolStripMenuItem";
            this.ExportAllToolStripMenuItem.Click += new System.EventHandler(this.ExportAllToolStripMenuItem_Click);
            // 
            // ReplaceSelectedToolStripMenuItem
            // 
            resources.ApplyResources(this.ReplaceSelectedToolStripMenuItem, "ReplaceSelectedToolStripMenuItem");
            this.ReplaceSelectedToolStripMenuItem.Name = "ReplaceSelectedToolStripMenuItem";
            this.ReplaceSelectedToolStripMenuItem.Click += new System.EventHandler(this.ReplaceSelectedToolStripMenuItem_Click);
            // 
            // Yaz0ToolStripComboBox
            // 
            resources.ApplyResources(this.Yaz0ToolStripComboBox, "Yaz0ToolStripComboBox");
            this.Yaz0ToolStripComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Yaz0ToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Yaz0ToolStripComboBox.Items.AddRange(new object[] {
            resources.GetString("Yaz0ToolStripComboBox.Items"),
            resources.GetString("Yaz0ToolStripComboBox.Items1"),
            resources.GetString("Yaz0ToolStripComboBox.Items2"),
            resources.GetString("Yaz0ToolStripComboBox.Items3")});
            this.Yaz0ToolStripComboBox.Name = "Yaz0ToolStripComboBox";
            this.Yaz0ToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.Yaz0ToolStripComboBox_SelectedIndexChanged);
            // 
            // ItemPropertiesToolStripMenuItem
            // 
            resources.ApplyResources(this.ItemPropertiesToolStripMenuItem, "ItemPropertiesToolStripMenuItem");
            this.ItemPropertiesToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ItemPropertiesToolStripMenuItem.Name = "ItemPropertiesToolStripMenuItem";
            this.ItemPropertiesToolStripMenuItem.Click += new System.EventHandler(this.ItemPropertiesToolStripMenuItem_Click);
            // 
            // SwitchThemeToolStripMenuItem
            // 
            resources.ApplyResources(this.SwitchThemeToolStripMenuItem, "SwitchThemeToolStripMenuItem");
            this.SwitchThemeToolStripMenuItem.Name = "SwitchThemeToolStripMenuItem";
            this.SwitchThemeToolStripMenuItem.Click += new System.EventHandler(this.SwitchThemeToolStripMenuItem_Click);
            // 
            // ArchiveImageList
            // 
            this.ArchiveImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ArchiveImageList.ImageStream")));
            this.ArchiveImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ArchiveImageList.Images.SetKeyName(0, "FolderClosed.png");
            this.ArchiveImageList.Images.SetKeyName(1, "FolderOpen.png");
            this.ArchiveImageList.Images.SetKeyName(2, "FormatUnknown.png");
            // 
            // MainFormStatusStrip
            // 
            resources.ApplyResources(this.MainFormStatusStrip, "MainFormStatusStrip");
            this.MainFormStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripProgressBar,
            this.MainToolStripStatusLabel});
            this.MainFormStatusStrip.Name = "MainFormStatusStrip";
            // 
            // MainToolStripProgressBar
            // 
            resources.ApplyResources(this.MainToolStripProgressBar, "MainToolStripProgressBar");
            this.MainToolStripProgressBar.Name = "MainToolStripProgressBar";
            // 
            // MainToolStripStatusLabel
            // 
            resources.ApplyResources(this.MainToolStripStatusLabel, "MainToolStripStatusLabel");
            this.MainToolStripStatusLabel.Name = "MainToolStripStatusLabel";
            this.MainToolStripStatusLabel.Spring = true;
            // 
            // RootPanel
            // 
            resources.ApplyResources(this.RootPanel, "RootPanel");
            this.RootPanel.Controls.Add(this.RootNameTextBox);
            this.RootPanel.Controls.Add(this.KeepIDsSyncedCheckBox);
            this.RootPanel.Controls.Add(this.RootNameLabel);
            this.RootPanel.Name = "RootPanel";
            // 
            // RootNameTextBox
            // 
            resources.ApplyResources(this.RootNameTextBox, "RootNameTextBox");
            this.RootNameTextBox.Name = "RootNameTextBox";
            this.RootNameTextBox.TextChanged += new System.EventHandler(this.RootNameTextBox_TextChanged);
            // 
            // KeepIDsSyncedCheckBox
            // 
            resources.ApplyResources(this.KeepIDsSyncedCheckBox, "KeepIDsSyncedCheckBox");
            this.KeepIDsSyncedCheckBox.Checked = true;
            this.KeepIDsSyncedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KeepIDsSyncedCheckBox.Name = "KeepIDsSyncedCheckBox";
            this.KeepIDsSyncedCheckBox.UseVisualStyleBackColor = true;
            this.KeepIDsSyncedCheckBox.CheckedChanged += new System.EventHandler(this.KeepIDsSyncedCheckBox_CheckedChanged);
            // 
            // RootNameLabel
            // 
            resources.ApplyResources(this.RootNameLabel, "RootNameLabel");
            this.RootNameLabel.Name = "RootNameLabel";
            // 
            // ArchiveTreeView
            // 
            resources.ApplyResources(this.ArchiveTreeView, "ArchiveTreeView");
            this.ArchiveTreeView.AllowDrop = true;
            this.ArchiveTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ArchiveTreeView.FullRowSelect = true;
            this.ArchiveTreeView.HideSelection = false;
            this.ArchiveTreeView.ImageList = this.ArchiveImageList;
            this.ArchiveTreeView.Name = "ArchiveTreeView";
            this.ArchiveTreeView.PathSeparator = "/";
            this.ArchiveTreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.ArchiveTreeView_BeforeCollapse);
            this.ArchiveTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.ArchiveTreeView_BeforeExpand);
            this.ArchiveTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ArchiveTreeView_ItemDrag);
            this.ArchiveTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ArchiveTreeView_AfterSelect);
            this.ArchiveTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ArchiveTreeView_NodeMouseClick);
            this.ArchiveTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ArchiveTreeView_DragDrop);
            this.ArchiveTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ArchiveTreeView_DragEnter);
            this.ArchiveTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.ArchiveTreeView_DragOver);
            this.ArchiveTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArchiveTreeView_MouseDown);
            // 
            // ArchiveContextMenuStrip
            // 
            resources.ApplyResources(this.ArchiveContextMenuStrip, "ArchiveContextMenuStrip");
            this.ArchiveContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextAddFileToolStripMenuItem,
            this.ContextAddFolderToolStripMenuItem,
            this.ContextImportFolderToolStripMenuItem,
            this.ContextDeleteSelectedToolStripMenuItems,
            this.ContextRenameToolStripMenuItem,
            this.ContextExportSelectToolStripMenuItem,
            this.ContextExportAllToolStripMenuItem,
            this.ContextReplaceSelectedToolStripMenuItem});
            this.ArchiveContextMenuStrip.Name = "ArchiveContextMenuStrip";
            // 
            // ContextAddFileToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextAddFileToolStripMenuItem, "ContextAddFileToolStripMenuItem");
            this.ContextAddFileToolStripMenuItem.Name = "ContextAddFileToolStripMenuItem";
            this.ContextAddFileToolStripMenuItem.Click += new System.EventHandler(this.AddFileToolStripMenuItem_Click);
            // 
            // ContextAddFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextAddFolderToolStripMenuItem, "ContextAddFolderToolStripMenuItem");
            this.ContextAddFolderToolStripMenuItem.Name = "ContextAddFolderToolStripMenuItem";
            this.ContextAddFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolderToolStripMenuItem_Click);
            // 
            // ContextImportFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextImportFolderToolStripMenuItem, "ContextImportFolderToolStripMenuItem");
            this.ContextImportFolderToolStripMenuItem.Name = "ContextImportFolderToolStripMenuItem";
            this.ContextImportFolderToolStripMenuItem.Click += new System.EventHandler(this.ImportFolderToolStripMenuItem_Click);
            // 
            // ContextDeleteSelectedToolStripMenuItems
            // 
            resources.ApplyResources(this.ContextDeleteSelectedToolStripMenuItems, "ContextDeleteSelectedToolStripMenuItems");
            this.ContextDeleteSelectedToolStripMenuItems.Name = "ContextDeleteSelectedToolStripMenuItems";
            this.ContextDeleteSelectedToolStripMenuItems.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
            // 
            // ContextRenameToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextRenameToolStripMenuItem, "ContextRenameToolStripMenuItem");
            this.ContextRenameToolStripMenuItem.Name = "ContextRenameToolStripMenuItem";
            this.ContextRenameToolStripMenuItem.Click += new System.EventHandler(this.RenameSelectedToolStripMenuItem_Click);
            // 
            // ContextExportSelectToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextExportSelectToolStripMenuItem, "ContextExportSelectToolStripMenuItem");
            this.ContextExportSelectToolStripMenuItem.Name = "ContextExportSelectToolStripMenuItem";
            this.ContextExportSelectToolStripMenuItem.Click += new System.EventHandler(this.ExportSelectedToolStripMenuItem_Click);
            // 
            // ContextExportAllToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextExportAllToolStripMenuItem, "ContextExportAllToolStripMenuItem");
            this.ContextExportAllToolStripMenuItem.Name = "ContextExportAllToolStripMenuItem";
            this.ContextExportAllToolStripMenuItem.Click += new System.EventHandler(this.ExportAllToolStripMenuItem_Click);
            // 
            // ContextReplaceSelectedToolStripMenuItem
            // 
            resources.ApplyResources(this.ContextReplaceSelectedToolStripMenuItem, "ContextReplaceSelectedToolStripMenuItem");
            this.ContextReplaceSelectedToolStripMenuItem.Name = "ContextReplaceSelectedToolStripMenuItem";
            this.ContextReplaceSelectedToolStripMenuItem.Click += new System.EventHandler(this.ReplaceSelectedToolStripMenuItem_Click);
            // 
            // EncodingBackgroundWorker
            // 
            this.EncodingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.EncodingBackgroundWorker_DoWork);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ArchiveTreeView);
            this.Controls.Add(this.RootPanel);
            this.Controls.Add(this.MainFormStatusStrip);
            this.Controls.Add(this.MainFormMenuStrip);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainFormMenuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.MainFormStatusStrip.ResumeLayout(false);
            this.MainFormStatusStrip.PerformLayout();
            this.RootPanel.ResumeLayout(false);
            this.RootPanel.PerformLayout();
            this.ArchiveContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip MainFormStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar MainToolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel MainToolStripStatusLabel;
        private System.Windows.Forms.ImageList ArchiveImageList;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameSelectedToolStripMenuItem;
        private System.Windows.Forms.Panel RootPanel;
        private ColourTextBox RootNameTextBox;
        private System.Windows.Forms.Label RootNameLabel;
        private System.Windows.Forms.TreeView ArchiveTreeView;
        private System.Windows.Forms.ToolStripMenuItem AddFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox Yaz0ToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem ExportSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReplaceSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewFromFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ArchiveContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ContextDeleteSelectedToolStripMenuItems;
        private System.Windows.Forms.ToolStripMenuItem ContextRenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextExportSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextReplaceSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextAddFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextAddFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextImportFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextExportAllToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker EncodingBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem ItemPropertiesToolStripMenuItem;
        private System.Windows.Forms.CheckBox KeepIDsSyncedCheckBox;
        private System.Windows.Forms.ToolStripMenuItem SwitchThemeToolStripMenuItem;
    }
}

