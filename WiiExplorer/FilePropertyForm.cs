using Hack.io.RARC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiExplorer
{
    public partial class FilePropertyForm : Form
    {
        public FilePropertyForm(TreeView treeview, RARC archive)
        {
            InitializeComponent();
            CancelButton = DiscardButton;
            AcceptButton = OKButton;
            MRAMRadioButton.Tag = RARC.FileAttribute.PRELOAD_TO_MRAM;
            ARAMRadioButton.Tag = RARC.FileAttribute.PRELOAD_TO_ARAM;
            DVDRadioButton.Tag = RARC.FileAttribute.LOAD_FROM_DVD;

            ArchiveTreeView = treeview;
            Archive = archive;
            CurrentFile = Archive[ArchiveTreeView.SelectedNode.FullPath] as RARC.File;
            IDNumericUpDown.Enabled = !Archive.KeepFileIDsSynced;
            CenterToParent();

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].BackColor = Program.ProgramColours.ControlBackColor;
                Controls[i].ForeColor = Program.ProgramColours.TextColour;
            }
            AutoDetectSettingsButton.BackColor = BackColor = Program.ProgramColours.ControlBackColor;
            AutoDetectSettingsButton.ForeColor = ForeColor = NameTextBox.ForeColor = IDNumericUpDown.ForeColor = Program.ProgramColours.TextColour;
            NameTextBox.BackColor = IDNumericUpDown.BackColor = Program.ProgramColours.WindowColour;
            NameTextBox.BorderColor = IDNumericUpDown.BorderColor = Program.ProgramColours.BorderColour;
            //FileSettingsGroupBox.BackColor = Program.ProgramColours.WindowColour;
        }

        private void FilePropertyForm_Load(object sender, EventArgs e)
        {
            NameTextBox.Text = CurrentFile.Name;
            IDNumericUpDown.Value = CurrentFile.ID;
            IsCompressedCheckBox.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.COMPRESSED);
            IsYAZ0CheckBox.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.YAZ0_COMPRESSED);
            MRAMRadioButton.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.PRELOAD_TO_MRAM);
            ARAMRadioButton.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.PRELOAD_TO_ARAM);
            DVDRadioButton.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.LOAD_FROM_DVD);
        }

        RARC.File CurrentFile;
        RARC Archive;
        TreeView ArchiveTreeView;

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AutoDetectSettingsButton_Click(object sender, EventArgs e)
        {
            if (CurrentFile.FileData[0] == 0x59 && CurrentFile.FileData[1] == 0x61 && CurrentFile.FileData[2] == 0x7A && CurrentFile.FileData[3] == 0x30)
            {
                IsCompressedCheckBox.Checked = true;
                IsYAZ0CheckBox.Checked = true;
            }
            if (Name.Contains(".rel"))
            {
                ARAMRadioButton.Checked = true;
            }
            else
            {
                MRAMRadioButton.Checked = true;
            }
        }

        private void FilePropertyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                string prevname = ArchiveTreeView.SelectedNode.Text;
                string prevpath = ArchiveTreeView.SelectedNode.FullPath;
                ArchiveTreeView.SelectedNode.Text = NameTextBox.Text;
                if (Archive.ItemExists(ArchiveTreeView.SelectedNode.FullPath) && (RARC.File)Archive[prevpath] != (RARC.File)Archive[ArchiveTreeView.SelectedNode.FullPath])
                {
                    ArchiveTreeView.SelectedNode.Text = prevname;
                    MessageBox.Show("There is already an item with this name in this directory", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
                Archive.MoveItem(prevpath, ArchiveTreeView.SelectedNode.FullPath);
                CurrentFile.Name = NameTextBox.Text;
                CurrentFile.ID = (short)IDNumericUpDown.Value;
                CurrentFile.FileSettings = RARC.FileAttribute.FILE | (IsCompressedCheckBox.Checked ? RARC.FileAttribute.COMPRESSED : 0) | (RARC.FileAttribute)FileSettingsGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag | (IsYAZ0CheckBox.Checked ? RARC.FileAttribute.YAZ0_COMPRESSED : 0);
            }
        }
    }
}
