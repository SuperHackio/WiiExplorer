using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiExplorer
{
    public class ColourToolStripComboBox : ToolStripComboBox
    {
        public ColourToolStripComboBox()
        {
            base.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            base.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            base.ComboBox.DrawItem += (object sender, DrawItemEventArgs e) =>
            {
                e.DrawBackground();
                if (e.State == DrawItemState.Focus)
                    e.DrawFocusRectangle();
                var index = e.Index;
                if (index < 0 || index >= Items.Count) return;
                var item = Items[index];
                string text = (item == null) ? "(null)" : item.ToString();
                using (SolidBrush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
            };
        }
        
    }
}
