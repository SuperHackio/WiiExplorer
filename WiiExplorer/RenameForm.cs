using Hack.io.RARC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiExplorer
{ 
    public partial class RenameForm : Form
    {
        public RenameForm(object Item)
        {
            InitializeComponent();
            CenterToParent();
            MaximumSize = new Size(Screen.GetBounds(Location).Width, Height);
            string OGName = ((dynamic)Item).Name;
            Text = Text.Replace("[]", $"\"{OGName}\"");
            if (!(Item is RARC.Directory))
            {
                FileInfo fi = new FileInfo(OGName);
                string[] splits = fi.Name.Split('.');
                NameTextBox.Text = fi.Name.Replace("."+splits[splits.Length - 1], "");
                ExtensionTextBox.Text = fi.Extension;
            }
            else
            {
                ExtensionTextBox.Enabled = false;
                ExtensionLabel.Enabled = false;
                NameTextBox.Text = OGName;
            }
        }
        bool complete = false;

        private void ZoneForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                complete = true;
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                complete = false;
                Close();
            }
        }

        private void RenameForm_FormClosing(object sender, FormClosingEventArgs e) => DialogResult = complete ? DialogResult.OK : DialogResult.Cancel;
    }
}
