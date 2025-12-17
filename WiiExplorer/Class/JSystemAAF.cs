using Hack.io.Class;
using Hack.io.Utility;
using System.Text;

namespace WiiExplorer.Class;

public class JSystemAAF : Archive
{
    protected override void Read(Stream Strm)
    {
        // How to prevent loading malformed files if there's no magic?
    }

    protected override void Write(Stream Strm)
    {
        
    }
}
