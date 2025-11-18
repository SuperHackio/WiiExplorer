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
            resources.ApplyResources(NameColorTextBox, "NameColorTextBox");
            NameColorTextBox.Name = "NameColorTextBox";
            // 
            // ExtensionColorTextBox
            // 
            resources.ApplyResources(ExtensionColorTextBox, "ExtensionColorTextBox");
            ExtensionColorTextBox.Name = "ExtensionColorTextBox";
            // 
            // SubmitButton
            // 
            resources.ApplyResources(SubmitButton, "SubmitButton");
            SubmitButton.Name = "SubmitButton";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(SubmitButton);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(NameLabel);
            splitContainer2.Panel1.Controls.Add(NameColorTextBox);
            resources.ApplyResources(splitContainer2.Panel1, "splitContainer2.Panel1");
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(ExtensionLabel);
            splitContainer2.Panel2.Controls.Add(ExtensionColorTextBox);
            resources.ApplyResources(splitContainer2.Panel2, "splitContainer2.Panel2");
            // 
            // NameLabel
            // 
            resources.ApplyResources(NameLabel, "NameLabel");
            NameLabel.Name = "NameLabel";
            // 
            // ExtensionLabel
            // 
            resources.ApplyResources(ExtensionLabel, "ExtensionLabel");
            ExtensionLabel.Name = "ExtensionLabel";
            // 
            // RenameForm
            // 
            AcceptButton = SubmitButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RenameForm";
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