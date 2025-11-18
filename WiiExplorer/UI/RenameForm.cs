using DarkModeForms;
using Hack.io.Class;
using Hack.io.RARC;

namespace WiiExplorer;

public partial class RenameForm : Form
{
    readonly dynamic CurrentItem;
    readonly Archive Archive;
    readonly TreeView ArchiveTreeView;

    public RenameForm(TreeView treeview, Archive archive)
    {
        InitializeComponent();
        CenterToParent();

        MaximumSize = new Size(Screen.GetBounds(Location).Width, Height);
        MinimumSize = Size;
        Archive = archive;

        ArchiveTreeView = treeview;
        CurrentItem = Archive[treeview.SelectedNode.FullPath] ?? throw new Exception("Should be impossible to hit this...");
        string OGName = CurrentItem.Name;
        //Text = string.Format(Strings.RenameWindowTitle, OGName);
        if (CurrentItem is not RARC.Directory)
        {
            FileInfo fi = new(OGName);
            NameColorTextBox.Text = Path.GetFileNameWithoutExtension(OGName);
            ExtensionColorTextBox.Text = fi.Extension;
        }
        else
        {
            ExtensionColorTextBox.Enabled = false;
            ExtensionLabel.Enabled = false;
            NameColorTextBox.Text = OGName;
        }

        DialogResult = DialogResult.Cancel;
        ProgramColors.ReloadTheme(this);
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

    private void SubmitButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void RenameForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK)
            return; // don't bother doing anything else

        string prevname = ArchiveTreeView.SelectedNode.Text;
        string prevpath = ArchiveTreeView.SelectedNode.FullPath;
        string ext = ExtensionColorTextBox.Text;
        if (ExtensionColorTextBox.Enabled && ext.Length > 0 && !ext.StartsWith('.'))
            ext = '.' + ext;
        string finalname = NameColorTextBox.Text + (CurrentItem is ArchiveFile ? ext : "");
        ArchiveTreeView.SelectedNode.Text = finalname;
        if (Archive.ItemExists(ArchiveTreeView.SelectedNode.FullPath) && Archive[prevpath] != Archive[ArchiveTreeView.SelectedNode.FullPath])
        {
            string existingpath = ArchiveTreeView.SelectedNode.FullPath;
            ArchiveTreeView.SelectedNode.Text = prevname;
            MessageBox.Show(string.Format(Properties.Resources.MessageBoxMsg_ItemWithTheSameNameError, finalname, existingpath), Properties.Resources.MessageBoxTitle_ItemNameConflict, MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
            return;
        }
        Archive.MoveItem(prevpath, ArchiveTreeView.SelectedNode.FullPath);
        CurrentItem.Name = finalname;

        if (ExtensionColorTextBox.Enabled)
        {
            int imgidx = ArcUtil.IndexOfExtensionImageOrDefault(finalname, ArchiveTreeView.ImageList);
            ArchiveTreeView.SelectedNode.ImageIndex = ArchiveTreeView.SelectedNode.SelectedImageIndex = imgidx;
        }
    }

    private void RenameForm_Shown(object sender, EventArgs e)
    {
        NameColorTextBox.Focus();
    }
}
