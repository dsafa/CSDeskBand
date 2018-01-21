using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/bb761789(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("F1DB8392-7331-11D0-8C99-00A0C92DBFE8")]
    public interface IInputObjectSite
    {
        [PreserveSig]
        Int32 OnFocusChangeIS([MarshalAs(UnmanagedType.IUnknown)] object punkObj, Int32 fSetFocus);
    }
}