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
        public FilePropertyForm(RARC.File currentfile, bool AllowCustomID)
        {
            InitializeComponent();
            CancelButton = DiscardButton;
            AcceptButton = OKButton;
            MRAMRadioButton.Tag = RARC.FileAttribute.PRELOAD_TO_MRAM;
            ARAMRadioButton.Tag = RARC.FileAttribute.PRELOAD_TO_ARAM;
            DVDRadioButton.Tag = RARC.FileAttribute.LOAD_FROM_DVD;

            CurrentFile = currentfile;
            IDNumericUpDown.Enabled = AllowCustomID;
            CenterToParent();


            AutoDetectSettingsButton.BackColor = BackColor = NameTextBox.BackColor = IDNumericUpDown.BackColor = Program.ProgramColours.ControlBackColor;
            AutoDetectSettingsButton.ForeColor = ForeColor = NameTextBox.ForeColor = IDNumericUpDown.ForeColor = Program.ProgramColours.TextColour;
            NameTextBox.BorderColor = IDNumericUpDown.BorderColor = Program.ProgramColours.BorderColour;
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].BackColor = Program.ProgramColours.ControlBackColor;
                Controls[i].ForeColor = Program.ProgramColours.TextColour;
            }
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
                CurrentFile.Name = NameTextBox.Text;
                CurrentFile.ID = (short)IDNumericUpDown.Value;
                CurrentFile.FileSettings = RARC.FileAttribute.FILE | (IsCompressedCheckBox.Checked ? RARC.FileAttribute.COMPRESSED : 0) | (RARC.FileAttribute)FileSettingsGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag | (IsYAZ0CheckBox.Checked ? RARC.FileAttribute.YAZ0_COMPRESSED : 0);
            }
        }
    }
}
