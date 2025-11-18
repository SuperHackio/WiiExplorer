using Hack.io.Interface;
using Hack.io.Utility;
using System.Text;

namespace WiiExplorer;

/* == Format Specification ==
 * 
 * 0x00 = MAGIC (WXSF)
 * 0x04 = SETTINGSVERSION
 * 0x08 = Last used Theme Index (int)
 * 0x0C = Last used Compression Index (int)
 * 0x10 = Last used Endian Index (int)
 */

/// <summary>
/// Settings file
/// </summary>
internal class Settings : ILoadSaveFile
{
    public const string MAGIC = "WXSF";
    public const string SETTINGSVERSION_LATEST = SETTINGSVERSION_0001;
    public const string SETTINGSVERSION_0001 = "0001";

    private FileStream DiskAccess;
    public bool IsFirstLaunch;

    public int ThemeIndex = 0;
    public int CompressionIndex = 0; // Default to Off
    public int PaddingIndex = 0; // Default to None
    public int EndianIndex = 1; // Default to Big Endian
    public bool AutoHandleCompression = true; // Default to automatically choosing a compression based on the loaded file type

    public Settings(string Path)
    {
        if (!File.Exists(Path))
        {
            //First time usage woo
            IsFirstLaunch = true;
            DiskAccess = new(Path, FileMode.OpenOrCreate);
            Save(DiskAccess);
            DiskAccess.Close();
            return;
        }

        DiskAccess = new(Path, FileMode.Open);
        Load(DiskAccess);
        DiskAccess.Close();
    }

    public void OnChanged(object? sender, EventArgs e)
    {
        DiskAccess = new(DiskAccess.Name, FileMode.Create);
        Save(DiskAccess);
        DiskAccess.Close();
    }

    public void Load(Stream Strm)
    {
        StreamUtil.PushEndianBig();
        try
        {
            FileUtil.ExceptionOnBadMagic(Strm, MAGIC);
            string Version = Strm.ReadString(4, Encoding.ASCII);

            if (Version.Equals(SETTINGSVERSION_0001))
            {
                ThemeIndex = Strm.ReadInt32();
                CompressionIndex = Strm.ReadInt32();
                PaddingIndex = Strm.ReadInt32();
                EndianIndex = Strm.ReadInt32();
                AutoHandleCompression = Strm.ReadInt32() > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to load settings: {e.Message}");
            DiskAccess.Close();

            IsFirstLaunch = true;
            DiskAccess = new(DiskAccess.Name, FileMode.Create);
            Save(DiskAccess);
            DiskAccess.Close();
        }
        finally
        {
            StreamUtil.PopEndian();
        }
    }

    public void Save(Stream Strm)
    {
        StreamUtil.PushEndianBig();

        Strm.WriteMagic(MAGIC);
        Strm.WriteString(SETTINGSVERSION_LATEST, Encoding.ASCII, null);

        Strm.WriteInt32(ThemeIndex);
        Strm.WriteInt32(CompressionIndex);
        Strm.WriteInt32(PaddingIndex);
        Strm.WriteInt32(EndianIndex);
        Strm.WriteInt32(AutoHandleCompression ? 1 : 0);

        StreamUtil.PopEndian();
    }
}
