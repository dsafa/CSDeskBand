using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MENUITEMINFO
    {
        public int cbSize;
        public MIIM fMask;
        public MFT fType;
        public MFS fState;
        public uint wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;
        [MarshalAs(UnmanagedType.LPStr)] public string dwTypeData;
        public uint cch;
        public IntPtr hbmpItem;

        [Flags]
        public enum MIIM : uint
        {
            MIIM_BITMAP = 0x00000080,
            MIIM_CHECKMARKS = 0x00000008,
            MIIM_DATA = 0x00000020,
            MIIM_FTYPE = 0x00000100,
            MIIM_ID = 0x00000002,
            MIIM_STATE = 0x00000001,
            MIIM_STRING = 0x00000040,
            MIIM_SUBMENU = 0x00000004,
            MIIM_TYPE = 0x00000010
        }

        [Flags]
        public enum MFT : uint
        {
            MFT_BITMAP = 0x00000004,
            MFT_MENUBARBREAK = 0x00000020,
            MFT_MENUBREAK = 0x00000040,
            MFT_OWNERDRAW = 0x00000100,
            MFT_RADIOCHECK = 0x00000200,
            MFT_RIGHTJUSTIFY = 0x00004000,
            MFT_RIGHTORDER = 0x00002000,
            MFT_SEPARATOR = 0x00000800,
            MFT_STRING = 0x00000000,
        }

        [Flags]
        public enum MFS : uint
        {
            MFS_CHECKED = 0x00000008,
            MFS_DEFAULT = 0x00001000,
            MFS_DISABLED = 0x00000003,
            MFS_ENABLED = 0x00000000,
            MFS_GRAYED = 0x00000003,
            MFS_HILITE = 0x00000080,
            MFS_UNCHECKED = 0x00000000,
            MFS_UNHILITE = 0x00000000,
        }
    }
}