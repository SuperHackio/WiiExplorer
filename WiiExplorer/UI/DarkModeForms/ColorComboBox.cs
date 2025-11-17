using System.ComponentModel;
using System.Diagnostics;

namespace DarkModeForms;

public class ColorComboBox : ComboBox
{
    private const int WM_PAINT = 0xF;
    private readonly int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;

    [DebuggerStepThrough]
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == WM_PAINT)
        {
            using var g = Graphics.FromHwnd(Handle);
            // Uncomment this if you don't want the "highlight border".
            //using (Pen p = new(BorderColor, 1))
            //{
            //    g.DrawRectangle(p, 0, 0, Width - buttonWidth - 1, Height - 1);
            //}
            using Pen p = new(BorderColor, 1);
            g.DrawRectangle(p, 0, 0, Width - buttonWidth - 1, Height - 1);
            if (!Enabled)
            {
                using Pen p2 = new(BorderColor, 2);
                g.DrawRectangle(p2, 2, 2, Width - buttonWidth - 4, Height - 4);
            }
        }
    }

    public ColorComboBox()
    {
        BorderColor = Color.FromArgb(200, 200, 200);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        Resize += (sender, e) => { Refresh(); };
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(typeof(Color), "Gray")]
    public Color BorderColor { get; set; }
}
