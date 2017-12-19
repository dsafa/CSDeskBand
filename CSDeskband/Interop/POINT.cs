using System.Runtime.InteropServices;

namespace CSDeskband.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}