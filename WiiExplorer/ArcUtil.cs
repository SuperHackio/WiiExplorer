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

        private static TreeNode ToTreeNode(this RARCDirectory Dir, int FolderClosedID, ImageList images)
        {
            TreeNode Final = new TreeNode(Dir.Name) { ImageIndex = FolderClosedID, SelectedImageIndex = FolderClosedID };
            for (int i = 0; i < Dir.SubDirectories.Count; i++)
                Final.Nodes.Add(Dir.SubDirectories[i].ToTreeNode(FolderClosedID, images));
            for (int i = 0; i < Dir.Files.Count; i++)
            {
                FileInfo fi = new FileInfo(Dir.Files[i].Name);
                int imageindex = 2;
                if (images.Images.ContainsKey("*" + fi.Extension))
                    imageindex = images.Images.IndexOfKey("*" + fi.Extension);
                Final.Nodes.Add(new TreeNode(Dir.Files[i].Name) { Tag = Dir.Files[i], ImageIndex = imageindex, SelectedImageIndex = imageindex });
            }
            return Final;
        }

        public static void FromTreeNode(this RARC Arc, TreeNode Root) => Arc.Root = FromTreeNode(Root);

        public static RARCDirectory FromTreeNode(this TreeNode TN)
        {
            RARCDirectory Dir = new RARCDirectory() { Name = TN.Text };
            for (int i = 0; i < TN.Nodes.Count; i++)
                if (TN.Nodes[i].Tag == null)
                    Dir.SubDirectories.Add(FromTreeNode(TN.Nodes[i]));
                else
                {
                    Dir.Files.Add((RARCFile)TN.Nodes[i].Tag);
                    Dir.Files[Dir.Files.Count - 1].Name = TN.Nodes[i].Text;
                }
            return Dir;
        }
    }
}
