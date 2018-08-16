using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    internal class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool InsertMenuItem(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateMenu();

        [DllImport("user32.dll")]
        public static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern IntPtr CreatePopupMenu();

        public static int HiWord(int val)
        {
            return Convert.ToInt32(BitConverter.ToInt16(BitConverter.GetBytes(val), 2));
        }

        public static int LoWord(int val)
        {
            return Convert.ToInt32(BitConverter.ToInt16(BitConverter.GetBytes(val), 0));
        }
    }
}
