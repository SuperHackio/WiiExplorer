namespace WiiExplorer
{
    partial class NewArchiveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewArchiveForm));
            SelectionSplitContainer = new SplitContainer();
            FormatListView = new ListView();
            DescriptionTextLabel = new Label();
            DescriptionHeaderLabel = new Label();
            SelectButton = new Button();
            ((System.ComponentModel.ISupportInitialize)SelectionSplitContainer).BeginInit();
            SelectionSplitContainer.Panel1.SuspendLayout();
            SelectionSplitContainer.Panel2.SuspendLayout();
            SelectionSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // SelectionSplitContainer
            // 
            SelectionSplitContainer.Dock = DockStyle.Fill;
            SelectionSplitContainer.IsSplitterFixed = true;
            SelectionSplitContainer.Location = new Point(0, 0);
            SelectionSplitContainer.Name = "SelectionSplitContainer";
            // 
            // SelectionSplitContainer.Panel1
            // 
            SelectionSplitContainer.Panel1.Controls.Add(FormatListView);
            // 
            // SelectionSplitContainer.Panel2
            // 
            SelectionSplitContainer.Panel2.Controls.Add(DescriptionTextLabel);
            SelectionSplitContainer.Panel2.Controls.Add(DescriptionHeaderLabel);
            SelectionSplitContainer.Size = new Size(415, 293);
            SelectionSplitContainer.SplitterDistance = 128;
            SelectionSplitContainer.SplitterWidth = 3;
            SelectionSplitContainer.TabIndex = 1;
            // 
            // FormatListView
            // 
            FormatListView.Dock = DockStyle.Fill;
            FormatListView.Font = new Font("Consolas", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            FormatListView.FullRowSelect = true;
            FormatListView.GridLines = true;
            FormatListView.Location = new Point(0, 0);
            FormatListView.MultiSelect = false;
            FormatListView.Name = "FormatListView";
            FormatListView.Size = new Size(128, 293);
            FormatListView.TabIndex = 0;
            FormatListView.UseCompatibleStateImageBehavior = false;
            FormatListView.View = View.List;
            FormatListView.SelectedIndexChanged += FormatListView_SelectedIndexChanged;
            FormatListView.MouseDoubleClick += FormatListView_MouseDoubleClick;
            // 
            // DescriptionTextLabel
            // 
            DescriptionTextLabel.Dock = DockStyle.Fill;
            DescriptionTextLabel.Font = new Font("Consolas", 10F);
            DescriptionTextLabel.Location = new Point(0, 22);
            DescriptionTextLabel.Margin = new Padding(1, 0, 1, 0);
            DescriptionTextLabel.Name = "DescriptionTextLabel";
            DescriptionTextLabel.Size = new Size(284, 271);
            DescriptionTextLabel.TabIndex = 2;
            // 
            // DescriptionHeaderLabel
            // 
            DescriptionHeaderLabel.Dock = DockStyle.Top;
            DescriptionHeaderLabel.Font = new Font("Consolas", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            DescriptionHeaderLabel.Location = new Point(0, 0);
            DescriptionHeaderLabel.Name = "DescriptionHeaderLabel";
            DescriptionHeaderLabel.Size = new Size(284, 22);
            DescriptionHeaderLabel.TabIndex = 2;
            DescriptionHeaderLabel.Text = "Format Description";
            DescriptionHeaderLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // SelectButton
            // 
            SelectButton.Dock = DockStyle.Bottom;
            SelectButton.FlatStyle = FlatStyle.Flat;
            SelectButton.Location = new Point(0, 293);
            SelectButton.Name = "SelectButton";
            SelectButton.Size = new Size(415, 20);
            SelectButton.TabIndex = 1;
            SelectButton.Text = "Select";
            SelectButton.UseVisualStyleBackColor = true;
            SelectButton.Click += SelectButton_Click;
            // 
            // NewArchiveForm
            // 
            AcceptButton = SelectButton;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 313);
            Controls.Add(SelectionSplitContainer);
            Controls.Add(SelectButton);
            Font = new Font("Microsoft Sans Serif", 8.25F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(431, 352);
            Name = "NewArchiveForm";
            Text = "WiiExplorer - Select Archive Format";
            SelectionSplitContainer.Panel1.ResumeLayout(false);
            SelectionSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SelectionSplitContainer).EndInit();
            SelectionSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SplitContainer SelectionSplitContainer;
        private ListView FormatListView;
        private Label DescriptionTextLabel;
        private Label DescriptionHeaderLabel;
        private Button SelectButton;
    }
}