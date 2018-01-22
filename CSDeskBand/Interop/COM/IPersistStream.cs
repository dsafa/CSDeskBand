﻿using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000109-0000-0000-C000-000000000046")]
    public interface IPersistStream : IPersist
    {
        #region IPersist

        [PreserveSig]
        new int GetClassID(out Guid pClassID);

        #endregion

        [PreserveSig]
        int GetSizeMax(out ulong pcbSize);

        [PreserveSig]
        int IsDirty();

        [PreserveSig]
        int Load([MarshalAs(UnmanagedType.IUnknown)] IntPtr pStm);

        [PreserveSig]
        int Save([MarshalAs(UnmanagedType.IUnknown)] IntPtr pStm, bool fClearDirty);
    }
}
