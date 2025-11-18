using DarkModeForms;
using Hack.io.RARC;

namespace WiiExplorer;

public partial class FilePropertyRARCForm : Form
{
    private readonly RARC.File CurrentFile;
    private readonly RARC Archive;
    private readonly TreeView ArchiveTreeView;

    public FilePropertyRARCForm(TreeView treeview, RARC archive)
    {
        InitializeComponent();
        CenterToParent();

        ProgramColors.ReloadTheme(this);

        MRAMRadioButton.Tag = RARC.FileAttribute.PRELOAD_TO_MRAM;
        ARAMRadioButton.Tag = RARC.FileAttribute.PRELOAD_TO_ARAM;
        DVDRadioButton.Tag = RARC.FileAttribute.LOAD_FROM_DVD;


        ArchiveTreeView = treeview;
        Archive = archive;
        CurrentFile = Archive[ArchiveTreeView.SelectedNode.FullPath] as RARC.File ?? throw new InvalidOperationException();
        FileIDColorNumericUpDown.Enabled = !Archive.KeepFileIDsSynced;

        HashLabel.Text = $"CRC: {HashUtil.CalcCRC32(CurrentFile.FileData):X8}";

        DialogResult = DialogResult.Cancel;
    }

    private void FilePropertyRARCForm_Load(object sender, EventArgs e)
    {
        NameColorTextBox.Text = CurrentFile.Name;
        FileIDColorNumericUpDown.Value = CurrentFile.ID;
        IsCompressedCheckBox.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.COMPRESSED);
        IsYAZ0CheckBox.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.YAZ0_COMPRESSED);
        MRAMRadioButton.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.PRELOAD_TO_MRAM);
        ARAMRadioButton.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.PRELOAD_TO_ARAM);
        DVDRadioButton.Checked = CurrentFile.FileSettings.HasFlag(RARC.FileAttribute.LOAD_FROM_DVD);
    }

    private void FilePropertyRARCForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK)
            return; // don't bother doing anything else

        string prevname = ArchiveTreeView.SelectedNode.Text;
        string prevpath = ArchiveTreeView.SelectedNode.FullPath;
        ArchiveTreeView.SelectedNode.Text = NameColorTextBox.Text;
        if (Archive.ItemExists(ArchiveTreeView.SelectedNode.FullPath) && (RARC.File?)Archive[prevpath] != (RARC.File?)Archive[ArchiveTreeView.SelectedNode.FullPath])
        {
            string existingpath = ArchiveTreeView.SelectedNode.FullPath;
            ArchiveTreeView.SelectedNode.Text = prevname;
            MessageBox.Show(string.Format(Properties.Resources.MessageBoxMsg_ItemWithTheSameNameError, NameColorTextBox.Text, existingpath), Properties.Resources.MessageBoxTitle_ItemNameConflict, MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
            return;
        }
        Archive.MoveItem(prevpath, ArchiveTreeView.SelectedNode.FullPath);
        CurrentFile.Name = NameColorTextBox.Text;
        CurrentFile.ID = (short)FileIDColorNumericUpDown.Value;
        if (FileSettingsGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) is RadioButton rb && rb.Tag is RARC.FileAttribute attr)
            CurrentFile.FileSettings = RARC.FileAttribute.FILE | (IsCompressedCheckBox.Checked ? RARC.FileAttribute.COMPRESSED : 0) | attr | (IsYAZ0CheckBox.Checked ? RARC.FileAttribute.YAZ0_COMPRESSED : 0);

        int imgidx = ArcUtil.IndexOfExtensionImageOrDefault(NameColorTextBox.Text, ArchiveTreeView.ImageList);
        ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = imgidx;
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void AutoButton_Click(object sender, EventArgs e)
    {
        if (CurrentFile is null || CurrentFile.FileData is null)
            return;

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
        if (!Archive.KeepFileIDsSynced && FileIDColorNumericUpDown.Value == -1)
            FileIDColorNumericUpDown.Value = Archive.NextFreeFileID;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Escape:
                Close();
                return true;
            default:
                return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
