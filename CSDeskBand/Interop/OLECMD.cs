using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct OLECMD
    {
        public uint cmdID;
        public uint cmdf;
    }
}
