using System;
using System.Runtime.InteropServices;

namespace CSDeskband.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OLECMDTEXT
    {
        public uint cmdtextf;
        public uint cwActual;
        public uint cwBuf;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        public string rgwz;
    }
}