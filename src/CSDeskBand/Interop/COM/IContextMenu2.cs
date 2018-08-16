using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214f4-0000-0000-c000-000000000046")]
    public interface IContextMenu2 : IContextMenu
    {
        [PreserveSig]
        new int QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags);

        [PreserveSig]
        new int InvokeCommand(IntPtr pici);

        [PreserveSig]
        new int GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, [MarshalAs(UnmanagedType.LPTStr)] out string pcszName, uint cchMax);

        [PreserveSig]
        int HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam);
    }
}
