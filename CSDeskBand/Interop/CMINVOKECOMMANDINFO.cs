using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMINVOKECOMMANDINFO
    {
        public int cbSize;
        public CMIC fMask;
        public IntPtr hwnd;
        public IntPtr lpVerb;
        [MarshalAs(UnmanagedType.LPStr)] public string lpParameters;
        [MarshalAs(UnmanagedType.LPStr)] public string lpDirectory;
        public int nShow;
        public int dwHotKey;
        public IntPtr hIcon;

        [Flags]
        public enum CMIC
        {
            CMIC_MASK_HOTKEY = 0x00000020,
            CMIC_MASK_ICON = 0x00000010,
            CMIC_MASK_FLAG_NO_UI = 0x00000400,
            CMIC_MASK_NO_CONSOLE = 0x00008000,
            CMIC_MASK_ASYNCOK = 0x00100000,
            CMIC_MASK_NOASYNC = 0x00000100,
            CMIC_MASK_SHIFT_DOWN = 0x10000000,
            CMIC_MASK_CONTROL_DOWN = 0x40000000,
            CMIC_MASK_FLAG_LOG_USAGE = 0x04000000,
            CMIC_MASK_NOZONECHECKS = 0x00800000,
        }
    }
}
