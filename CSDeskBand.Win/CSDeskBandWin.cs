using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSDeskBand.Interop;

namespace CSDeskBand.Win
{
    public class CSDeskBandWin: UserControl, ICSDeskBand
    {
        public CSDeskBandOptions Options { get; } = new CSDeskBandOptions();

        private readonly CSDeskBandImpl _impl;

        public CSDeskBandWin()
        {
            _impl = new CSDeskBandImpl(Handle, Options);
            _impl.VisibilityChanged += VisibilityChanged;
            _impl.OnClose += OnClose;
            _impl.TaskbarOrientationChanged += TaskbarOrientationChanged;
        }

        protected virtual void TaskbarOrientationChanged(object sender, TaskbarOrientationChangedEventArgs taskbarOrientationChangedEventArgs)
        {
        }

        protected virtual void OnClose(object sender, EventArgs eventArgs)
        {
            Dispose(true);
        }

        protected virtual void VisibilityChanged(object sender, VisibilityChangedEventArgs visibilityChangedEventArgs)
        {
            if (visibilityChangedEventArgs.IsVisible)
            {
                Show();
            }
            else
            {
                Hide();
            }
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

        [ComRegisterFunction]
        private static void Register(Type t)
        {
            CSDeskBandImpl.Register(t);
        }

        [ComUnregisterFunction]
        private static void Unregister(Type t)
        {
            CSDeskBandImpl.Unregister(t);
        }
    }
}