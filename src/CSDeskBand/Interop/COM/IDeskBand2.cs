using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/bb762064(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79D16DE4-ABEE-4021-8D9D-9169B261D657")]
    public interface IDeskBand2 : IDeskBand
    {
        #region IOleWindow
        [PreserveSig]
        new int GetWindow(out IntPtr phwnd);

        [PreserveSig]
        new int ContextSensitiveHelp(bool fEnterMode);
        #endregion

        #region IDockingWindow
        [PreserveSig]
        new int ShowDW([In] bool fShow);

        [PreserveSig]
        new int CloseDW([In] UInt32 dwReserved);

        [PreserveSig]
        new int ResizeBorderDW(RECT prcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] IntPtr punkToolbarSite, bool fReserved);
        #endregion

        #region IDeskBand
        [PreserveSig]
        new int GetBandInfo(UInt32 dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi);
        #endregion

        [PreserveSig]
        int CanRenderComposited(out bool pfCanRenderComposited);

        [PreserveSig]
        int SetCompositionState(bool fCompositionEnabled);

        [PreserveSig]
        int GetCompositionState(out bool pfCompositionEnabled);
    }
}