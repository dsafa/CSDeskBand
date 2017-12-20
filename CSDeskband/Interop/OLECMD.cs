using System.Runtime.InteropServices;

namespace CSDeskband.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OLECMD
    {
        public uint cmdID;
        public uint cmdf;
    }
}
