using Hack.io.Class;
using Hack.io.RARC;
using Hack.io.Utility;
using System.Diagnostics;
using System.IO.Hashing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WiiExplorer;

public static class ControlEx
{
    [DebuggerStepThrough]
    // set instance non-public property with name "DoubleBuffered" to true
    public static void SetDoubleBuffered(this Control control)
        => typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, control, [true]);

    public static double DistanceTo(this Point from, Point to) => Math.Sqrt(Math.Pow(from.X - (double)to.X, 2) + Math.Pow(from.Y - (double)to.Y, 2));
}

public static class ArcUtil
{
    public const string INTERNAL_FOLDERCLOSE = "internal_folderclose";
    public const string INTERNAL_FOLDEROPEN = "internal_folderopen";
    public const string INTERNAL_FORMATUNKNOWN = "internal_formatunknown";

    public static TreeNode[] ToTreeNode(this Archive Arc, ImageList? ImageListing) => Arc.Root?.ToTreeNode(ImageListing) ?? throw new InvalidDataException();
    public static TreeNode[] ToTreeNode(this Archive Arc, ImageList? ImageListing, string Extension) => Arc.Root?.ToTreeNode(ImageListing, Extension) ?? throw new InvalidDataException();

    public static TreeNode[] ToTreeNode(this ArchiveDirectory Dir, ImageList? ImageListing)
    {
        List<TreeNode> Nodes = [];
        TreeNode Root = Dir.ToTreeNode_Internal(ImageListing) ?? throw new InvalidDataException(); //Very serious error
        for (int i = 0; i < Root.Nodes.Count; i++)
            Nodes.Add(Root.Nodes[i]);
        return [.. Nodes];
    }
    public static TreeNode[] ToTreeNode(this ArchiveDirectory Dir, ImageList? ImageListing, string Extension)
    {
        List<TreeNode> Nodes = [];
        TreeNode Root = Dir.ToTreeNode_Internal(ImageListing, Extension) ?? throw new InvalidDataException();
        for (int i = 0; i < Root.Nodes.Count; i++)
            Nodes.Add(Root.Nodes[i]);
        return [.. Nodes];
    }

    private static TreeNode ToTreeNode_Internal(this ArchiveDirectory Dir, ImageList? ImageListing)
    {
        int FolderClosedIdx = ImageListing?.Images.IndexOfKey(INTERNAL_FOLDERCLOSE) ?? -1;
        TreeNode Final = new(Dir.Name) { ImageIndex = FolderClosedIdx, SelectedImageIndex = FolderClosedIdx };
        foreach (KeyValuePair<string, object> CurrentItem in Dir.Items)
        {
            if (CurrentItem.Value is ArchiveDirectory subdir)
            {
                Final.Nodes.Add(subdir.ToTreeNode_Internal(ImageListing));
            }
            else if (CurrentItem.Value is ArchiveFile file)
            {
                int FormatIdx = IndexOfExtensionImageOrDefault(file.Name, ImageListing);
                Final.Nodes.Add(new TreeNode(file.Name) { Tag = file, ImageIndex = FormatIdx, SelectedImageIndex = FormatIdx });
            }
        }
        return Final;
    }
    private static TreeNode ToTreeNode_Internal(this ArchiveDirectory Dir, ImageList? ImageListing, string Extension)
    {
        int FolderClosedIdx = ImageListing?.Images.IndexOfKey(INTERNAL_FOLDERCLOSE) ?? -1;
        TreeNode Final = new(Dir.Name) { ImageIndex = FolderClosedIdx, SelectedImageIndex = FolderClosedIdx };
        foreach (KeyValuePair<string, object> CurrentItem in Dir.Items)
        {
            if (CurrentItem.Value is ArchiveDirectory subdir)
            {
                TreeNode tmp = subdir.ToTreeNode_Internal(ImageListing, Extension);
                if (tmp.Nodes.Count > 0)
                    Final.Nodes.Add(tmp);
            }
            else if (CurrentItem.Value is ArchiveFile file)
            {
                string[] exts = Extension.Split('|');
                for (int x = 0; x < exts.Length; x++)
                {
                    if (file.Extension?.Equals(exts[x]) ?? false)
                    {
                        int FormatIdx = IndexOfExtensionImageOrDefault(file.Extension, ImageListing);
                        Final.Nodes.Add(new TreeNode(file.Name) { Tag = file, ImageIndex = FormatIdx, SelectedImageIndex = FormatIdx });
                    }
                }
            }
        }
        return Final;
    }

    public static (ImageList ImgList, string[] ExtArray) CreateKnownExtensionList()
    {
        string ExtensionDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtensionData");
        List<string> KnownExtensions =
        [
            "Extensionless File|*"
        ];

        ImageList Images = new() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new(16, 16), TransparentColor = Color.Transparent };

        // Add the base images
        TryAddImage("INTERNAL_FolderClose", "internal_folderclose");
        TryAddImage("INTERNAL_FolderOpen", "internal_folderopen");
        TryAddImage("INTERNAL_FormatUnknown", "internal_formatunknown");

        string ExtensionListPath = Path.Combine(ExtensionDataPath, "ExtensionList.txt");
        if (!File.Exists(ExtensionListPath))
            goto ExitFunction; //CRY ABOUT IT

        string[] exts = File.ReadAllLines(ExtensionListPath);
        for (int i = 0; i < exts.Length; i++)
        {
            string line = exts[i];
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                continue; // Blank lines / Comments

            string[] linesplit = line.Split('|');
            if (linesplit.Length != 2)
                continue; // Likely incorrectly formatted

            KnownExtensions.Add(line);
            TryAddImage(linesplit[0], linesplit[1]);
        }


    ExitFunction:
        return (Images, KnownExtensions.ToArray());


        void TryAddImage(string Filename, string Key)
        {
            string ThemeString = Program.Settings.ThemeIndex switch
            {
                0 => ".Light",
                1 => ".Dark",
                _ => ""
            };
            if (!TryPath(Path.Combine(ExtensionDataPath, Filename + ThemeString + ".png")))
                TryPath(Path.Combine(ExtensionDataPath, Filename + ".png"));
            
            bool TryPath(string path)
            {
                if (!File.Exists(path))
                    return false;
                Images.Images.Add(Key, new Bitmap(path));
                return true;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Extension">File Extension. Must be formatted like `.ext`</param>
    /// <param name="ImageListing"></param>
    /// <returns></returns>
    public static int IndexOfExtensionImageOrDefault(string? Extension, ImageList? ImageListing)
    {
        if (ImageListing is not null && !string.IsNullOrWhiteSpace(Extension))
            foreach (string? item in ImageListing.Images.Keys)
                if (item is not null && !item.StartsWith("internal_") && Regex.IsMatch(Extension, StringUtil.WildCardToRegex(item)))
                    return ImageListing.Images.IndexOfKey(item);

        return IndexOfInternalImageOrDefault(INTERNAL_FORMATUNKNOWN, ImageListing);
    }
    public static int IndexOfInternalImageOrDefault(string Key, ImageList? ImageListing) => ImageListing?.Images.IndexOfKey(Key) ?? -1;

    // This method just re-processes the image indexing
    public static void RefreshTreeViewIcons(TreeNodeCollection NodeCollection, ImageList? ImageListing)
    {
        foreach (object? item in NodeCollection)
        {
            if (item is not TreeNode tn)
                continue; // I have no idea why I have to do this but OK
            if (tn.Tag is ArchiveFile file)
                tn.ImageIndex = tn.SelectedImageIndex = IndexOfExtensionImageOrDefault(file.Name, ImageListing);

            RefreshTreeViewIcons(tn.Nodes, ImageListing);
        }
    }

    // =========================================================================================================

    public static void FromTreeNode(this Archive Arc, TreeView Root)
    {
        if (Arc.Root is null)
            return;

        string[] NewFileOrder = new string[Root.Nodes.Count];
        for (int i = 0; i < Root.Nodes.Count; i++)
        {
            if (Arc[Root.Nodes[i].FullPath] is ArchiveDirectory)
            {
                FromTreeNode(Arc, Root.Nodes[i]);
            }
            NewFileOrder[i] = Root.Nodes[i].Text;
        }
        Arc.Root.SortItemsByOrder(NewFileOrder);
    }

    public static void FromTreeNode(Archive Arc, TreeNode TN)
    {
        string[] NewFileOrder = new string[TN.Nodes.Count];
        for (int i = 0; i < TN.Nodes.Count; i++)
        {
            if (Arc[TN.Nodes[i].FullPath] is ArchiveDirectory)
                FromTreeNode(Arc, TN.Nodes[i]);

            NewFileOrder[i] = TN.Nodes[i].Text;
        }
        var fp = Arc[TN.FullPath] ?? throw new Exception("This should be impossible");
        ((ArchiveDirectory)fp).SortItemsByOrder(NewFileOrder);
    }

    public static TreeNode? FindTreeNodeByFullPath(this TreeNodeCollection collection, string fullPath, string? Root = null, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        var foundNode = collection.Cast<TreeNode>().FirstOrDefault(tn => string.Equals((Root is null ? "" : Root + "/") + tn.FullPath, fullPath, comparison));
        if (foundNode is null)
            foreach (var childNode in collection.Cast<TreeNode>())
            {
                var foundChildNode = FindTreeNodeByFullPath(childNode.Nodes, fullPath, Root, comparison);
                if (foundChildNode is not null)
                    return foundChildNode;
            }

        return foundNode;
    }
    public static TreeNode? FindTreeNodeByReference(this TreeNodeCollection collection, TreeNode node)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            TreeNode tn = collection[i];
            if (ReferenceEquals(tn, node))
                return node;
            TreeNode? nt = FindTreeNodeByReference(tn.Nodes, node);
            if (nt is not null)
                return nt;
        }
        return null;
    }

    public static void ReOrderDirectory(this ArchiveDirectory Dir, TreeNodeCollection Nodes)
    {
        string[] NewFileOrder = new string[Nodes.Count];
        for (int i = 0; i < Nodes.Count; i++)
        {
            NewFileOrder[i] = Nodes[i].Text;
        }
        Dir.SortItemsByOrder(NewFileOrder);
    }

    // =========================================================================================================

    public static dynamic? CreateNewDirectory(Archive Archive)
    {
        if (Archive.Root is null)
            return null; // bruh...
        Type ArchiveDirectoryType = Archive.Root.GetType();
        ConstructorInfo? ctor = ArchiveDirectoryType.GetConstructor(Type.EmptyTypes);
        if (ctor is null)
            return null; // Can't create anything...
        return ctor.Invoke([]);
    }

    public static dynamic? CreateNewFile(Archive Archive)
    {
        // For now I'm just hardcoding it...
        if (Archive is RARC)
            return new RARC.File();

        return new ArchiveFile();
    }

    // Just assume 0x00 is the pad byte for now...
    public static long PadFile(string Filepath, int PadTo, byte PadByte = 0x00)
    {
        if (!File.Exists(Filepath))
            throw new FileNotFoundException();

        FileStream FS = new(Filepath, FileMode.Open, FileAccess.Write);
        FS.Position = FS.Length;
        FS.PadTo(PadTo, PadByte);
        long length = FS.Length;
        FS.Close();
        return length;
    }
}

public static class HashUtil
{
    public static uint CalcCRC32(ReadOnlySpan<byte> data) => Crc32.HashToUInt32(data);
}