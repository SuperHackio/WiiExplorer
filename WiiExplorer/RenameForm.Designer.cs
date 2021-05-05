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
            this.InfoLabel = new System.Windows.Forms.Label();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.NameTextBox = new WiiExplorer.ColourTextBox();
            this.ExtensionTextBox = new WiiExplorer.ColourTextBox();
            this.ExtensionLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.TextPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoLabel
            // 
            resources.ApplyResources(this.InfoLabel, "InfoLabel");
            this.InfoLabel.Name = "InfoLabel";
            // 
            // TextPanel
            // 
            resources.ApplyResources(this.TextPanel, "TextPanel");
            this.TextPanel.Controls.Add(this.NameTextBox);
            this.TextPanel.Controls.Add(this.ExtensionTextBox);
            this.TextPanel.Name = "TextPanel";
            // 
            // NameTextBox
            // 
            resources.ApplyResources(this.NameTextBox, "NameTextBox");
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NameTextBox.Name = "NameTextBox";
            // 
            // ExtensionTextBox
            // 
            resources.ApplyResources(this.ExtensionTextBox, "ExtensionTextBox");
            this.ExtensionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtensionTextBox.Name = "ExtensionTextBox";
            // 
            // ExtensionLabel
            // 
            resources.ApplyResources(this.ExtensionLabel, "ExtensionLabel");
            this.ExtensionLabel.Name = "ExtensionLabel";
            // 
            // OKButton
            // 
            resources.ApplyResources(this.OKButton, "OKButton");
            this.OKButton.Name = "OKButton";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // DiscardButton
            // 
            resources.ApplyResources(this.DiscardButton, "DiscardButton");
            this.DiscardButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // RenameForm
            // 
            this.AcceptButton = this.OKButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DiscardButton;
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.ExtensionLabel);
            this.Controls.Add(this.TextPanel);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RenameForm_FormClosing);
            this.TextPanel.ResumeLayout(false);
            this.TextPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label InfoLabel;
        public ColourTextBox NameTextBox;
        private System.Windows.Forms.Panel TextPanel;
        public ColourTextBox ExtensionTextBox;
        private System.Windows.Forms.Label ExtensionLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button DiscardButton;
    }
}