using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using CSDeskBand.Interop;
using CSDeskBand.Logging;

namespace CSDeskBand.Wpf
{
    public class CSDeskBandWpf : UserControl, ICSDeskBand
    {
        public CSDeskBandOptions Options { get; } = new CSDeskBandOptions();
        public TaskbarInfo TaskbarInfo { get; }

        //so we can get a handle
        private ElementHost Host { get; }
        private readonly CSDeskBandImpl _impl;
        private readonly ILog _logger;

        public CSDeskBandWpf()
        {
            _logger = LogProvider.GetCurrentClassLogger();
            try
            {
                Host = new ElementHost
                {
                    Child = this,
                    AutoSize = true,
                    BackColorTransparent = true
                };

                _impl = new CSDeskBandImpl(Host.Handle, Options);
                _impl.VisibilityChanged += VisibilityChanged;
                TaskbarInfo = _impl.TaskbarInfo;

                SizeChanged += CSDeskBandWpf_SizeChanged;
            }
            catch (Exception e)
            {
                _logger.DebugException("Initialization error", e);
                throw;
            }
        }

        private void CSDeskBandWpf_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (TaskbarInfo.Orientation == TaskbarOrientation.Horizontal)
            {
                Options.Horizontal = new System.Windows.Size(ActualWidth, ActualHeight);
            }
            else
            {
                Options.Vertical = new System.Windows.Size(ActualWidth, ActualHeight);
            }
        }

        private void VisibilityChanged(object sender, VisibilityChangedEventArgs visibilityChangedEventArgs)
        {
            VisibilityChanged(visibilityChangedEventArgs.IsVisible);
        }

        protected virtual void VisibilityChanged(bool visible)
        {
            Visibility = visible ? Visibility.Visible : Visibility.Hidden;
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

        public int QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags)
        {
            return _impl.QueryContextMenu(hMenu, indexMenu, idCmdFirst, idCmdLast, uFlags);
        }

        public int InvokeCommand(IntPtr pici)
        {
            return _impl.InvokeCommand(pici);
        }

        public int GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, out string pcszName, uint cchMax)
        {
            return _impl.GetCommandString(ref idcmd, uflags, ref pwReserved, out pcszName, cchMax);
        }

        public int HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            return _impl.HandleMenuMsg(uMsg, wParam, lParam);
        }

        public int HandleMenuMsg2(uint uMsg, IntPtr wParam, IntPtr lParam, out IntPtr plResult)
        {
            return _impl.HandleMenuMsg2(uMsg, wParam, lParam, out plResult);
        }
    }
}
