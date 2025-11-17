using DarkModeForms;
using Hack.io.Class;
using Hack.io.RARC;
using Hack.io.U8;
using Microsoft.VisualBasic.Devices;
using System.Reflection;
using WiiExplorer.Class;

namespace WiiExplorer;

public partial class NewArchiveForm : Form
{
    private readonly static Dictionary<string, (Type t, string d, bool s)> ArchiveFormats = new()
    {
        { "JSystem Archive", (typeof(RARC), "The archive format used in JSystem\r\n\r\nSupported games include:\r\n- Pikmin\r\n- Luigi's Mansion\r\n- Super Mario Sunshine\r\n- The Legend of Zelda:\r\n    The Wind Waker\r\n- Mario Kart: Double Dash!!\r\n- Donkey Kong Jungle Beat\r\n- The Legend of Zelda:\r\n    Twilight Princess\r\n- Super Mario Galaxy\r\n- Super Mario Galaxy 2", false) },
        { "JSystem AAF", (typeof(AAF), "A JSystem audio archive used for storing a select few files\r\n\r\nSupported games include:\r\n- Super Mario Galaxy\r\n- Super Mario Galaxy 2", true) },

        { "NW4R Archive", (typeof(U8), "The archive format used in NW4R\r\n\r\nSupported games include:\r\n- Super Paper Mario\r\n- Mario Kart Wii\r\n- Metroid: Other M\r\n- Sonic Colors [Wii]\r\n- Mario and Sonic at the\r\n    London 2012 Olympic Games\r\n- The Legend of Zelda:\r\n    Skyward Sword\r\n- Rodea the Sky Soldier", false) },
    };

    public NewArchiveForm()
    {
        InitializeComponent();
        CenterToParent();

        this.SetDoubleBuffered();
        FormatListView.SetDoubleBuffered();

        ProgramColors.ReloadTheme(this);

        Keyboard key = new(); // Does this work on Mac/Linux??
        bool IsShiftDown = key.ShiftKeyDown;
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
                break;
            default:
                return base.ProcessCmdKey(ref msg, keyData);
        }

        return true;
    }

    private void FormatListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FormatListView.SelectedItems.Count == 0)
        {
            SelectButton.Enabled = false;
            DescriptionTextLabel.Text = "Select an Archive Format on the left.";
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
        arc["NewArchive"] = null;
        return arc;
    }
}
