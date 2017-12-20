using System;
using System.Runtime.InteropServices;

namespace CSDeskband.Interop
{
    class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
