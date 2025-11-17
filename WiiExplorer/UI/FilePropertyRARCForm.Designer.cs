namespace WiiExplorer
{
    partial class FilePropertyRARCForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilePropertyRARCForm));
            NameLabel = new Label();
            FileIDLabel = new Label();
            NameColorTextBox = new DarkModeForms.ColorTextBox();
            FileIDColorNumericUpDown = new DarkModeForms.ColorNumericUpDown();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            OKButton = new Button();
            HashLabel = new Label();
            FileSettingsGroupBox = new GroupBox();
            DVDRadioButton = new RadioButton();
            ARAMRadioButton = new RadioButton();
            MRAMRadioButton = new RadioButton();
            IsYAZ0CheckBox = new CheckBox();
            IsCompressedCheckBox = new CheckBox();
            AutoButton = new Button();
            ((System.ComponentModel.ISupportInitialize)FileIDColorNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            FileSettingsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Dock = DockStyle.Left;
            NameLabel.Location = new Point(3, 3);
            NameLabel.Name = "NameLabel";
            NameLabel.Padding = new Padding(0, 3, 0, 3);
            NameLabel.Size = new Size(35, 19);
            NameLabel.TabIndex = 0;
            NameLabel.Text = "Name";
            NameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FileIDLabel
            // 
            FileIDLabel.AutoSize = true;
            FileIDLabel.Dock = DockStyle.Left;
            FileIDLabel.Location = new Point(3, 3);
            FileIDLabel.Name = "FileIDLabel";
            FileIDLabel.Padding = new Padding(0, 3, 0, 3);
            FileIDLabel.Size = new Size(37, 19);
            FileIDLabel.TabIndex = 1;
            FileIDLabel.Text = "File ID";
            FileIDLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NameColorTextBox
            // 
            NameColorTextBox.Dock = DockStyle.Fill;
            NameColorTextBox.Location = new Point(38, 3);
            NameColorTextBox.Name = "NameColorTextBox";
            NameColorTextBox.Size = new Size(172, 20);
            NameColorTextBox.TabIndex = 2;
            // 
            // FileIDColorNumericUpDown
            // 
            FileIDColorNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            FileIDColorNumericUpDown.Dock = DockStyle.Fill;
            FileIDColorNumericUpDown.Location = new Point(40, 3);
            FileIDColorNumericUpDown.Maximum = new decimal(new int[] { 65534, 0, 0, 0 });
            FileIDColorNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            FileIDColorNumericUpDown.Name = "FileIDColorNumericUpDown";
            FileIDColorNumericUpDown.Size = new Size(36, 20);
            FileIDColorNumericUpDown.TabIndex = 3;
            FileIDColorNumericUpDown.TextValue = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
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
            splitContainer1.Panel2.Controls.Add(OKButton);
            splitContainer1.Panel2.Controls.Add(HashLabel);
            splitContainer1.Panel2.Controls.Add(FileSettingsGroupBox);
            splitContainer1.Size = new Size(294, 205);
            splitContainer1.SplitterDistance = 25;
            splitContainer1.TabIndex = 4;
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
            splitContainer2.Panel1.Controls.Add(NameColorTextBox);
            splitContainer2.Panel1.Controls.Add(NameLabel);
            splitContainer2.Panel1.Padding = new Padding(3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(FileIDColorNumericUpDown);
            splitContainer2.Panel2.Controls.Add(FileIDLabel);
            splitContainer2.Panel2.Padding = new Padding(3);
            splitContainer2.Size = new Size(294, 25);
            splitContainer2.SplitterDistance = 213;
            splitContainer2.SplitterWidth = 2;
            splitContainer2.TabIndex = 0;
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.FlatStyle = FlatStyle.Flat;
            OKButton.Location = new Point(207, 148);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 2;
            OKButton.Text = "Save";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // HashLabel
            // 
            HashLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            HashLabel.AutoSize = true;
            HashLabel.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HashLabel.Location = new Point(12, 152);
            HashLabel.Name = "HashLabel";
            HashLabel.Size = new Size(63, 15);
            HashLabel.TabIndex = 1;
            HashLabel.Text = "CRC: {0}";
            // 
            // FileSettingsGroupBox
            // 
            FileSettingsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FileSettingsGroupBox.Controls.Add(DVDRadioButton);
            FileSettingsGroupBox.Controls.Add(ARAMRadioButton);
            FileSettingsGroupBox.Controls.Add(MRAMRadioButton);
            FileSettingsGroupBox.Controls.Add(IsYAZ0CheckBox);
            FileSettingsGroupBox.Controls.Add(IsCompressedCheckBox);
            FileSettingsGroupBox.Controls.Add(AutoButton);
            FileSettingsGroupBox.Location = new Point(12, 3);
            FileSettingsGroupBox.Name = "FileSettingsGroupBox";
            FileSettingsGroupBox.Size = new Size(270, 139);
            FileSettingsGroupBox.TabIndex = 0;
            FileSettingsGroupBox.TabStop = false;
            FileSettingsGroupBox.Text = "File Settings";
            // 
            // DVDRadioButton
            // 
            DVDRadioButton.AutoSize = true;
            DVDRadioButton.Location = new Point(6, 111);
            DVDRadioButton.Name = "DVDRadioButton";
            DVDRadioButton.Size = new Size(92, 17);
            DVDRadioButton.TabIndex = 8;
            DVDRadioButton.TabStop = true;
            DVDRadioButton.Text = "Read off DVD";
            DVDRadioButton.UseVisualStyleBackColor = true;
            // 
            // ARAMRadioButton
            // 
            ARAMRadioButton.AutoSize = true;
            ARAMRadioButton.Location = new Point(6, 88);
            ARAMRadioButton.Name = "ARAMRadioButton";
            ARAMRadioButton.Size = new Size(232, 17);
            ARAMRadioButton.TabIndex = 7;
            ARAMRadioButton.TabStop = true;
            ARAMRadioButton.Text = "Load File to Auxilary RAM (GameCube Only)";
            ARAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // MRAMRadioButton
            // 
            MRAMRadioButton.AutoSize = true;
            MRAMRadioButton.Location = new Point(6, 65);
            MRAMRadioButton.Name = "MRAMRadioButton";
            MRAMRadioButton.Size = new Size(133, 17);
            MRAMRadioButton.TabIndex = 6;
            MRAMRadioButton.TabStop = true;
            MRAMRadioButton.Text = "Load File to Main RAM";
            MRAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // IsYAZ0CheckBox
            // 
            IsYAZ0CheckBox.AutoSize = true;
            IsYAZ0CheckBox.Location = new Point(6, 42);
            IsYAZ0CheckBox.Name = "IsYAZ0CheckBox";
            IsYAZ0CheckBox.Size = new Size(125, 17);
            IsYAZ0CheckBox.TabIndex = 5;
            IsYAZ0CheckBox.Text = "Is Compressed YAZ0";
            IsYAZ0CheckBox.UseVisualStyleBackColor = true;
            // 
            // IsCompressedCheckBox
            // 
            IsCompressedCheckBox.AutoSize = true;
            IsCompressedCheckBox.Location = new Point(6, 19);
            IsCompressedCheckBox.Name = "IsCompressedCheckBox";
            IsCompressedCheckBox.Size = new Size(95, 17);
            IsCompressedCheckBox.TabIndex = 4;
            IsCompressedCheckBox.Text = "Is Compressed";
            IsCompressedCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoButton
            // 
            AutoButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AutoButton.FlatStyle = FlatStyle.Flat;
            AutoButton.Location = new Point(215, 0);
            AutoButton.Name = "AutoButton";
            AutoButton.Size = new Size(51, 23);
            AutoButton.TabIndex = 3;
            AutoButton.Text = "Auto";
            AutoButton.UseVisualStyleBackColor = true;
            AutoButton.Click += AutoButton_Click;
            // 
            // FilePropertyRARCForm
            // 
            AcceptButton = OKButton;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 205);
            Controls.Add(splitContainer1);
            Font = new Font("Microsoft Sans Serif", 8.25F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FilePropertyRARCForm";
            Text = "WiiExplorer - File Properties";
            FormClosing += FilePropertyRARCForm_FormClosing;
            Load += FilePropertyRARCForm_Load;
            ((System.ComponentModel.ISupportInitialize)FileIDColorNumericUpDown).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            FileSettingsGroupBox.ResumeLayout(false);
            FileSettingsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label NameLabel;
        private Label FileIDLabel;
        private DarkModeForms.ColorTextBox NameColorTextBox;
        private DarkModeForms.ColorNumericUpDown FileIDColorNumericUpDown;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Button OKButton;
        private Label HashLabel;
        private GroupBox FileSettingsGroupBox;
        private Button AutoButton;
        private RadioButton DVDRadioButton;
        private RadioButton ARAMRadioButton;
        private RadioButton MRAMRadioButton;
        private CheckBox IsYAZ0CheckBox;
        private CheckBox IsCompressedCheckBox;
    }
}