namespace WiiExplorer
{
    partial class FilePropertyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilePropertyForm));
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new WiiExplorer.ColourTextBox();
            this.IDNumericUpDown = new WiiExplorer.ColourNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.FileSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.AutoDetectSettingsButton = new System.Windows.Forms.Button();
            this.DVDRadioButton = new System.Windows.Forms.RadioButton();
            this.ARAMRadioButton = new System.Windows.Forms.RadioButton();
            this.MRAMRadioButton = new System.Windows.Forms.RadioButton();
            this.IsYAZ0CheckBox = new System.Windows.Forms.CheckBox();
            this.IsCompressedCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.DiscardButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.IDNumericUpDown)).BeginInit();
            this.FileSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            resources.ApplyResources(this.NameLabel, "NameLabel");
            this.NameLabel.Name = "NameLabel";
            // 
            // NameTextBox
            // 
            resources.ApplyResources(this.NameTextBox, "NameTextBox");
            this.NameTextBox.Name = "NameTextBox";
            // 
            // IDNumericUpDown
            // 
            resources.ApplyResources(this.IDNumericUpDown, "IDNumericUpDown");
            this.IDNumericUpDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.IDNumericUpDown.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.IDNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.IDNumericUpDown.Name = "IDNumericUpDown";
            this.IDNumericUpDown.TextValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FileSettingsGroupBox
            // 
            resources.ApplyResources(this.FileSettingsGroupBox, "FileSettingsGroupBox");
            this.FileSettingsGroupBox.Controls.Add(this.AutoDetectSettingsButton);
            this.FileSettingsGroupBox.Controls.Add(this.DVDRadioButton);
            this.FileSettingsGroupBox.Controls.Add(this.ARAMRadioButton);
            this.FileSettingsGroupBox.Controls.Add(this.MRAMRadioButton);
            this.FileSettingsGroupBox.Controls.Add(this.IsYAZ0CheckBox);
            this.FileSettingsGroupBox.Controls.Add(this.IsCompressedCheckBox);
            this.FileSettingsGroupBox.Name = "FileSettingsGroupBox";
            this.FileSettingsGroupBox.TabStop = false;
            // 
            // AutoDetectSettingsButton
            // 
            resources.ApplyResources(this.AutoDetectSettingsButton, "AutoDetectSettingsButton");
            this.AutoDetectSettingsButton.Name = "AutoDetectSettingsButton";
            this.AutoDetectSettingsButton.UseVisualStyleBackColor = true;
            this.AutoDetectSettingsButton.Click += new System.EventHandler(this.AutoDetectSettingsButton_Click);
            // 
            // DVDRadioButton
            // 
            resources.ApplyResources(this.DVDRadioButton, "DVDRadioButton");
            this.DVDRadioButton.Name = "DVDRadioButton";
            this.DVDRadioButton.UseVisualStyleBackColor = true;
            // 
            // ARAMRadioButton
            // 
            resources.ApplyResources(this.ARAMRadioButton, "ARAMRadioButton");
            this.ARAMRadioButton.Name = "ARAMRadioButton";
            this.ARAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // MRAMRadioButton
            // 
            resources.ApplyResources(this.MRAMRadioButton, "MRAMRadioButton");
            this.MRAMRadioButton.Checked = true;
            this.MRAMRadioButton.Name = "MRAMRadioButton";
            this.MRAMRadioButton.TabStop = true;
            this.MRAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // IsYAZ0CheckBox
            // 
            resources.ApplyResources(this.IsYAZ0CheckBox, "IsYAZ0CheckBox");
            this.IsYAZ0CheckBox.Name = "IsYAZ0CheckBox";
            this.IsYAZ0CheckBox.UseVisualStyleBackColor = true;
            // 
            // IsCompressedCheckBox
            // 
            resources.ApplyResources(this.IsCompressedCheckBox, "IsCompressedCheckBox");
            this.IsCompressedCheckBox.Name = "IsCompressedCheckBox";
            this.IsCompressedCheckBox.UseVisualStyleBackColor = true;
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
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // FilePropertyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.FileSettingsGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IDNumericUpDown);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FilePropertyForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FilePropertyForm_FormClosing);
            this.Load += new System.EventHandler(this.FilePropertyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IDNumericUpDown)).EndInit();
            this.FileSettingsGroupBox.ResumeLayout(false);
            this.FileSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private ColourTextBox NameTextBox;
        private ColourNumericUpDown IDNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FileSettingsGroupBox;
        private System.Windows.Forms.CheckBox IsYAZ0CheckBox;
        private System.Windows.Forms.CheckBox IsCompressedCheckBox;
        private System.Windows.Forms.RadioButton DVDRadioButton;
        private System.Windows.Forms.RadioButton ARAMRadioButton;
        private System.Windows.Forms.RadioButton MRAMRadioButton;
        private System.Windows.Forms.Button AutoDetectSettingsButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button DiscardButton;
    }
}