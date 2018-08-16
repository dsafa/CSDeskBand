using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/bb762048(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("012DD920-7B26-11D0-8CA9-00A0C92DBFE8")]
    public interface IDockingWindow : IOleWindow
    {
        #region IOleWindow
        [PreserveSig]
        new int GetWindow(out IntPtr phwnd);

        [PreserveSig]
        new int ContextSensitiveHelp(bool fEnterMode);
        #endregion

        [PreserveSig]
        int ShowDW([In] bool fShow);

        [PreserveSig]
        int CloseDW([In] UInt32 dwReserved);

        [PreserveSig]
        int ResizeBorderDW(RECT prcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] IntPtr punkToolbarSite, bool fReserved);
    }
}