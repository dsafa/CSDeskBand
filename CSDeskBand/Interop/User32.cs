using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    internal class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
