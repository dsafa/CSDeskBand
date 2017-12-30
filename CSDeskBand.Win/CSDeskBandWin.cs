using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSDeskBand.Interop;
using CSDeskBand.Logging;

namespace CSDeskBand.Win
{
    public class CSDeskBandWin: UserControl, ICSDeskBand
    {
        protected CSDeskBandOptions Options { get; } = new CSDeskBandOptions();
        protected TaskbarInfo TaskbarInfo { get; }

        private readonly CSDeskBandImpl _impl;
        private readonly ILog _logger;

        public CSDeskBandWin()
        {
            _logger = LogProvider.GetCurrentClassLogger();
            try
            {
                _impl = new CSDeskBandImpl(Handle, Options);
                _impl.VisibilityChanged += VisibilityChanged;
                _impl.Closed += OnClose;
                TaskbarInfo = _impl.TaskbarInfo;
            }
            catch (Exception e)
            {
                _logger.DebugException("Initialization Error", e);
                throw;
            }
        }

        private void OnClose(object sender, EventArgs eventArgs)
        {
            OnClose();
        }

        private void VisibilityChanged(object sender, VisibilityChangedEventArgs visibilityChangedEventArgs)
        {
            VisibilityChanged(visibilityChangedEventArgs.IsVisible);
        }

        protected virtual void OnClose()
        {
            Dispose(true);
        }

        protected virtual void VisibilityChanged(bool visible)
        {
            if (visible)
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