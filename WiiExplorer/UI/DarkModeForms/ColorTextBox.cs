using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DarkModeForms;

public partial class ColorTextBox : TextBox
{
    private const int WM_PAINT = 0xF;
    //const int WM_NCPAINT = 0x85;
    const uint RDW_INVALIDATE = 0x1;
    const uint RDW_IUPDATENOW = 0x100;
    const uint RDW_FRAME = 0x400;
    [LibraryImport("user32.dll")]
    private static partial IntPtr GetWindowDC(IntPtr hWnd);
    [LibraryImport("user32.dll")]
    private static partial int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    protected static partial bool RedrawWindow(IntPtr hWnd, IntPtr lprc, IntPtr hrgn, uint flags);

    Color borderColor = Color.Gray;
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(typeof(Color), "Gray")]
    public Color BorderColor
    {
        get { return borderColor; }
        set
        {
            borderColor = value;
            RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
        }
    }
    public ColorTextBox() => SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

    [DebuggerStepThrough]
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == WM_PAINT && BorderColor != Color.Transparent)
        {
            var hdc = GetWindowDC(Handle);
            using (Graphics g = Graphics.FromHdcInternal(hdc))
            using (Pen p = new(BorderColor, 1))
                g.DrawRectangle(p, new Rectangle(1, 1, Width - 3, Height - 3));
            _ = ReleaseDC(Handle, hdc);
        }
    }
    protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);
}