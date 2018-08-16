using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("68284faa-6a48-11d0-8c78-00c04fd918b4")]
    public interface IInputObject
    {
        [PreserveSig]
        void UIActivateIO(int fActivate, ref MSG msg);

        [PreserveSig]
        int HasFocusIO();

        [PreserveSig]
        int TranslateAcceleratorIO(ref MSG msg);
    }
}
