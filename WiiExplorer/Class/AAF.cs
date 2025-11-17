using Hack.io.Class;
using Hack.io.Utility;
using System.Text;

namespace WiiExplorer.Class;

public class AAF : Archive
{
    public const string OPENER = "AA_<";
    public const string CLOSER = ">_AA";


    protected override void Read(Stream Strm)
    {
        Dictionary<string, int> NameCounter = [];
        FileUtil.ExceptionOnBadMagic(Strm, OPENER);
        string name;
        do
        {
            name = Strm.ReadString(4, Encoding.ASCII);
            uint Start, End;
            MemoryStream ms;

            if (name.Equals(CLOSER))
                continue;

            NameCounter.TryAdd(name, 0);

            switch (name)
            {
                case "bst ":
                case "bstn":
                case "bsc ":
                    ArchiveFile f1 = new();
                    Start = Strm.ReadUInt32();
                    End = Strm.ReadUInt32();
                    ms = StreamUtil.CreateStreamSlice(Strm, Start, End - Start);
                    f1.Load(ms);
                    this[$"{NameCounter[name]++:000}.{name.TrimEnd()}"] = f1;
                    break;
                case "ws  ":
                    int WSysFileID = Strm.ReadInt32(),
                        Offset1 = Strm.ReadInt32(),
                        Unk3 = Strm.ReadInt32();

                    ArchiveFile f2 = new();
                    long PausePos1 = Strm.Position;
                    Strm.Position = Offset1 + 4;
                    uint Size1 = Strm.ReadUInt32();
                    Size1 += (uint)StreamUtil.CalculatePaddingLength(Size1, 32);
                    Strm.Position = Offset1;
                    ms = StreamUtil.CreateStreamSlice(Strm, Offset1, Size1);
                    Strm.Position = PausePos1;
                    f2.Load(ms);
                    this[$"WSYS_{WSysFileID:000}_{Unk3:X8}.{name.TrimEnd()}"] = f2;
                    break;
                case "bnk ":
                    int Unk1_ = Strm.ReadInt32(),
                        Offset2 = Strm.ReadInt32();

                    ArchiveFile f3 = new();
                    long PausePos2 = Strm.Position;
                    Strm.Position = Offset2 + 4;
                    uint Size2 = Strm.ReadUInt32();
                    Size2 += 0x08;
                    Size2 += (uint)StreamUtil.CalculatePaddingLength(Size2, 16);
                    ms = StreamUtil.CreateStreamSlice(Strm, Offset2, Size2);
                    Strm.Position = PausePos2;
                    f3.Load(ms);
                    this[$"BNK_{Unk1_:000}.{name.TrimEnd()}"] = f3;
                    break;
            }

        } while (!name.Equals(CLOSER));
        if (Root is not null)
            Root.Name = "";
    }

    protected override void Write(Stream Strm)
    {
        //Calculate the size of the header and allocate that space
        int HeaderSize = 8; //8 = 4 for the Opener and 4 for the Closer
        List<MemoryStream> FileDatas = [];
        List<ArchiveFile> ArchiveFiles = []; // Need to flatten this archive as the format

        AddNodes("*.bst");
        AddNodes("*.bstn");
        AddNodes("*.bsc");
        AddNodes("*.ws");
        AddNodes("*.bnk");
        // Anything not supported gets ignored... Should there be a warning for this somehow?

        void AddNodes(string regex)
        {
            var f = FindItems(regex);
            foreach (var name in f)
                if (this[name] is ArchiveFile af)
                    ArchiveFiles.Add(af);
        }

        for (int i = 0; i < ArchiveFiles.Count; i++)
        {
            if (ArchiveFiles[i].Extension?.Equals(".ws") ?? false)
                HeaderSize += 0x10;
            else
                HeaderSize += 0x0C;

            MemoryStream ms = new();
            ArchiveFiles[i].Save(ms);
            FileDatas.Add(ms);
        }
        ;
        Strm.WriteString(OPENER, Encoding.ASCII, null);
        long FilePointer = HeaderSize;
        for (int i = 0; i < ArchiveFiles.Count; i++)
        {
            ArchiveFile AF = ArchiveFiles[i];
            switch (AF.Extension)
            {
                case ".bst":
                    Strm.WriteString("bst ", Encoding.ASCII, null);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                case ".bstn":
                    Strm.WriteString("bstn", Encoding.ASCII, null);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                case ".bsc":
                    Strm.WriteString("bsc ", Encoding.ASCII, null);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                case ".ws":
                    string NameNoExtension = AF.Name[..^AF.Extension.Length];
                    string[] namesplit = NameNoExtension.Split('_');
                    uint Unk1 = 0; // I know we can default to Zero for this
                    uint Unk2 = 0;
                    if (namesplit.Length == 2)
                        _ = uint.TryParse(namesplit[1], System.Globalization.NumberStyles.HexNumber, null, out Unk1) & uint.TryParse(namesplit[0], out Unk2); // Single '&' is not a mistake
                    else if (namesplit.Length == 1)
                        _ = uint.TryParse(namesplit[0], out Unk2);

                    Strm.WriteString("ws  ", Encoding.ASCII, null);
                    Strm.WriteUInt32(Unk2);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32(Unk1);
                    break;
                case ".bnk":
                    string NameNoExtension2 = AF.Name[..^AF.Extension.Length];
                    string[] namesplit2 = NameNoExtension2.Split('_');
                    uint Unk3 = 0;
                    if (namesplit2.Length == 1)
                        _ = uint.TryParse(namesplit2[0], out Unk3);

                    Strm.WriteString("bnk ", Encoding.ASCII, null);
                    Strm.WriteUInt32(Unk3);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    break;
                default:
                    throw new InvalidOperationException("This should be impossible...");
            }
        }
        Strm.WriteString(CLOSER, Encoding.ASCII, null);
        for (int i = 0; i < FileDatas.Count; i++)
            Strm.Write(FileDatas[i].ToArray());
        Strm.PadTo(16);
    }
}
