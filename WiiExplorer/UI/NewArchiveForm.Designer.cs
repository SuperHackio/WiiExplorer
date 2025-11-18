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
            resources.ApplyResources(SelectionSplitContainer, "SelectionSplitContainer");
            SelectionSplitContainer.Name = "SelectionSplitContainer";
            // 
            // SelectionSplitContainer.Panel1
            // 
            resources.ApplyResources(SelectionSplitContainer.Panel1, "SelectionSplitContainer.Panel1");
            SelectionSplitContainer.Panel1.Controls.Add(FormatListView);
            // 
            // SelectionSplitContainer.Panel2
            // 
            resources.ApplyResources(SelectionSplitContainer.Panel2, "SelectionSplitContainer.Panel2");
            SelectionSplitContainer.Panel2.Controls.Add(DescriptionTextLabel);
            SelectionSplitContainer.Panel2.Controls.Add(DescriptionHeaderLabel);
            // 
            // FormatListView
            // 
            resources.ApplyResources(FormatListView, "FormatListView");
            FormatListView.FullRowSelect = true;
            FormatListView.GridLines = true;
            FormatListView.MultiSelect = false;
            FormatListView.Name = "FormatListView";
            FormatListView.UseCompatibleStateImageBehavior = false;
            FormatListView.View = View.List;
            FormatListView.SelectedIndexChanged += FormatListView_SelectedIndexChanged;
            FormatListView.MouseDoubleClick += FormatListView_MouseDoubleClick;
            // 
            // DescriptionTextLabel
            // 
            resources.ApplyResources(DescriptionTextLabel, "DescriptionTextLabel");
            DescriptionTextLabel.Name = "DescriptionTextLabel";
            // 
            // DescriptionHeaderLabel
            // 
            resources.ApplyResources(DescriptionHeaderLabel, "DescriptionHeaderLabel");
            DescriptionHeaderLabel.Name = "DescriptionHeaderLabel";
            // 
            // SelectButton
            // 
            resources.ApplyResources(SelectButton, "SelectButton");
            SelectButton.Name = "SelectButton";
            SelectButton.UseVisualStyleBackColor = true;
            SelectButton.Click += SelectButton_Click;
            // 
            // NewArchiveForm
            // 
            AcceptButton = SelectButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(SelectionSplitContainer);
            Controls.Add(SelectButton);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewArchiveForm";
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