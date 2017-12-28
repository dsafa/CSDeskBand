using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/ms683797(v=vs.85).aspx
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b722bccb-4e68-101b-a2bc-00aa00404770")]
    internal interface IOleCommandTarget
    {
        [PreserveSig]
        void QueryStatus(ref Guid pguidCmdGroup, uint cCmds, [MarshalAs(UnmanagedType.LPArray), In, Out] OLECMD[] prgCmds, [In, Out] ref OLECMDTEXT pCmdText);

        [PreserveSig]
        int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdExecOpt, ref object pvaIn, [In, Out] ref object pvaOut);
    }
}