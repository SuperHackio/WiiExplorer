using Hack.io.Class;
using Hack.io.Utility;
using System.Text;

namespace WiiExplorer.Class;

public class JSystemBAA : Archive
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
            Span<char> cr = Strm.ReadString(4, Encoding.ASCII).ToCharArray();
            StreamUtil.ApplyEndian(cr, true);
            name = cr.ToString();
            uint Start, End;

            if (name.Equals(CLOSER))
                continue;

            NameCounter.TryAdd(name, 0);

            ArchiveFile newfile = new();
            MemoryStream ms;
            string filekey;
            switch (name)
            {
                case "bst ":
                case "bstn":
                case "bsc ":
                    {
                        Start = Strm.ReadUInt32();
                        End = Strm.ReadUInt32();
                        ms = StreamUtil.CreateStreamSlice(Strm, Start, End - Start);
                        filekey = $"{NameCounter[name]++:000}";
                    }
                    break;
                case "ws  ":
                    {
                        int WSysFileID = Strm.ReadInt32(),
                            Offset1 = Strm.ReadInt32(),
                            LoadFlags = Strm.ReadInt32();

                        uint Size1 = SizeFromFileOffset(Strm, Offset1, 4, 0x00, 32);
                        ms = StreamUtil.CreateStreamSlice(Strm, Offset1, Size1);
                        filekey = $"WSYS_{WSysFileID:000}_{LoadFlags:X8}";
                    }
                    break;
                case "bnk ":
                    {
                        int Unk1_ = Strm.ReadInt32(),
                            Offset2 = Strm.ReadInt32();

                        uint Size2 = SizeFromFileOffset(Strm, Offset2, 4, 0x08, 16);
                        ms = StreamUtil.CreateStreamSlice(Strm, Offset2, Size2);
                        filekey = $"IBNK_{Unk1_:000}";
                    }
                    break;
                case "bsft":
                    {
                        // This format sucks. We have to GUESS it's size...
                        // to do this, we will traverse to the presumed end of the file
                        int Offset = Strm.ReadInt32();
                        long PausePosition = Strm.Position;

                        Strm.Position = Offset;
                        Strm.Position += 0x04; // Skip Magic
                        uint StringCount = Strm.ReadUInt32();
                        Strm.Position += 0x04 * StringCount;
                        Strm.Position -= 0x04;
                        uint LastStringOffset = Strm.ReadUInt32();
                        Strm.Position = Offset + LastStringOffset;
                        string laststring = Strm.ReadString(Encoding.ASCII);

                        long EndPos = Offset + LastStringOffset + laststring.Length + 1;
                        EndPos += StreamUtil.CalculatePaddingLength(EndPos, 32);

                        ms = StreamUtil.CreateStreamSlice(Strm, Offset, EndPos - Offset);
                        filekey = $"{NameCounter[name]++:000}";

                        Strm.Position = PausePosition;
                    }
                    break;
                case "bms ":
                    {
                        uint JAudioID = Strm.ReadUInt32();
                        Start = Strm.ReadUInt32();
                        End = Strm.ReadUInt32();
                        ms = StreamUtil.CreateStreamSlice(Strm, Start, End - Start);
                        filekey = $"BMS_{JAudioID:X8}";
                    }
                    break;
                default:
                    //throw new NotSupportedException($"A file with subfile format {name} is not supported.");
                    filekey = "";
                    ms = null;
                    continue;
            }

            newfile.Load(ms);
            this[$"{filekey}.{name.TrimEnd()}"] = newfile;

        } while (!name.Equals(CLOSER));
        if (Root is not null)
            Root.Name = "";

        static uint SizeFromFileOffset(Stream Strm, long FileOffset, int OffsetInFile, uint? Additive, int? SizePad)
        {
            long PausePos = Strm.Position;
            Strm.Position = FileOffset + OffsetInFile;
            uint Size = Strm.ReadUInt32();
            if (Additive is uint a)
                Size += a;
            if (SizePad is int p)
                Size += (uint)StreamUtil.CalculatePaddingLength(Size, p);
            Strm.Position = PausePos;
            return Size;
        }
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
        AddNodes("*.bsft");
        AddNodes("*.bms");
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
            switch (ArchiveFiles[i].Extension)
            {
                case ".bsft":
                    HeaderSize += 0x08;
                    break;
                case ".ws":
                case ".bms":
                    HeaderSize += 0x10;
                    break;
                default:
                    HeaderSize += 0x0C;
                    break;
            }

            MemoryStream ms = new();
            ArchiveFiles[i].Save(ms);
            FileDatas.Add(ms);
        }

        Strm.WriteMagic(OPENER);
        long FilePointer = HeaderSize;
        for (int i = 0; i < ArchiveFiles.Count; i++)
        {
            ArchiveFile AF = ArchiveFiles[i];
            switch (AF.Extension)
            {
                case ".bst":
                    Strm.WriteMagic("bst ");
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                case ".bstn":
                    Strm.WriteMagic("bstn");
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                case ".bsc":
                    Strm.WriteMagic("bsc ");
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                case ".ws":
                    string NameNoExtension = AF.Name[..^AF.Extension.Length];
                    string[] namesplit = NameNoExtension.Split('_');
                    uint Unk1 = 0; // I know we can default to Zero for this
                    uint Unk2 = 0;
                    if (namesplit.Length == 3)
                        _ = uint.TryParse(namesplit[2], System.Globalization.NumberStyles.HexNumber, null, out Unk1) & uint.TryParse(namesplit[1], out Unk2); // Single '&' is not a mistake
                    else if (namesplit.Length == 2)
                        _ = uint.TryParse(namesplit[1], out Unk2);

                    Strm.WriteMagic("ws  ");
                    Strm.WriteUInt32(Unk2);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32(Unk1);
                    break;
                case ".bnk":
                    string NameNoExtension2 = AF.Name[..^AF.Extension.Length];
                    string[] namesplit2 = NameNoExtension2.Split('_');
                    uint Unk3 = 0;
                    if (namesplit2.Length == 2)
                        _ = uint.TryParse(namesplit2[1], out Unk3);

                    Strm.WriteMagic("bnk ");
                    Strm.WriteUInt32(Unk3);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    break;
                case ".bsft":
                    Strm.WriteMagic("bsft");
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    break;
                case ".bms":
                    string NameNoExtension3 = AF.Name[..^AF.Extension.Length];
                    string[] namesplit3 = NameNoExtension3.Split('_');
                    uint JAudioID = 0;
                    if (namesplit3.Length == 2)
                        _ = uint.TryParse(namesplit3[1], System.Globalization.NumberStyles.HexNumber, null, out JAudioID);

                    Strm.WriteMagic("bms ");
                    Strm.WriteUInt32(JAudioID);
                    Strm.WriteUInt32((uint)FilePointer);
                    FilePointer += FileDatas[i].Length;
                    Strm.WriteUInt32((uint)FilePointer);
                    break;
                default:
                    throw new InvalidOperationException("This should be impossible...");
            }
        }
        Strm.WriteMagic(CLOSER);
        for (int i = 0; i < FileDatas.Count; i++)
            Strm.Write(FileDatas[i].ToArray());
        Strm.PadTo(16);
    }
}
