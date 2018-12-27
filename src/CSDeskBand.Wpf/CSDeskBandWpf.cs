using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using CSDeskBand.Interop;
using CSDeskBand.Interop.COM;
using CSDeskBand.Logging;

namespace CSDeskBand.Wpf
{
    /// <summary>
    /// Wpf implementation of <see cref="ICSDeskBand"/>. The deskband should inherit this class.
    /// The deskband should also have these attributes <see cref="ComVisibleAttribute"/>, <see cref="GuidAttribute"/>, <see cref="CSDeskBandRegistrationAttribute"/>.
    /// </summary>
    public class CSDeskBandWpf : UserControl, ICSDeskBand
    {
        private readonly ILog _logger = LogHelper.GetLogger(typeof(CSDeskBandWpf));
        private readonly CSDeskBandWpfHost _host;
        private readonly CSDeskBandImpl _impl;
        private readonly Guid _deskbandGuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSDeskBandWpf"/> class.
        /// </summary>
        public CSDeskBandWpf()
        {
            try
            {
                Options.Title = RegistrationHelper.GetToolbarName(GetType());

                if (DesignerProperties.GetIsInDesignMode(this))
                {
                    _impl = new CSDeskBandImpl(IntPtr.Zero, Options);
                }
                else
                {
                    _host = new CSDeskBandWpfHost(this);
                    _impl = new CSDeskBandImpl(_host.Handle, Options);
                }

                _impl.VisibilityChanged += VisibilityChanged;
                _impl.Closed += OnClose;

                TaskbarInfo = _impl.TaskbarInfo;

                _deskbandGuid = new Guid(GetType().GetCustomAttribute<GuidAttribute>(true)?.Value ?? Guid.Empty.ToString("B"));
            }
            catch (Exception e)
            {
                _logger.DebugException("Initialization error", e);
                throw;
            }
        }

        /// <summary>
        /// Gets options for this deskband.
        /// </summary>
        /// <seealso cref="CSDeskBandOptions"/>
        public CSDeskBandOptions Options { get; } = new CSDeskBandOptions();

        /// <summary>
        /// Gets the current taskbar information.
        /// </summary>
        /// <seealso cref="TaskbarInfo"/>
        public TaskbarInfo TaskbarInfo { get; }

        /// <summary>
        /// Method is called when deskband is being closed.
        /// </summary>
        protected virtual void OnClose()
        {
        }

        /// <summary>
        /// Method is called when deskband visibility has changed.
        /// </summary>
        /// <param name="visible">The value indicating whether the deskband is visible.</param>
        protected virtual void VisibilityChanged(bool visible)
        {
            Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Close the deskband.
        /// </summary>
        protected void CloseDeskBand()
        {
            _impl.CloseDeskBand();
        }

        [ComRegisterFunction]
        private static void Register(Type t)
        {
            RegistrationHelper.Register(t);
        }

        [ComUnregisterFunction]
        private static void Unregister(Type t)
        {
            RegistrationHelper.Unregister(t);
        }

        private void OnClose(object sender, EventArgs eventArgs)
        {
            _host.Close();
            OnClose();
        }

        private void VisibilityChanged(object sender, VisibilityChangedEventArgs visibilityChangedEventArgs)
        {
            VisibilityChanged(visibilityChangedEventArgs.IsVisible);
        }

#pragma warning disable SA1202 // Elements must be ordered by access
#pragma warning disable SA1600 // Elements must be documented
        int IDeskBand2.ShowDW(bool fShow)
        {
            return _impl.ShowDW(fShow);
        }

        int IDeskBand2.CloseDW(uint dwReserved)
        {
            return _impl.CloseDW(dwReserved);
        }

        int IDeskBand2.ResizeBorderDW(RECT prcBorder, IntPtr punkToolbarSite, bool fReserved)
        {
            return _impl.ResizeBorderDW(prcBorder, punkToolbarSite, fReserved);
        }

        int IDeskBand2.GetBandInfo(uint dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi)
        {
            return _impl.GetBandInfo(dwBandID, dwViewMode, ref pdbi);
        }

        int IDeskBand2.CanRenderComposited(out bool pfCanRenderComposited)
        {
            return _impl.CanRenderComposited(out pfCanRenderComposited);
        }

        int IDeskBand2.SetCompositionState(bool fCompositionEnabled)
        {
            return _impl.SetCompositionState(fCompositionEnabled);
        }

        int IDeskBand2.GetCompositionState(out bool pfCompositionEnabled)
        {
            return _impl.GetCompositionState(out pfCompositionEnabled);
        }

        int IDeskBand2.GetWindow(out IntPtr phwnd)
        {
            return _impl.GetWindow(out phwnd);
        }

        int IDeskBand2.ContextSensitiveHelp(bool fEnterMode)
        {
            return _impl.ContextSensitiveHelp(fEnterMode);
        }

        int IDeskBand.GetWindow(out IntPtr phwnd)
        {
            return _impl.GetWindow(out phwnd);
        }

        int IDeskBand.ContextSensitiveHelp(bool fEnterMode)
        {
            return _impl.ContextSensitiveHelp(fEnterMode);
        }

        int IDeskBand.ShowDW(bool fShow)
        {
            return _impl.ShowDW(fShow);
        }

        int IDeskBand.CloseDW(uint dwReserved)
        {
            return _impl.CloseDW(dwReserved);
        }

        int IDeskBand.ResizeBorderDW(RECT prcBorder, IntPtr punkToolbarSite, bool fReserved)
        {
            return _impl.ResizeBorderDW(prcBorder, punkToolbarSite, fReserved);
        }

        int IDeskBand.GetBandInfo(uint dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi)
        {
            return _impl.GetBandInfo(dwBandID, dwViewMode, ref pdbi);
        }

        int IDockingWindow.GetWindow(out IntPtr phwnd)
        {
            return _impl.GetWindow(out phwnd);
        }

        int IDockingWindow.ContextSensitiveHelp(bool fEnterMode)
        {
            return _impl.ContextSensitiveHelp(fEnterMode);
        }

        int IDockingWindow.ShowDW(bool fShow)
        {
            return _impl.ShowDW(fShow);
        }

        int IDockingWindow.CloseDW(uint dwReserved)
        {
            return _impl.CloseDW(dwReserved);
        }

        int IDockingWindow.ResizeBorderDW(RECT prcBorder, IntPtr punkToolbarSite, bool fReserved)
        {
            return _impl.ResizeBorderDW(prcBorder, punkToolbarSite, fReserved);
        }

        int IOleWindow.GetWindow(out IntPtr phwnd)
        {
            return _impl.GetWindow(out phwnd);
        }

        int IOleWindow.ContextSensitiveHelp(bool fEnterMode)
        {
            return _impl.ContextSensitiveHelp(fEnterMode);
        }

        void IObjectWithSite.SetSite(object pUnkSite)
        {
            _impl.SetSite(pUnkSite);
        }

        void IObjectWithSite.GetSite(ref Guid riid, out object ppvSite)
        {
            _impl.GetSite(ref riid, out ppvSite);
        }

        int IContextMenu3.QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags)
        {
            return _impl.QueryContextMenu(hMenu, indexMenu, idCmdFirst, idCmdLast, uFlags);
        }

        int IContextMenu3.InvokeCommand(IntPtr pici)
        {
            return _impl.InvokeCommand(pici);
        }

        int IContextMenu3.GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, out string pcszName, uint cchMax)
        {
            return _impl.GetCommandString(ref idcmd, uflags, ref pwReserved, out pcszName, cchMax);
        }

        int IContextMenu3.HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            return _impl.HandleMenuMsg(uMsg, wParam, lParam);
        }

        int IContextMenu3.HandleMenuMsg2(uint uMsg, IntPtr wParam, IntPtr lParam, out IntPtr plResult)
        {
            return _impl.HandleMenuMsg2(uMsg, wParam, lParam, out plResult);
        }

        int IContextMenu2.QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags)
        {
            return _impl.QueryContextMenu(hMenu, indexMenu, idCmdFirst, idCmdLast, uFlags);
        }

        int IContextMenu2.InvokeCommand(IntPtr pici)
        {
            return _impl.InvokeCommand(pici);
        }

        int IContextMenu2.GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, out string pcszName, uint cchMax)
        {
            return _impl.GetCommandString(ref idcmd, uflags, ref pwReserved, out pcszName, cchMax);
        }

        int IContextMenu2.HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            return _impl.HandleMenuMsg(uMsg, wParam, lParam);
        }

        int IContextMenu.QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags)
        {
            return _impl.QueryContextMenu(hMenu, indexMenu, idCmdFirst, idCmdLast, uFlags);
        }

        int IContextMenu.InvokeCommand(IntPtr pici)
        {
            return _impl.InvokeCommand(pici);
        }

        int IContextMenu.GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, out string pcszName, uint cchMax)
        {
            return _impl.GetCommandString(ref idcmd, uflags, ref pwReserved, out pcszName, cchMax);
        }

        int IPersistStream.GetClassID(out Guid pClassID)
        {
            pClassID = _deskbandGuid;
            return HRESULT.S_OK;
        }

        int IPersistStream.GetSizeMax(out ulong pcbSize)
        {
            return _impl.GetSizeMax(out pcbSize);
        }

        int IPersistStream.IsDirty()
        {
            return _impl.IsDirty();
        }

        int IPersistStream.Load(object pStm)
        {
            return _impl.Load(pStm);
        }

        int IPersistStream.Save(object pStm, bool fClearDirty)
        {
            return _impl.Save(pStm, fClearDirty);
        }

        int IPersist.GetClassID(out Guid pClassID)
        {
            pClassID = _deskbandGuid;
            return HRESULT.S_OK;
        }

        void IInputObject.UIActivateIO(int fActivate, ref MSG msg)
        {
            _impl.UIActivateIO(fActivate, ref msg);
        }

        int IInputObject.HasFocusIO()
        {
            return _impl.HasFocusIO();
        }

        int IInputObject.TranslateAcceleratorIO(ref MSG msg)
        {
            return _impl.TranslateAcceleratorIO(ref msg);
        }
    }
#pragma warning restore SA1202 // Elements must be ordered by access
#pragma warning restore SA1600 // Elements must be documented
}
