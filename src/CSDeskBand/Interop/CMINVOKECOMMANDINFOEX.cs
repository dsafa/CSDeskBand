using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CMINVOKECOMMANDINFOEX
    {
        public uint cbSize;
        public CMIC fMask;
        public IntPtr hwnd;
        public IntPtr lpVerb;
        [MarshalAs(UnmanagedType.LPStr)] public string lpParameters;
        [MarshalAs(UnmanagedType.LPStr)] public string lpDirectory;
        public int nShow;
        public uint dwHotKey;
        public IntPtr hIcon;
        [MarshalAs(UnmanagedType.LPStr)] public string lpTitle;
        public IntPtr lpVerbW;
        [MarshalAs(UnmanagedType.LPWStr)] public string lpParametersW;
        [MarshalAs(UnmanagedType.LPWStr)] public string lpDirectoryW;
        [MarshalAs(UnmanagedType.LPWStr)] public string lpTitleW;
        public POINT ptInvoke;

        [Flags]
        public enum CMIC
        {
            CMIC_MASK_HOTKEY = 0x00000020,
            CMIC_MASK_ICON = 0x00000010,
            CMIC_MASK_FLAG_NO_UI = 0x00000400,
            CMIC_MASK_UNICODE = 0x00004000,
            CMIC_MASK_NO_CONSOLE = 0x00008000,
            CMIC_MASK_ASYNCOK = 0x00100000,
            CMIC_MASK_NOASYNC = 0x00000100,
            CMIC_MASK_SHIFT_DOWN = 0x10000000,
            CMIC_MASK_PTINVOKE = 0x20000000,
            CMIC_MASK_CONTROL_DOWN = 0x40000000,
            CMIC_MASK_FLAG_LOG_USAGE = 0x04000000,
            CMIC_MASK_NOZONECHECKS = 0x00800000,
        }
    }
}
