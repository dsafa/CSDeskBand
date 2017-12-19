using System;
using System.Runtime.InteropServices;

namespace CSDeskband.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/ms693765(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")]
    public interface IObjectWithSite
    {
        [PreserveSig]
        void SetSite([In, MarshalAs(UnmanagedType.IUnknown)] Object pUnkSite);

        [PreserveSig]
        void GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out Object ppvSite);
    }
}