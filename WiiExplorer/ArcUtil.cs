using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Hack.io.RARC;

namespace WiiExplorer
{
    public static class ArcUtil
    {
        public static TreeNode[] ToTreeNode(this RARC Arc, int FolderClosedID, ImageList images)
        {
            List<TreeNode> Nodes = new List<TreeNode>();
            TreeNode Root = Arc.Root.ToTreeNode(FolderClosedID, images);
            for (int i = 0; i < Root.Nodes.Count; i++)
                Nodes.Add(Root.Nodes[i]);
            return Nodes.ToArray();
        }

        private static TreeNode ToTreeNode(this RARC.Directory Dir, int FolderClosedID, ImageList images)
        {
            TreeNode Final = new TreeNode(Dir.Name) { ImageIndex = FolderClosedID, SelectedImageIndex = FolderClosedID };
            foreach (KeyValuePair<string, object> item in Dir.Items)
            {
                if (item.Value is RARC.Directory dir)
                    Final.Nodes.Add(dir.ToTreeNode(FolderClosedID, images));
                else if (item.Value is RARC.File file)
                {
                    FileInfo fi = new FileInfo(file.Name);
                    int imageindex = 2;
                    if (images.Images.ContainsKey("*" + fi.Extension))
                        imageindex = images.Images.IndexOfKey("*" + fi.Extension);
                    Final.Nodes.Add(new TreeNode(file.Name) { Tag = file, ImageIndex = imageindex, SelectedImageIndex = imageindex });
                }
            }
            return Final;
        }

        public static void FromTreeNode(this RARC Arc, TreeNode Root) => Arc.Root = FromTreeNode(Root);

        public static RARC.Directory FromTreeNode(this TreeNode TN)
        {
            RARC.Directory Dir = new RARC.Directory() { Name = TN.Text };
            for (int i = 0; i < TN.Nodes.Count; i++)
                if (TN.Nodes[i].Tag == null)
                    Dir.Items.Add(TN.Nodes[i].Text, FromTreeNode(TN.Nodes[i]));
                else
                {
                    ((RARC.File)TN.Nodes[i].Tag).Name = TN.Nodes[i].Text;
                    Dir.Items.Add(TN.Nodes[i].Text, (RARC.File)TN.Nodes[i].Tag);
                }
            return Dir;
        }
    }
}
