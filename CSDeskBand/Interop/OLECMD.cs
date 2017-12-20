using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OLECMD
    {
        public uint cmdID;
        public uint cmdf;
    }
}
