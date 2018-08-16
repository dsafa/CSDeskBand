using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/ms680102(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000114-0000-0000-C000-000000000046")]
    public interface IOleWindow
    {
        [PreserveSig]
        int GetWindow(out IntPtr phwnd);

        [PreserveSig]
        int ContextSensitiveHelp(bool fEnterMode);
    }
}