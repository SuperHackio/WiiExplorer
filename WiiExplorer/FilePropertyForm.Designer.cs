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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.IDNumericUpDown = new System.Windows.Forms.NumericUpDown();
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
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 15);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(35, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(53, 13);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(112, 20);
            this.NameTextBox.TabIndex = 1;
            // 
            // IDNumericUpDown
            // 
            this.IDNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IDNumericUpDown.Location = new System.Drawing.Point(214, 13);
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
            this.IDNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.IDNumericUpDown.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File ID";
            // 
            // FileSettingsGroupBox
            // 
            this.FileSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileSettingsGroupBox.Controls.Add(this.AutoDetectSettingsButton);
            this.FileSettingsGroupBox.Controls.Add(this.DVDRadioButton);
            this.FileSettingsGroupBox.Controls.Add(this.ARAMRadioButton);
            this.FileSettingsGroupBox.Controls.Add(this.MRAMRadioButton);
            this.FileSettingsGroupBox.Controls.Add(this.IsYAZ0CheckBox);
            this.FileSettingsGroupBox.Controls.Add(this.IsCompressedCheckBox);
            this.FileSettingsGroupBox.Location = new System.Drawing.Point(12, 39);
            this.FileSettingsGroupBox.Name = "FileSettingsGroupBox";
            this.FileSettingsGroupBox.Size = new System.Drawing.Size(260, 137);
            this.FileSettingsGroupBox.TabIndex = 5;
            this.FileSettingsGroupBox.TabStop = false;
            this.FileSettingsGroupBox.Text = "File Settings";
            // 
            // AutoDetectSettingsButton
            // 
            this.AutoDetectSettingsButton.Location = new System.Drawing.Point(221, 0);
            this.AutoDetectSettingsButton.Name = "AutoDetectSettingsButton";
            this.AutoDetectSettingsButton.Size = new System.Drawing.Size(38, 23);
            this.AutoDetectSettingsButton.TabIndex = 5;
            this.AutoDetectSettingsButton.Text = "Auto";
            this.AutoDetectSettingsButton.UseVisualStyleBackColor = true;
            this.AutoDetectSettingsButton.Click += new System.EventHandler(this.AutoDetectSettingsButton_Click);
            // 
            // DVDRadioButton
            // 
            this.DVDRadioButton.AutoSize = true;
            this.DVDRadioButton.Location = new System.Drawing.Point(6, 111);
            this.DVDRadioButton.Name = "DVDRadioButton";
            this.DVDRadioButton.Size = new System.Drawing.Size(94, 17);
            this.DVDRadioButton.TabIndex = 4;
            this.DVDRadioButton.Text = "Read Off DVD";
            this.DVDRadioButton.UseVisualStyleBackColor = true;
            // 
            // ARAMRadioButton
            // 
            this.ARAMRadioButton.AutoSize = true;
            this.ARAMRadioButton.Location = new System.Drawing.Point(6, 88);
            this.ARAMRadioButton.Name = "ARAMRadioButton";
            this.ARAMRadioButton.Size = new System.Drawing.Size(253, 17);
            this.ARAMRadioButton.TabIndex = 3;
            this.ARAMRadioButton.Text = "Pre-Load File to Auxiliary RAM (GameCube Only)";
            this.ARAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // MRAMRadioButton
            // 
            this.MRAMRadioButton.AutoSize = true;
            this.MRAMRadioButton.Checked = true;
            this.MRAMRadioButton.Location = new System.Drawing.Point(6, 65);
            this.MRAMRadioButton.Name = "MRAMRadioButton";
            this.MRAMRadioButton.Size = new System.Drawing.Size(152, 17);
            this.MRAMRadioButton.TabIndex = 2;
            this.MRAMRadioButton.TabStop = true;
            this.MRAMRadioButton.Text = "Pre-Load File to Main RAM";
            this.MRAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // IsYAZ0CheckBox
            // 
            this.IsYAZ0CheckBox.AutoSize = true;
            this.IsYAZ0CheckBox.Location = new System.Drawing.Point(6, 42);
            this.IsYAZ0CheckBox.Name = "IsYAZ0CheckBox";
            this.IsYAZ0CheckBox.Size = new System.Drawing.Size(127, 17);
            this.IsYAZ0CheckBox.TabIndex = 1;
            this.IsYAZ0CheckBox.Text = "Is Compression YAZ0";
            this.IsYAZ0CheckBox.UseVisualStyleBackColor = true;
            // 
            // IsCompressedCheckBox
            // 
            this.IsCompressedCheckBox.AutoSize = true;
            this.IsCompressedCheckBox.Location = new System.Drawing.Point(6, 19);
            this.IsCompressedCheckBox.Name = "IsCompressedCheckBox";
            this.IsCompressedCheckBox.Size = new System.Drawing.Size(95, 17);
            this.IsCompressedCheckBox.TabIndex = 0;
            this.IsCompressedCheckBox.Text = "Is Compressed";
            this.IsCompressedCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OKButton.Location = new System.Drawing.Point(217, 182);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(55, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // DiscardButton
            // 
            this.DiscardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DiscardButton.Location = new System.Drawing.Point(155, 182);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(55, 23);
            this.DiscardButton.TabIndex = 6;
            this.DiscardButton.Text = "Cancel";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // FilePropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 217);
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.FileSettingsGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IDNumericUpDown);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilePropertyForm";
            this.Text = "File Properties";
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
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.NumericUpDown IDNumericUpDown;
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