﻿using Hack.io.RARC;
using Hack.io.YAZ0;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

namespace WiiExplorer
{
    public partial class MainForm : Form
    {
        public MainForm(string Openwith)
        {
            InitializeComponent();
            CenterToScreen();
            Yaz0ToolStripComboBox.SelectedIndex = Program.Yaz0Mode;
            Text = $"WiiExplorer {Application.ProductVersion}";
            OpenWith = Openwith;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ExtensionList.txt"))
            {
                string[] exts = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "ExtensionList.txt");
                KnownExtensions.AddRange(exts);
                for (int i = 0; i < exts.Length; i++)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Icons\\" + exts[i].Split('|')[0] + ".png"))
                    {
                        ArchiveImageList.Images.Add(exts[i].Split('|')[1], new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Icons\\" + exts[i].Split('|')[0] + ".png"));
                    }
                }
            }

            RootNameTextBox.ContextMenu = new ContextMenu();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (OpenWith != null)
                OpenArchive(OpenWith);
            OpenWith = null;
        }

        OpenFileDialog ofd = new OpenFileDialog() { Filter = "All Supported Files|*.arc;*.szs|Revolution Archives|*.arc|YAZ0 Identified Revolution Archives|*.szs|All Files|*.*" };
        SaveFileDialog sfd = new SaveFileDialog() { Filter = "All Supported Files|*.arc;*.szs|Revolution Archives|*.arc|YAZ0 Identified Revolution Archives|*.szs|All Files|*.*" };

        OpenFileDialog Fileofd = new OpenFileDialog() { Multiselect = true };
        SaveFileDialog Exportsfd = new SaveFileDialog();

        RARC Archive;
        bool Edited = false;
        static List<string> KnownExtensions = new List<string>
        {
            "Extensionless File|*"
        };
        string OpenWith = null;

        #region Treeview

        #region Private Fields
        private string NodeMap;
        private const int MAPSIZE = 128;
        private StringBuilder NewNodeMap = new StringBuilder(MAPSIZE);
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


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

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


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
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


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

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
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
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
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void ArchiveTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && this.NodeMap != "" && this.NodeMap != null)
            {
                TreeNode MovingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                string[] NodeIndexes = this.NodeMap.Split('|');
                TreeNodeCollection InsertCollection = this.ArchiveTreeView.Nodes;
                for (int i = 0; i < NodeIndexes.Length - 1; i++)
                {
                    InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
                }

                if (InsertCollection != null)
                {
                    InsertCollection.Insert(Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), (TreeNode)MovingNode.Clone());
                    this.ArchiveTreeView.SelectedNode = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])];
                    Archive.MoveItem(MovingNode.FullPath, InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])].FullPath);
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

        #endregion
        
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

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Edited && MessageBox.Show("You have unsaved changes.\nAre you sure you want to start a new file?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Archive = new RARC() { KeepFileIDsSynced = true };
            ArchiveTreeView.Nodes.Clear();
            Archive["NewArchive"] = null;
            RootNameTextBox.Text = Archive.Root.Name;
            KeepIDsSyncedCheckBox.Checked = Archive.KeepFileIDsSynced;

            Edited = false;
            SetControlsEnabled();
            MainToolStripProgressBar.Value = 100;
            MainToolStripStatusLabel.Text = "Created a new Archive.";
            Text = $"WiiExplorer {Application.ProductVersion} - New Archive";
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(Edited && MessageBox.Show("You have unsaved changes.\nAre you sure you want to open another file?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No))
            {
                ofd.InitialDirectory = Properties.Settings.Default.PreviousOpenArchivePath;
                string tmp = Properties.Settings.Default.PreviousOpenArchivePath;
                if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != "")
                    OpenArchive(ofd.FileName);
            }
        }

        private void OpenArchive(string Filename)
        {
            ofd.FileName = Filename;
            MainToolStripProgressBar.Value = 0;
            Archive = YAZ0.Check(Filename) ? new RARC(YAZ0.Decompress(Filename), Filename) : new RARC(Filename);
            MainToolStripProgressBar.Value = 20;
            ArchiveTreeView.Nodes.Clear();
            ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
            RootNameTextBox.Text = Archive.Root.Name;
            KeepIDsSyncedCheckBox.Checked = Archive.KeepFileIDsSynced;
            Edited = false;
            SetControlsEnabled();
            MainToolStripProgressBar.Value = 100;
            MainToolStripStatusLabel.Text = $"Archive loaded successfully!";
            Text = $"WiiExplorer {Application.ProductVersion} - {new FileInfo(Filename).Name}";
            Properties.Settings.Default.PreviousOpenArchivePath = new FileInfo(Filename).DirectoryName;
            Properties.Settings.Default.Save();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Archive.FileName is null)
            {
                sfd.InitialDirectory = Properties.Settings.Default.PreviousSaveArchivePath;
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
            sfd.InitialDirectory = Properties.Settings.Default.PreviousSaveArchivePath;
            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName != "")
                SaveArchive(sfd.FileName);
        }

        private void SaveArchive(string Filename)
        {
            MainToolStripProgressBar.Value = 0;
            Archive.FromTreeNode(ArchiveTreeView);
            MainToolStripProgressBar.Value = 70;
            Archive.Save(Filename);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            if (Program.Yaz0Mode != 0)
                Yaz0BackgroundWorker.RunWorkerAsync(Filename);
            SetControlsEnabled(false, true);
            ItemPropertiesToolStripMenuItem.Enabled = false;
            while (Yaz0BackgroundWorker.IsBusy)
            {
                MainToolStripStatusLabel.Text = $"{(Program.Yaz0Mode == 2 ? "Fast ":"")}Yaz0 Encoding, Please Wait. ({timer.Elapsed.ToString("mm\\:ss")} Elapsed)";
                Application.DoEvents();
            }
            timer.Stop();

            Edited = false;
            MainToolStripProgressBar.Value = 100;
            MainToolStripStatusLabel.Text = $"Archive saved successfully!{(Program.Yaz0Mode != 0 ? $"({timer.Elapsed.ToString("mm\\:ss")} Elapsed)":"")}";
            SetControlsEnabled(affectall:true);
            Text = $"WiiExplorer {Application.ProductVersion} - {new FileInfo(Filename).Name}";
            Properties.Settings.Default.PreviousSaveArchivePath = new FileInfo(Filename).DirectoryName;
            Properties.Settings.Default.Save();
        }

        private void AddFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RootNameTextBox.Focused)
            {
                RootNameTextBox.SelectAll();
                return;
            }

            TreeNode tmp = ArchiveTreeView.SelectedNode;
            Fileofd.InitialDirectory = Properties.Settings.Default.PreviousAddFilePath;
            if (Fileofd.ShowDialog() == DialogResult.OK && Fileofd.FileName != "")
            {
                ArchiveTreeView.SelectedNode = tmp;
                AddItemToRARC(Fileofd.FileNames);
                MainToolStripStatusLabel.Text = $"{Fileofd.FileNames.Length} File{(Fileofd.FileNames.Length > 1 ? "s":"")} added.";
                Properties.Settings.Default.PreviousAddFilePath = new FileInfo(Fileofd.FileName).DirectoryName;
                Properties.Settings.Default.Save();
            }
            ArchiveTreeView.SelectedNode = tmp;

            Edited = true;
        }

        private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode newnode = new TreeNode("New Folder") { ImageIndex = 0, SelectedImageIndex = 0 };

            //SelectedNode is NULL, put the new file on the root
            if (ArchiveTreeView.SelectedNode == null)
                ArchiveTreeView.Nodes.Add(newnode);
            //Determine where to put it otherwise
            else
            {
                if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory)
                    ArchiveTreeView.SelectedNode.Nodes.Add(newnode);
                else if (ArchiveTreeView.SelectedNode.Parent == null)
                    ArchiveTreeView.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, newnode);
                else
                    ArchiveTreeView.SelectedNode.Parent.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, newnode);
            }

            ArchiveTreeView.SelectedNode = newnode;
            Archive[newnode.FullPath] = new RARC.Directory() { Name = "NewFolder" };

            Edited = true;
            MainToolStripStatusLabel.Text = $"New Folder added.";
        }

        private void AddItemToRARC(string[] FileNames)
        {
            for (int i = 0; i < FileNames.Length; i++)
            {
                FileInfo fi = new FileInfo(FileNames[i]);
                int imageindex = 2;
                if (ArchiveImageList.Images.ContainsKey("*" + fi.Extension))
                    imageindex = ArchiveImageList.Images.IndexOfKey("*" + fi.Extension);

                RARC.File CurrentFile = new RARC.File(Fileofd.FileNames[i]);
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
                Archive[NewTreeNode.FullPath] = CurrentFile;
            }
        }

        private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;

            MainToolStripStatusLabel.Text = $"\"{ArchiveTreeView.SelectedNode.Text}\" has been removed.";
            Archive[ArchiveTreeView.SelectedNode.FullPath] = null;
            ArchiveTreeView.SelectedNode.Remove();

            Edited = true;
        }

        private void RenameSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;
            RenameForm RN = new RenameForm(Archive[ArchiveTreeView.SelectedNode.FullPath]);
            string tmp = ArchiveTreeView.SelectedNode.Text;
            if (RN.ShowDialog() != DialogResult.OK)
                return;
            string oldpath = ArchiveTreeView.SelectedNode.FullPath;
            ArchiveTreeView.SelectedNode.Text = RN.NameTextBox.Text+RN.ExtensionTextBox.Text;
            Archive.MoveItem(oldpath, ArchiveTreeView.SelectedNode.FullPath);

            Edited = true;
            MainToolStripStatusLabel.Text = $"\"{tmp}\" renamed to \"{RN.NameTextBox.Text+RN.ExtensionTextBox.Text}\"";
        }

        private void ExportSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportArchiveFile(Archive[ArchiveTreeView.SelectedNode.FullPath]);
        }

        private void ExportAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exportsfd.Filter = "Directory|directory";
            Exportsfd.FileName = RootNameTextBox.Text;
            if (Exportsfd.ShowDialog() == DialogResult.OK && Exportsfd.FileName != "")
            {
                MainToolStripProgressBar.Value = 0;
                Archive.Export(new DirectoryInfo(Exportsfd.FileName).Parent.FullName, true);
                MainToolStripProgressBar.Value = 100;
                MainToolStripStatusLabel.Text = $"Full Archive \"{Archive.Root.Name}\" has been saved!";
                Properties.Settings.Default.PreviousExportPath = new DirectoryInfo(Exportsfd.FileName).Parent.FullName;
                Properties.Settings.Default.Save();
            }
        }

        private void ReplaceSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode == null)
                return;

            string OldPath = ArchiveTreeView.SelectedNode.FullPath;
            dynamic Item = Archive[OldPath];
            if (Item is RARC.Directory dir)
            {
                CommonOpenFileDialog BFB = new CommonOpenFileDialog() { InitialDirectory = Properties.Settings.Default.PreviousAddFilePath, Multiselect = false, IsFolderPicker = true };
                if (BFB.ShowDialog() == CommonFileDialogResult.Ok && !BFB.FileName.Equals(""))
                {
                    string oldname = ArchiveTreeView.SelectedNode.Text;
                    
                    dir.Clear();
                    ArchiveTreeView.SelectedNode.Nodes.Clear();
                    ArchiveTreeView.SelectedNode.Text = new DirectoryInfo(BFB.FileName).Name;
                    Archive.MoveItem(dir.FullPath, ArchiveTreeView.SelectedNode.FullPath);
                    dir.CreateFromFolder(BFB.FileName);

                    ArchiveTreeView.Nodes.Clear();
                    ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                    ArchiveTreeView.SelectedNode = ArchiveTreeView.Nodes.FindTreeNodeByFullPath(OldPath);
                    Properties.Settings.Default.PreviousAddFilePath = new DirectoryInfo(BFB.FileName).Parent.FullName;
                    Properties.Settings.Default.Save();
                    MainToolStripStatusLabel.Text = $"\"{new DirectoryInfo(BFB.FileName).Name}\" has replaced \"{oldname}\"!";
                }
            }
            else if (Item is RARC.File file)
            {
                Fileofd.InitialDirectory = Properties.Settings.Default.PreviousAddFilePath;
                if (Fileofd.ShowDialog() == DialogResult.OK && Fileofd.FileName != "")
                {
                    string oldname = ArchiveTreeView.SelectedNode.Text;
                    FileInfo fi = new FileInfo(Fileofd.FileName);
                    int imageindex = 2;
                    if (ArchiveImageList.Images.ContainsKey("*" + fi.Extension))
                        imageindex = ArchiveImageList.Images.IndexOfKey("*" + fi.Extension);

                    ArchiveTreeView.SelectedNode.Text = fi.Name;
                    ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = imageindex;
                    Archive[OldPath] = null;
                    Archive[ArchiveTreeView.SelectedNode.FullPath] = new RARC.File(Fileofd.FileName);
                    
                    ArchiveTreeView.SelectedNode = ArchiveTreeView.Nodes.FindTreeNodeByFullPath(OldPath);


                    Properties.Settings.Default.PreviousAddFilePath = new FileInfo(Fileofd.FileName).DirectoryName;
                    Properties.Settings.Default.Save();
                    MainToolStripStatusLabel.Text = $"\"{new FileInfo(Fileofd.FileName).Name}\" has replaced \"{oldname}\"!";
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = Yaz0BackgroundWorker.IsBusy || (e.CloseReason == CloseReason.UserClosing & Edited) && MessageBox.Show("You have unsaved changes.\nAre you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;

        private void Yaz0ToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e) => Program.Yaz0Mode = (byte)Yaz0ToolStripComboBox.SelectedIndex;

        private void ExportArchiveFile(dynamic Item)
        {
            if (Item == null)
                return;

            Exportsfd.FileName = Item.Name;
            if (Item is RARC.Directory)
            {
                Exportsfd.Filter = "Directory|directory";

                if (Exportsfd.ShowDialog() == DialogResult.OK && Exportsfd.FileName != "")
                {
                    if (Directory.Exists(Exportsfd.FileName) && MessageBox.Show("The target directory already exists.\nAre you sure you want to replace it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;
                    else if (Directory.Exists(Exportsfd.FileName))
                        Directory.Delete(Exportsfd.FileName, true);
                    
                    Directory.CreateDirectory(Exportsfd.FileName);
                    Item.Export(Exportsfd.FileName);
                    MainToolStripStatusLabel.Text = $"\"{Item.Name}\" has been saved!";
                    Properties.Settings.Default.PreviousExportPath = new DirectoryInfo(Exportsfd.FileName).Parent.FullName;
                    Properties.Settings.Default.Save();
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
                Exportsfd.InitialDirectory = Properties.Settings.Default.PreviousExportPath;
                if (Exportsfd.ShowDialog() == DialogResult.OK && Exportsfd.FileName != "")
                {
                    Item.Save(Exportsfd.FileName);
                    MainToolStripStatusLabel.Text = $"\"{Item.Name}\" has been saved!";

                    Properties.Settings.Default.PreviousExportPath = new FileInfo(Exportsfd.FileName).DirectoryName;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void ArchiveTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ItemPropertiesToolStripMenuItem.Enabled = false;
            if (ArchiveTreeView.SelectedNode == null)
                MainToolStripStatusLabel.Text = "No File Selected";
            else if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory dir)
                MainToolStripStatusLabel.Text = $"Folder \"{dir.Name}\" ({dir.Items.Count} Item{(dir.Items.Count > 1 ? "s" : "")})";
            else if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.File file)
            {
                MainToolStripStatusLabel.Text = $"File \"{file.Name}\" ({file.FileData.Length} Bytes)";
                ItemPropertiesToolStripMenuItem.Enabled = true;
            }
        }

        private void ImportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog BFB = new CommonOpenFileDialog() { InitialDirectory = Properties.Settings.Default.PreviousAddFilePath, Multiselect = false, IsFolderPicker = true };
            if (BFB.ShowDialog() == CommonFileDialogResult.Ok && !BFB.FileName.Equals(""))
            {
                TreeNode newnode = new TreeNode(new DirectoryInfo(BFB.FileName).Name) { ImageIndex = 0, SelectedImageIndex = 0 };

                //SelectedNode is NULL, put the new file on the root
                if (ArchiveTreeView.SelectedNode == null)
                    ArchiveTreeView.Nodes.Add(newnode);
                //Determine where to put it otherwise
                else
                {
                    if (Archive[ArchiveTreeView.SelectedNode.FullPath] is RARC.Directory)
                        ArchiveTreeView.SelectedNode.Nodes.Add(newnode);
                    else if (ArchiveTreeView.SelectedNode.Parent == null)
                        ArchiveTreeView.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, newnode);
                    else
                        ArchiveTreeView.SelectedNode.Parent.Nodes.Insert(ArchiveTreeView.SelectedNode.Index + 1, newnode);
                }

                ArchiveTreeView.SelectedNode = newnode;
                RARC.Directory dir = new RARC.Directory(BFB.FileName);
                Archive[newnode.FullPath] = dir;

                ArchiveTreeView.Nodes.Clear();
                ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                ArchiveTreeView.SelectedNode = newnode;
                newnode.Expand();
                Properties.Settings.Default.PreviousAddFilePath = new DirectoryInfo(BFB.FileName).Parent.FullName;
                Properties.Settings.Default.Save();
                Edited = true;
                MainToolStripStatusLabel.Text = $"Folder \"{BFB.FileName}\" Imported.";
            }
        }

        private void NewFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Edited && MessageBox.Show("You have unsaved changes.\nAre you sure you want to start a new file?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            CommonOpenFileDialog BFB = new CommonOpenFileDialog() { InitialDirectory = Properties.Settings.Default.PreviousAddFilePath, Multiselect = false, IsFolderPicker = true };
            if (BFB.ShowDialog() == CommonFileDialogResult.Ok && !BFB.FileName.Equals(""))
            {
                Archive = new RARC();
                ArchiveTreeView.Nodes.Clear();
                Archive.Import(BFB.FileName);
                RootNameTextBox.Text = Archive.Root.Name;
                KeepIDsSyncedCheckBox.Checked = Archive.KeepFileIDsSynced;
                ArchiveTreeView.Nodes.AddRange(Archive.ToTreeNode(0, ArchiveImageList));
                Properties.Settings.Default.PreviousAddFilePath = new DirectoryInfo(BFB.FileName).Parent.FullName;
                Properties.Settings.Default.Save();

                Edited = true;
                SetControlsEnabled();
                MainToolStripProgressBar.Value = 100;
                MainToolStripStatusLabel.Text = $"Created a new Archive from \"{BFB.FileName}\"";
                Text = $"WiiExplorer {Application.ProductVersion} - New Archive";
            }
        }

        private void ArchiveTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ArchiveContextMenuStrip.Show(ArchiveTreeView, e.Location);
            }
        }

        private void Yaz0BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            YAZ0.Compress((string)e.Argument, Program.Yaz0Mode == 2);
        }

        private void ItemPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveTreeView.SelectedNode is null)
            {
                ItemPropertiesToolStripMenuItem.Enabled = false;
                return;
            }
            FilePropertyForm FPF = new FilePropertyForm((RARC.File)Archive[ArchiveTreeView.SelectedNode.FullPath], !Archive.KeepFileIDsSynced);
            if (FPF.ShowDialog() == DialogResult.OK)
                Edited = true;
        }

        private void RootNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Archive.Root.Name = RootNameTextBox.Text;
            Edited = true;
        }

        private void KeepIDsSyncedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Archive != null)
            {
                Archive.KeepFileIDsSynced = KeepIDsSyncedCheckBox.Checked;
                Edited = true;
            }
        }
    }
}