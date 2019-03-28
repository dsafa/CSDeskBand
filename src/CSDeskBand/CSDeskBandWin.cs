#pragma warning disable 1591
#if DESKBAND_WINFORMS
namespace CSDeskBand
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using CSDeskBand.Interop;

    /// <summary>
    /// Winforms implementation of <see cref="ICSDeskBand"/>.
    /// The deskband should also have these attributes <see cref="ComVisibleAttribute"/>, <see cref="GuidAttribute"/>, <see cref="CSDeskBandRegistrationAttribute"/>.
    /// </summary>
    public abstract class CSDeskBandWin : ICSDeskBand, IDeskBandProvider
    {
        private readonly CSDeskBandImpl _impl;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSDeskBandWin"/> class.
        /// </summary>
        public CSDeskBandWin()
        {
            Options.Title = RegistrationHelper.GetToolbarName(GetType());
            _impl = new CSDeskBandImpl(this);
            _impl.Closed += (o, e) => DeskbandOnClosed();
            TaskbarInfo = _impl.TaskbarInfo;
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

        /// <summary>
        /// Gets the taskbar information
        /// </summary>
        protected TaskbarInfo TaskbarInfo { get; }

        /// <summary>
        /// Gets the main control for the deskband.
        /// </summary>
        protected abstract Control Control { get; }

        /// <summary>
        /// Gets the options for this deskband.
        /// </summary>
        /// <seealso cref="CSDeskBandOptions"/>
        public CSDeskBandOptions Options { get; } = new CSDeskBandOptions();

        /// <summary>
        /// Gets the handle
        /// </summary>
        public IntPtr Handle => Control.Handle;

        /// <summary>
        /// Gets the deskband guid
        /// </summary>
        public Guid Guid => GetType().GUID;

        public bool HasFocus
        {
            get => Control?.ContainsFocus ?? false;
            set
            {
                if (value)
                {
                    Control?.Focus();
                }
            }
        }

        /// <summary>
        /// Updates the focus on this deskband.
        /// </summary>
        /// <param name="focused"><see langword="true"/> if focused.</param>
        public void UpdateFocus(bool focused)
        {
            _impl.UpdateFocus(focused);
        }

        /// <summary>
        /// Handle closing of the deskband.
        /// </summary>
        protected virtual void DeskbandOnClosed()
        {
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

        public int SetSite([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkSite)
        {
            return _impl.SetSite(pUnkSite);
        }

        public int GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out IntPtr ppvSite)
        {
            return _impl.GetSite(ref riid, out ppvSite);
        }

        public int QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags)
        {
            return _impl.QueryContextMenu(hMenu, indexMenu, idCmdFirst, idCmdLast, uFlags);
        }

        public int InvokeCommand(IntPtr pici)
        {
            return _impl.InvokeCommand(pici);
        }

        public int GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, [MarshalAs(UnmanagedType.LPTStr)] out string pcszName, uint cchMax)
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

        public int GetClassID(out Guid pClassID)
        {
            return _impl.GetClassID(out pClassID);
        }

        public int GetSizeMax(out ulong pcbSize)
        {
            return _impl.GetSizeMax(out pcbSize);
        }

        public int IsDirty()
        {
            return _impl.IsDirty();
        }

        public int Load(object pStm)
        {
            return _impl.Load(pStm);
        }

        public int Save(IntPtr pStm, bool fClearDirty)
        {
            return _impl.Save(pStm, fClearDirty);
        }

        public int UIActivateIO(int fActivate, ref MSG msg)
        {
            return _impl.UIActivateIO(fActivate, ref msg);
        }

        public int HasFocusIO()
        {
            return _impl.HasFocusIO();
        }

        public int TranslateAcceleratorIO(ref MSG msg)
        {
            return _impl.TranslateAcceleratorIO(ref msg);
        }
    }
}
#endif