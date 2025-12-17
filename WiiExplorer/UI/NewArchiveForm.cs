using DarkModeForms;
using Hack.io.Class;
using Hack.io.RARC;
using Hack.io.U8;
using System.Reflection;
using WiiExplorer.Class;
using WiiExplorer.Properties;

namespace WiiExplorer;

public partial class NewArchiveForm : Form
{
    private readonly static Dictionary<string, (Type t, string d, bool s)> ArchiveFormats = new()
    {
        { Resources.ArchiveFormat_JSystemRARC_Name, (typeof(RARC), Resources.ArchiveFormat_JSystemRARC_Description, false) },
        { Resources.ArchiveFormat_JSystemBAA_Name, (typeof(JSystemBAA), Resources.ArchiveFormat_JSystemBAA_Description, true) },

        { Resources.ArchiveFormat_NW4RArchive_Name, (typeof(U8), Resources.ArchiveFormat_NW4RArchive_Description, false) },
    };

    public NewArchiveForm()
    {
        InitializeComponent();
        CenterToParent();

        this.SetDoubleBuffered();
        FormatListView.SetDoubleBuffered();

        ProgramColors.ReloadTheme(this);

        bool IsShiftDown = false;

#if WINDOWS
        Microsoft.VisualBasic.Devices.Keyboard key = new(); // Windows Only, apparently
        IsShiftDown = key.ShiftKeyDown;
#endif

        foreach (string item in ArchiveFormats.Keys)
        {
            if (ArchiveFormats[item].s && !IsShiftDown)
                continue;

            ListViewItem LVI = new(item);
            FormatListView.Items.Add(LVI);
        }
        FormatListView.Items[0].Selected = true;
        DialogResult = DialogResult.Cancel;
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

    private void FormatListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FormatListView.SelectedItems.Count == 0)
        {
            SelectButton.Enabled = false;
            DescriptionTextLabel.Text = Resources.String_NewArchiveGuide;
            return;
        }

        SelectButton.Enabled = true;
        string key = FormatListView.SelectedItems[0].Text;
        DescriptionTextLabel.Text = ArchiveFormats[key].d;
    }

    private void SelectButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void FormatListView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        ListViewHitTestInfo info = FormatListView.HitTest(e.X, e.Y);
        ListViewItem? item = info.Item;

        if (item is not null)
            SelectButton_Click(sender, EventArgs.Empty);
    }

    public Archive? GetNewArchive()
    {
        if (FormatListView.SelectedItems.Count == 0)
            return null; // Nothing selected...
        string key = FormatListView.SelectedItems[0].Text;
        Type t = ArchiveFormats[key].t;
        ConstructorInfo? ctor = t.GetConstructor(Type.EmptyTypes);
        if (ctor is null)
            return null; // Can't create anything...
        Archive? arc = (Archive?)ctor.Invoke([]);
        if (arc is null)
            return null;
        arc[Resources.String_NewArchiveRootName] = null;
        return arc;
    }
}
