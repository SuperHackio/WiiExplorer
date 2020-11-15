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
                    Final.Nodes.Add(new TreeNode(file.Name) { ImageIndex = imageindex, SelectedImageIndex = imageindex });
                }
            }
            return Final;
        }

        public static void FromTreeNode(this RARC Arc, TreeView Root)
        {
            string[] NewFileOrder = new string[Root.Nodes.Count];
            for (int i = 0; i < Root.Nodes.Count; i++)
            {
                if (Arc[Root.Nodes[i].FullPath] is RARC.Directory)
                {
                    FromTreeNode(Arc, Root.Nodes[i]);
                }
                NewFileOrder[i] = Root.Nodes[i].Text;
            }
            Arc.Root.SortItemsByOrder(NewFileOrder);
        }

        public static void FromTreeNode(RARC Arc, TreeNode TN)
        {
            string[] NewFileOrder = new string[TN.Nodes.Count];
            for (int i = 0; i < TN.Nodes.Count; i++)
            {
                if (Arc[TN.Nodes[i].FullPath] is RARC.Directory)
                    FromTreeNode(Arc, TN.Nodes[i]);

                NewFileOrder[i] = TN.Nodes[i].Text;
            }
            ((RARC.Directory)Arc[TN.FullPath]).SortItemsByOrder(NewFileOrder);
        }
        
        public static TreeNode FindTreeNodeByFullPath(this TreeNodeCollection collection, string fullPath, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            var foundNode = collection.Cast<TreeNode>().FirstOrDefault(tn => string.Equals(tn.FullPath, fullPath, comparison));
            if (null == foundNode)
                foreach (var childNode in collection.Cast<TreeNode>())
                {
                    var foundChildNode = FindTreeNodeByFullPath(childNode.Nodes, fullPath, comparison);
                    if (null != foundChildNode)
                        return foundChildNode;
                }

            return foundNode;
        }
    }
}
