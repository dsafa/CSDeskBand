using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010c-0000-0000-C000-000000000046")]
    public interface IPersist
    {
        [PreserveSig]
        int GetClassID(out Guid pClassID);
    }
}
