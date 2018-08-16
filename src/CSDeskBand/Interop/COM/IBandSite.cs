using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CF504B0-DE96-11D0-8B3F-00A0C911E8E5")]
    internal interface IBandSite
    {
        [PreserveSig]
        int AddBand(ref object punk);

        [PreserveSig]
        int EnumBands(int uBand, out uint pdwBandID);

        [PreserveSig]
        int QueryBand(uint dwBandID, out IDeskBand ppstb, out BANDSITEINFO.BSSF pdwState, [MarshalAs(UnmanagedType.LPWStr)] out string pszName, int cchName);

        [PreserveSig]
        int SetBandState(uint dwBandID, BANDSITEINFO.BSIM dwMask, BANDSITEINFO.BSSF dwState);

        [PreserveSig]
        int RemoveBand(uint dwBandID);

        [PreserveSig]
        int GetBandObject(uint dwBandID, ref Guid riid, out IntPtr ppv);

        [PreserveSig]
        int SetBandSiteInfo([In] ref BANDSITEINFO pbsinfo);

        [PreserveSig]
        int GetBandSiteInfo([In, Out] ref BANDSITEINFO pbsinfo);
    }
}
