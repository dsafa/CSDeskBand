using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/ms693765(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")]
    public interface IObjectWithSite
    {
        //Deskband does not work when these methods return a value
        [PreserveSig]
        void SetSite([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkSite);

        [PreserveSig]
        void GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvSite);
    }
}