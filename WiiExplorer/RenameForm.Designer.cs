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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.ExtensionTextBox = new System.Windows.Forms.TextBox();
            this.ExtensionLabel = new System.Windows.Forms.Label();
            this.TextPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameTextBox.Location = new System.Drawing.Point(0, 0);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(265, 20);
            this.NameTextBox.TabIndex = 0;
            // 
            // InfoLabel
            // 
            this.InfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoLabel.Location = new System.Drawing.Point(0, 0);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(265, 21);
            this.InfoLabel.TabIndex = 1;
            this.InfoLabel.Text = "Type the new name, then press Enter";
            this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TextPanel
            // 
            this.TextPanel.Controls.Add(this.NameTextBox);
            this.TextPanel.Controls.Add(this.ExtensionTextBox);
            this.TextPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextPanel.Location = new System.Drawing.Point(0, 21);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(324, 20);
            this.TextPanel.TabIndex = 2;
            // 
            // ExtensionTextBox
            // 
            this.ExtensionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtensionTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExtensionTextBox.Location = new System.Drawing.Point(265, 0);
            this.ExtensionTextBox.Name = "ExtensionTextBox";
            this.ExtensionTextBox.Size = new System.Drawing.Size(59, 20);
            this.ExtensionTextBox.TabIndex = 1;
            // 
            // ExtensionLabel
            // 
            this.ExtensionLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExtensionLabel.Location = new System.Drawing.Point(265, 0);
            this.ExtensionLabel.Name = "ExtensionLabel";
            this.ExtensionLabel.Size = new System.Drawing.Size(59, 21);
            this.ExtensionLabel.TabIndex = 3;
            this.ExtensionLabel.Text = "Extension";
            this.ExtensionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RenameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 41);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.ExtensionLabel);
            this.Controls.Add(this.TextPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 80);
            this.Name = "RenameForm";
            this.Text = "WiiExplorer - Rename []";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RenameForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZoneForm_KeyDown);
            this.TextPanel.ResumeLayout(false);
            this.TextPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label InfoLabel;
        public System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Panel TextPanel;
        public System.Windows.Forms.TextBox ExtensionTextBox;
        private System.Windows.Forms.Label ExtensionLabel;
    }
}