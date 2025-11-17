namespace WiiExplorer
{
    partial class RenameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameForm));
            NameColorTextBox = new DarkModeForms.ColorTextBox();
            ExtensionColorTextBox = new DarkModeForms.ColorTextBox();
            SubmitButton = new Button();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            NameLabel = new Label();
            ExtensionLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // NameColorTextBox
            // 
            NameColorTextBox.Dock = DockStyle.Bottom;
            NameColorTextBox.Location = new Point(1, 34);
            NameColorTextBox.Name = "NameColorTextBox";
            NameColorTextBox.Size = new Size(295, 20);
            NameColorTextBox.TabIndex = 0;
            // 
            // ExtensionColorTextBox
            // 
            ExtensionColorTextBox.Dock = DockStyle.Bottom;
            ExtensionColorTextBox.Location = new Point(0, 34);
            ExtensionColorTextBox.Name = "ExtensionColorTextBox";
            ExtensionColorTextBox.Size = new Size(77, 20);
            ExtensionColorTextBox.TabIndex = 1;
            // 
            // SubmitButton
            // 
            SubmitButton.Dock = DockStyle.Fill;
            SubmitButton.FlatStyle = FlatStyle.Flat;
            SubmitButton.Location = new Point(0, 0);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(376, 25);
            SubmitButton.TabIndex = 2;
            SubmitButton.Text = "OK";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(SubmitButton);
            splitContainer1.Size = new Size(376, 83);
            splitContainer1.SplitterDistance = 54;
            splitContainer1.TabIndex = 11;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(NameLabel);
            splitContainer2.Panel1.Controls.Add(NameColorTextBox);
            splitContainer2.Panel1.Padding = new Padding(1, 0, 0, 0);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(ExtensionLabel);
            splitContainer2.Panel2.Controls.Add(ExtensionColorTextBox);
            splitContainer2.Panel2.Padding = new Padding(0, 0, 1, 0);
            splitContainer2.Size = new Size(376, 54);
            splitContainer2.SplitterDistance = 296;
            splitContainer2.SplitterWidth = 2;
            splitContainer2.TabIndex = 11;
            // 
            // NameLabel
            // 
            NameLabel.Dock = DockStyle.Fill;
            NameLabel.Location = new Point(1, 0);
            NameLabel.Name = "NameLabel";
            NameLabel.Padding = new Padding(0, 10, 0, 0);
            NameLabel.Size = new Size(295, 34);
            NameLabel.TabIndex = 11;
            NameLabel.Text = "Type the new name, then press Enter";
            NameLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ExtensionLabel
            // 
            ExtensionLabel.Dock = DockStyle.Fill;
            ExtensionLabel.Location = new Point(0, 0);
            ExtensionLabel.Name = "ExtensionLabel";
            ExtensionLabel.Padding = new Padding(0, 10, 0, 0);
            ExtensionLabel.Size = new Size(77, 34);
            ExtensionLabel.TabIndex = 11;
            ExtensionLabel.Text = "Extension";
            ExtensionLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RenameForm
            // 
            AcceptButton = SubmitButton;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 83);
            Controls.Add(splitContainer1);
            Font = new Font("Microsoft Sans Serif", 8.25F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RenameForm";
            Text = "WiiExplorer - Rename []";
            FormClosing += RenameForm_FormClosing;
            Shown += RenameForm_Shown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DarkModeForms.ColorTextBox NameColorTextBox;
        private DarkModeForms.ColorTextBox ExtensionColorTextBox;
        private Button SubmitButton;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label NameLabel;
        private Label ExtensionLabel;
    }
}