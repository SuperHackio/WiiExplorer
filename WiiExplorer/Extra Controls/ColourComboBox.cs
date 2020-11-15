using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WiiExplorer
{
    public class ColourComboBox : ComboBox
    {
        private const int WM_PAINT = 0xF;
        private readonly int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;

        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (var g = Graphics.FromHwnd(Handle))
                {
                    // Uncomment this if you don't want the "highlight border".
                    //using (var p = new Pen(this.BorderColor, 1))
                    //{
                    //    g.DrawRectangle(p, 0, 0, Width - buttonWidth - 1, Height - 1);
                    //}
                    using (var p = new Pen(this.BorderColor, 1))
                    {
                        g.DrawRectangle(p, 0, 0, Width - buttonWidth - 1, Height - 1);
                    }
                    if (!Enabled)
                    {
                        using (var p = new Pen(this.BorderColor, 2))
                        {
                            g.DrawRectangle(p, 2, 2, Width - buttonWidth - 4, Height - 4);
                        }
                    }
                }
            }
        }

        public ColourComboBox()
        {
            BorderColor = Color.FromArgb(200, 200, 200);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Resize += (object sender, EventArgs e) => { Refresh(); };
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor { get; set; }
    }
}
