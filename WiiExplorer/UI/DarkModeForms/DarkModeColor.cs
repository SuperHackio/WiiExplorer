namespace DarkModeForms;

internal interface IReloadTheme
{
    void ReloadTheme();
}

public static class ProgramColors
{
    public static bool IsDarkMode { get; set; }

    public static Color ControlBackColor => IsDarkMode ? Color.FromArgb(62, 62, 66) : Color.FromArgb(240, 240, 240);
    public static Color WindowColor => IsDarkMode ? Color.FromArgb(37, 37, 38) : Color.FromArgb(255, 255, 255);
    public static Color TextColor => IsDarkMode ? Color.FromArgb(241, 241, 241) : Color.FromArgb(0, 0, 0);
    public static Color BorderColor => IsDarkMode ? Color.FromArgb(50, 50, 50) : Color.Gray;

    public static void ReloadTheme(Control control)
    {
        if (control is not IReloadTheme)
        {
            if (control is ColorComboBox CCB)
            {
                CCB.ForeColor = TextColor;
                CCB.BackColor = WindowColor;
                CCB.BorderColor = BorderColor;
            }
            else if (control is ColorTextBox CTB)
            {
                control.ForeColor = TextColor;
                control.BackColor = WindowColor;
                CTB.BorderColor = BorderColor;
            }
            else if (control is ColorNumericUpDown CNUD)
            {
                control.ForeColor = TextColor;
                control.BackColor = WindowColor;
                CNUD.BorderColor = BorderColor;
            }
            else if (control is GroupBox or Button)
            {
                control.ForeColor = TextColor;
                control.BackColor = ControlBackColor;
            }
            else if (control is Label or CheckBox)
            {
                control.ForeColor = TextColor;
            }
            else if (control is TreeView Tv)
            {
                Tv.LineColor = control.ForeColor = TextColor;
                control.BackColor = WindowColor;
            }
            else if (control is ListBox)
            {
                control.ForeColor = TextColor;
                control.BackColor = WindowColor;
            }
            else if (control is ListView)
            {
                control.ForeColor = TextColor;
                control.BackColor = WindowColor;
            }
            else if (control is Form F)
            {
                control.ForeColor = TextColor;
                control.BackColor = ControlBackColor;
                if (F.MainMenuStrip is not null)
                    ReloadMenuStrip(F.MainMenuStrip);
            }
            else if (control is StatusStrip)
            {
                control.ForeColor = TextColor;
                control.BackColor = ControlBackColor;
            }
            else if (control is ContextMenuStrip CMS)
            {
                ReloadContextMenuStrip(CMS);
            }
        }

        for (int i = 0; i < control.Controls.Count; i++)
            ReloadTheme(control.Controls[i]);
    }

    private static void ReloadMenuStrip(MenuStrip strip)
    {
        strip.Renderer = IsDarkMode ? new MyRenderer() : default;
        ReloadMenuItems(strip.Items);
        strip.BackColor = ControlBackColor;
    }

    private static void ReloadContextMenuStrip(ContextMenuStrip strip)
    {
        strip.Renderer = IsDarkMode ? new MyRenderer() : default;
        ReloadMenuItems(strip.Items);
    }

    private static void ReloadMenuItems(ToolStripItemCollection items)
    {
        foreach(ToolStripItem x in items)
        {
            x.ForeColor = TextColor;
            x.BackColor = x.GetCurrentParent() is MenuStrip ? ControlBackColor : WindowColor;

            if (x is ToolStripMenuItem TSMI)
                ReloadMenuItems(TSMI.DropDownItems);
            if (x is ToolStripTextBox TSTB && !TSTB.ReadOnly)
                x.BackColor = WindowColor;
        }
    }
}

public class MyRenderer : ToolStripProfessionalRenderer
{
    public MyRenderer() : base(new MyColors()) { }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        if (e.Item is ToolStripMenuItem)
            e.ArrowColor = ProgramColors.TextColor;
        if (e.Item is ToolStripComboBox)
            e.ArrowColor = ProgramColors.TextColor;
        base.OnRenderArrow(e);
    }
}

internal class MyColors : ProfessionalColorTable
{
    public override Color ButtonSelectedHighlight => Color.Black;

    public override Color ButtonSelectedHighlightBorder => Color.Black;

    public override Color ButtonPressedHighlight => Color.Black;

    public override Color ButtonPressedHighlightBorder => Color.Black;

    public override Color ButtonCheckedHighlight => Color.Black;

    public override Color ButtonCheckedHighlightBorder => Color.Black;

    public override Color ButtonPressedBorder => Color.Black;

    public override Color ButtonSelectedBorder => Color.Black;

    public override Color ButtonCheckedGradientBegin => Color.Black;

    public override Color ButtonCheckedGradientMiddle => Color.Black;

    public override Color ButtonCheckedGradientEnd => Color.Black;

    public override Color ButtonSelectedGradientBegin => Color.Black;

    public override Color ButtonSelectedGradientMiddle => Color.Black;

    public override Color ButtonSelectedGradientEnd => Color.Black;

    public override Color ButtonPressedGradientBegin => Color.Black;

    public override Color ButtonPressedGradientMiddle => Color.Black;

    public override Color ButtonPressedGradientEnd => Color.Black;

    public override Color CheckBackground => Color.Black;

    public override Color CheckSelectedBackground => Color.Black;

    public override Color CheckPressedBackground => Color.Black;

    public override Color GripDark => Color.Black;

    public override Color GripLight => Color.Black;

    public override Color ImageMarginGradientBegin => Color.Black;

    public override Color ImageMarginGradientMiddle => Color.Black;

    public override Color ImageMarginGradientEnd => Color.Black;

    public override Color ImageMarginRevealedGradientBegin => Color.Black;

    public override Color ImageMarginRevealedGradientMiddle => Color.Black;

    public override Color ImageMarginRevealedGradientEnd => Color.Black;

    public override Color MenuStripGradientBegin => Color.Black;

    public override Color MenuStripGradientEnd => Color.Black;

    public override Color MenuItemSelected => Color.Black;

    public override Color MenuItemBorder => Color.Black;

    public override Color MenuBorder => Color.Black;

    public override Color MenuItemSelectedGradientBegin => Color.Black;

    public override Color MenuItemSelectedGradientEnd => Color.Black;

    public override Color MenuItemPressedGradientBegin => Color.Black;

    public override Color MenuItemPressedGradientMiddle => Color.White;

    public override Color MenuItemPressedGradientEnd => Color.Black;

    public override Color RaftingContainerGradientBegin => Color.Black;

    public override Color RaftingContainerGradientEnd => Color.Black;

    public override Color SeparatorDark => Color.Black;

    public override Color SeparatorLight => Color.Black;

    public override Color StatusStripGradientBegin => Color.Black;

    public override Color StatusStripGradientEnd => Color.Black;

    public override Color ToolStripBorder => Color.Black;

    public override Color ToolStripDropDownBackground => Color.Black;

    public override Color ToolStripGradientBegin => Color.Black;

    public override Color ToolStripGradientMiddle => Color.Black;

    public override Color ToolStripGradientEnd => Color.Black;

    public override Color ToolStripContentPanelGradientBegin => Color.Black;

    public override Color ToolStripContentPanelGradientEnd => Color.Black;

    public override Color ToolStripPanelGradientBegin => Color.Black;

    public override Color ToolStripPanelGradientEnd => Color.Black;

    public override Color OverflowButtonGradientBegin => Color.Black;

    public override Color OverflowButtonGradientMiddle => Color.Black;

    public override Color OverflowButtonGradientEnd => Color.Black;
}
