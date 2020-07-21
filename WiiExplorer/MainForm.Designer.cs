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
            this.ArchiveImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.RootPanel = new System.Windows.Forms.Panel();
            this.RootNameTextBox = new System.Windows.Forms.TextBox();
            this.RootNameLabel = new System.Windows.Forms.Label();
            this.ArchiveTreeView = new System.Windows.Forms.TreeView();
            this.MainFormMenuStrip.SuspendLayout();
            this.MainFormStatusStrip.SuspendLayout();
            this.RootPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.Yaz0ToolStripComboBox});
            this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.Size = new System.Drawing.Size(464, 27);
            this.MainFormMenuStrip.TabIndex = 0;
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.NewFromFolderToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.NewToolStripMenuItem.Text = "New";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // NewFromFolderToolStripMenuItem
            // 
            this.NewFromFolderToolStripMenuItem.Name = "NewFromFolderToolStripMenuItem";
            this.NewFromFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.NewFromFolderToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.NewFromFolderToolStripMenuItem.Text = "New From Folder";
            this.NewFromFolderToolStripMenuItem.Click += new System.EventHandler(this.NewFromFolderToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Enabled = false;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Enabled = false;
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.SaveAsToolStripMenuItem.Text = "Save As";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFileToolStripMenuItem,
            this.AddFolderToolStripMenuItem,
            this.ImportFolderToolStripMenuItem,
            this.DeleteSelectedToolStripMenuItem,
            this.RenameSelectedToolStripMenuItem,
            this.ExportSelectedToolStripMenuItem,
            this.ExportAllToolStripMenuItem,
            this.ReplaceSelectedToolStripMenuItem});
            this.EditToolStripMenuItem.Enabled = false;
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // AddFileToolStripMenuItem
            // 
            this.AddFileToolStripMenuItem.Enabled = false;
            this.AddFileToolStripMenuItem.Name = "AddFileToolStripMenuItem";
            this.AddFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.AddFileToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.AddFileToolStripMenuItem.Text = "Add File(s)";
            this.AddFileToolStripMenuItem.Click += new System.EventHandler(this.AddFileToolStripMenuItem_Click);
            // 
            // AddFolderToolStripMenuItem
            // 
            this.AddFolderToolStripMenuItem.Enabled = false;
            this.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            this.AddFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.AddFolderToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.AddFolderToolStripMenuItem.Text = "Add Empty Folder";
            this.AddFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolderToolStripMenuItem_Click);
            // 
            // ImportFolderToolStripMenuItem
            // 
            this.ImportFolderToolStripMenuItem.Enabled = false;
            this.ImportFolderToolStripMenuItem.Name = "ImportFolderToolStripMenuItem";
            this.ImportFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.ImportFolderToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.ImportFolderToolStripMenuItem.Text = "Import Folder";
            this.ImportFolderToolStripMenuItem.Click += new System.EventHandler(this.ImportFolderToolStripMenuItem_Click);
            // 
            // DeleteSelectedToolStripMenuItem
            // 
            this.DeleteSelectedToolStripMenuItem.Enabled = false;
            this.DeleteSelectedToolStripMenuItem.Name = "DeleteSelectedToolStripMenuItem";
            this.DeleteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.DeleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.DeleteSelectedToolStripMenuItem.Text = "Delete Selected";
            this.DeleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
            // 
            // RenameSelectedToolStripMenuItem
            // 
            this.RenameSelectedToolStripMenuItem.Enabled = false;
            this.RenameSelectedToolStripMenuItem.Name = "RenameSelectedToolStripMenuItem";
            this.RenameSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.RenameSelectedToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.RenameSelectedToolStripMenuItem.Text = "Rename Selected";
            this.RenameSelectedToolStripMenuItem.Click += new System.EventHandler(this.RenameSelectedToolStripMenuItem_Click);
            // 
            // ExportSelectedToolStripMenuItem
            // 
            this.ExportSelectedToolStripMenuItem.Enabled = false;
            this.ExportSelectedToolStripMenuItem.Name = "ExportSelectedToolStripMenuItem";
            this.ExportSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.ExportSelectedToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.ExportSelectedToolStripMenuItem.Text = "Export Selected";
            this.ExportSelectedToolStripMenuItem.Click += new System.EventHandler(this.ExportSelectedToolStripMenuItem_Click);
            // 
            // ExportAllToolStripMenuItem
            // 
            this.ExportAllToolStripMenuItem.Enabled = false;
            this.ExportAllToolStripMenuItem.Name = "ExportAllToolStripMenuItem";
            this.ExportAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.ExportAllToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.ExportAllToolStripMenuItem.Text = "Export All";
            this.ExportAllToolStripMenuItem.Click += new System.EventHandler(this.ExportAllToolStripMenuItem_Click);
            // 
            // ReplaceSelectedToolStripMenuItem
            // 
            this.ReplaceSelectedToolStripMenuItem.Enabled = false;
            this.ReplaceSelectedToolStripMenuItem.Name = "ReplaceSelectedToolStripMenuItem";
            this.ReplaceSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.ReplaceSelectedToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.ReplaceSelectedToolStripMenuItem.Text = "Replace Selected";
            this.ReplaceSelectedToolStripMenuItem.Click += new System.EventHandler(this.ReplaceSelectedToolStripMenuItem_Click);
            // 
            // Yaz0ToolStripComboBox
            // 
            this.Yaz0ToolStripComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Yaz0ToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Yaz0ToolStripComboBox.Items.AddRange(new object[] {
            "Yaz0 Off",
            "Yaz0 On",
            "Yaz0 Fast"});
            this.Yaz0ToolStripComboBox.Name = "Yaz0ToolStripComboBox";
            this.Yaz0ToolStripComboBox.Size = new System.Drawing.Size(75, 23);
            this.Yaz0ToolStripComboBox.ToolTipText = "Yaz0 Toggle\r\nIf set to Yaz0 On, the saved archive will be\r\nYaz0 Encoded. This mea" +
    "ns smaller filesizes.\r\nYaz0 On by Default";
            this.Yaz0ToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.Yaz0ToolStripComboBox_SelectedIndexChanged);
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
            this.MainFormStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripProgressBar,
            this.MainToolStripStatusLabel});
            this.MainFormStatusStrip.Location = new System.Drawing.Point(0, 329);
            this.MainFormStatusStrip.Name = "MainFormStatusStrip";
            this.MainFormStatusStrip.Size = new System.Drawing.Size(464, 22);
            this.MainFormStatusStrip.TabIndex = 2;
            this.MainFormStatusStrip.Text = "statusStrip1";
            // 
            // MainToolStripProgressBar
            // 
            this.MainToolStripProgressBar.Name = "MainToolStripProgressBar";
            this.MainToolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainToolStripStatusLabel
            // 
            this.MainToolStripStatusLabel.Name = "MainToolStripStatusLabel";
            this.MainToolStripStatusLabel.Size = new System.Drawing.Size(347, 17);
            this.MainToolStripStatusLabel.Spring = true;
            this.MainToolStripStatusLabel.Text = "No File Loaded.";
            this.MainToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RootPanel
            // 
            this.RootPanel.Controls.Add(this.RootNameTextBox);
            this.RootPanel.Controls.Add(this.RootNameLabel);
            this.RootPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RootPanel.Location = new System.Drawing.Point(0, 27);
            this.RootPanel.Name = "RootPanel";
            this.RootPanel.Size = new System.Drawing.Size(464, 20);
            this.RootPanel.TabIndex = 3;
            // 
            // RootNameTextBox
            // 
            this.RootNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootNameTextBox.Enabled = false;
            this.RootNameTextBox.Location = new System.Drawing.Point(64, 0);
            this.RootNameTextBox.Name = "RootNameTextBox";
            this.RootNameTextBox.Size = new System.Drawing.Size(400, 20);
            this.RootNameTextBox.TabIndex = 0;
            // 
            // RootNameLabel
            // 
            this.RootNameLabel.AutoSize = true;
            this.RootNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.RootNameLabel.Location = new System.Drawing.Point(0, 0);
            this.RootNameLabel.Name = "RootNameLabel";
            this.RootNameLabel.Size = new System.Drawing.Size(64, 13);
            this.RootNameLabel.TabIndex = 1;
            this.RootNameLabel.Text = "Root Name:";
            // 
            // ArchiveTreeView
            // 
            this.ArchiveTreeView.AllowDrop = true;
            this.ArchiveTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveTreeView.Enabled = false;
            this.ArchiveTreeView.FullRowSelect = true;
            this.ArchiveTreeView.HideSelection = false;
            this.ArchiveTreeView.ImageIndex = 0;
            this.ArchiveTreeView.ImageList = this.ArchiveImageList;
            this.ArchiveTreeView.Location = new System.Drawing.Point(0, 47);
            this.ArchiveTreeView.Name = "ArchiveTreeView";
            this.ArchiveTreeView.SelectedImageIndex = 0;
            this.ArchiveTreeView.Size = new System.Drawing.Size(464, 282);
            this.ArchiveTreeView.TabIndex = 1;
            this.ArchiveTreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.ArchiveTreeView_BeforeCollapse);
            this.ArchiveTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.ArchiveTreeView_BeforeExpand);
            this.ArchiveTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ArchiveTreeView_ItemDrag);
            this.ArchiveTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ArchiveTreeView_AfterSelect);
            this.ArchiveTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ArchiveTreeView_DragDrop);
            this.ArchiveTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ArchiveTreeView_DragEnter);
            this.ArchiveTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.ArchiveTreeView_DragOver);
            this.ArchiveTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArchiveTreeView_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 351);
            this.Controls.Add(this.ArchiveTreeView);
            this.Controls.Add(this.RootPanel);
            this.Controls.Add(this.MainFormStatusStrip);
            this.Controls.Add(this.MainFormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainFormMenuStrip;
            this.MinimumSize = new System.Drawing.Size(480, 390);
            this.Name = "MainForm";
            this.Text = "WiiExplorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.MainFormStatusStrip.ResumeLayout(false);
            this.MainFormStatusStrip.PerformLayout();
            this.RootPanel.ResumeLayout(false);
            this.RootPanel.PerformLayout();
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
        private System.Windows.Forms.TextBox RootNameTextBox;
        private System.Windows.Forms.Label RootNameLabel;
        private System.Windows.Forms.TreeView ArchiveTreeView;
        private System.Windows.Forms.ToolStripMenuItem AddFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox Yaz0ToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem ExportSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReplaceSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewFromFolderToolStripMenuItem;
    }
}

