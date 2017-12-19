using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using CSDeskband.Interop;

namespace CSDeskband.Wpf
{
    public class CSDeskBand : UserControl, ICSDeskBand
    {
        public System.Drawing.Size MinVertical { get; set; } = new System.Drawing.Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, 100);
        public System.Drawing.Size MaxVertical { get; set; } = new System.Drawing.Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, 100);
        public System.Drawing.Size Vertical { get; set; } = new System.Drawing.Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, 100);
        public System.Drawing.Size MinHorizontal { get; set; } = new System.Drawing.Size(100, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);
        public System.Drawing.Size MaxHorizontal { get; set; } = new System.Drawing.Size(100, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);
        public System.Drawing.Size Horizontal { get; set; } = new System.Drawing.Size(100, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);
        public int Increment { get; set; } = CSDeskBandImpl.NO_LIMIT;
        public string Title { get; set; } = "";
        public CSDeskBandOptions Options { get; set; } = new CSDeskBandOptions();

        private CSDeskBandImpl _impl;

        public CSDeskBand()
        {
            var handleSrc = (HwndSource)PresentationSource.FromVisual(this);
            _impl = new CSDeskBandImpl(IntPtr.Zero)
            {
                MinHorizontal = MinHorizontal,
                MaxHorizontal = MaxHorizontal,
                Horizontal = Horizontal,
                MinVertical = MinVertical,
                MaxVertical = MaxVertical,
                Vertical = Vertical,
                Increment = Increment,
                Title = Title,
                Options = Options,
            };
        }

        public int GetWindow(out IntPtr phwnd)
        {
            return _impl.GetWindow(out phwnd);
        }

        public int ContextSensitiveHelp(bool fEnterMode)
        {
            return _impl.ContextSensitiveHelp(fEnterMode);
        }

        public int ShowDW([In] bool fShow)
        {
            Visibility = fShow ? Visibility.Visible : Visibility.Hidden;
            return _impl.ShowDW(fShow);
        }

        public int CloseDW([In] uint dwReserved)
        {
            return _impl.CloseDW(dwReserved);
        }

        public int ResizeBorderDW(RECT prcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] IntPtr punkToolbarSite, bool fReserved)
        {
            return _impl.ResizeBorderDW(prcBorder, punkToolbarSite, fReserved);
        }

        public int GetBandInfo(uint dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi)
        {
            return _impl.GetBandInfo(dwBandID, dwViewMode, ref pdbi);
        }

        public int CanRenderComposited(out bool pfCanRenderComposited)
        {
            return _impl.CanRenderComposited(out pfCanRenderComposited);
        }

        public int SetCompositionState(bool fCompositionEnabled)
        {
            return _impl.SetCompositionState(fCompositionEnabled);
        }

        public int GetCompositionState(out bool pfCompositionEnabled)
        {
            return _impl.GetCompositionState(out pfCompositionEnabled);
        }

        public void SetSite([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkSite)
        {
            _impl.SetSite(pUnkSite);
        }

        public void GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvSite)
        {
            _impl.GetSite(ref riid, out ppvSite);
        }
    }
}
