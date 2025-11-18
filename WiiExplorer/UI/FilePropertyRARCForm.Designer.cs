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
            splitContainer2 = new SplitContainer();
            NameColorTextBox = new DarkModeForms.ColorTextBox();
            NameLabel = new Label();
            FileIDColorNumericUpDown = new DarkModeForms.ColorNumericUpDown();
            FileIDLabel = new Label();
            splitContainer1 = new SplitContainer();
            OKButton = new Button();
            HashLabel = new Label();
            FileSettingsGroupBox = new GroupBox();
            DVDRadioButton = new RadioButton();
            ARAMRadioButton = new RadioButton();
            MRAMRadioButton = new RadioButton();
            IsYAZ0CheckBox = new CheckBox();
            IsCompressedCheckBox = new CheckBox();
            AutoButton = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FileIDColorNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            FileSettingsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(NameColorTextBox);
            splitContainer2.Panel1.Controls.Add(NameLabel);
            resources.ApplyResources(splitContainer2.Panel1, "splitContainer2.Panel1");
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(FileIDColorNumericUpDown);
            splitContainer2.Panel2.Controls.Add(FileIDLabel);
            resources.ApplyResources(splitContainer2.Panel2, "splitContainer2.Panel2");
            // 
            // NameColorTextBox
            // 
            resources.ApplyResources(NameColorTextBox, "NameColorTextBox");
            NameColorTextBox.Name = "NameColorTextBox";
            // 
            // NameLabel
            // 
            resources.ApplyResources(NameLabel, "NameLabel");
            NameLabel.Name = "NameLabel";
            // 
            // FileIDColorNumericUpDown
            // 
            FileIDColorNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            resources.ApplyResources(FileIDColorNumericUpDown, "FileIDColorNumericUpDown");
            FileIDColorNumericUpDown.Maximum = new decimal(new int[] { 65534, 0, 0, 0 });
            FileIDColorNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            FileIDColorNumericUpDown.Name = "FileIDColorNumericUpDown";
            FileIDColorNumericUpDown.TextValue = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // FileIDLabel
            // 
            resources.ApplyResources(FileIDLabel, "FileIDLabel");
            FileIDLabel.Name = "FileIDLabel";
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Name = "splitContainer1";
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
            // 
            // OKButton
            // 
            resources.ApplyResources(OKButton, "OKButton");
            OKButton.Name = "OKButton";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // HashLabel
            // 
            resources.ApplyResources(HashLabel, "HashLabel");
            HashLabel.Name = "HashLabel";
            // 
            // FileSettingsGroupBox
            // 
            resources.ApplyResources(FileSettingsGroupBox, "FileSettingsGroupBox");
            FileSettingsGroupBox.Controls.Add(DVDRadioButton);
            FileSettingsGroupBox.Controls.Add(ARAMRadioButton);
            FileSettingsGroupBox.Controls.Add(MRAMRadioButton);
            FileSettingsGroupBox.Controls.Add(IsYAZ0CheckBox);
            FileSettingsGroupBox.Controls.Add(IsCompressedCheckBox);
            FileSettingsGroupBox.Controls.Add(AutoButton);
            FileSettingsGroupBox.Name = "FileSettingsGroupBox";
            FileSettingsGroupBox.TabStop = false;
            // 
            // DVDRadioButton
            // 
            resources.ApplyResources(DVDRadioButton, "DVDRadioButton");
            DVDRadioButton.Name = "DVDRadioButton";
            DVDRadioButton.TabStop = true;
            DVDRadioButton.UseVisualStyleBackColor = true;
            // 
            // ARAMRadioButton
            // 
            resources.ApplyResources(ARAMRadioButton, "ARAMRadioButton");
            ARAMRadioButton.Name = "ARAMRadioButton";
            ARAMRadioButton.TabStop = true;
            ARAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // MRAMRadioButton
            // 
            resources.ApplyResources(MRAMRadioButton, "MRAMRadioButton");
            MRAMRadioButton.Name = "MRAMRadioButton";
            MRAMRadioButton.TabStop = true;
            MRAMRadioButton.UseVisualStyleBackColor = true;
            // 
            // IsYAZ0CheckBox
            // 
            resources.ApplyResources(IsYAZ0CheckBox, "IsYAZ0CheckBox");
            IsYAZ0CheckBox.Name = "IsYAZ0CheckBox";
            IsYAZ0CheckBox.UseVisualStyleBackColor = true;
            // 
            // IsCompressedCheckBox
            // 
            resources.ApplyResources(IsCompressedCheckBox, "IsCompressedCheckBox");
            IsCompressedCheckBox.Name = "IsCompressedCheckBox";
            IsCompressedCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoButton
            // 
            resources.ApplyResources(AutoButton, "AutoButton");
            AutoButton.Name = "AutoButton";
            AutoButton.UseVisualStyleBackColor = true;
            AutoButton.Click += AutoButton_Click;
            // 
            // FilePropertyRARCForm
            // 
            AcceptButton = OKButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FilePropertyRARCForm";
            FormClosing += FilePropertyRARCForm_FormClosing;
            Load += FilePropertyRARCForm_Load;
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)FileIDColorNumericUpDown).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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