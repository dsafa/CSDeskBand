using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSDeskBand.Interop;
using CSDeskBand.Logging;

namespace CSDeskBand.Win
{
    /// <summary>
    /// Winforms deskband. The deskband should inherit this class.
    /// The deskband should also have these attributes <see cref="ComVisibleAttribute"/>, <see cref="GuidAttribute"/>, <see cref="CSDeskBandRegistrationAttribute"/>.
    /// Look at Sample.Win for an example
    /// </summary>
    public class CSDeskBandWin: UserControl, ICSDeskBand
    {
        protected CSDeskBandOptions Options { get; } = new CSDeskBandOptions();
        protected TaskbarInfo TaskbarInfo { get; }

        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private readonly CSDeskBandImpl _impl;
        private readonly Guid _deskbandGuid;

        public CSDeskBandWin()
        {
            try
            {
                Options.Title = CSDeskBandImpl.GetToolbarName(GetType());

                _impl = new CSDeskBandImpl(Handle, Options);
                _impl.VisibilityChanged += VisibilityChanged;
                _impl.Closed += OnClose;

                TaskbarInfo = _impl.TaskbarInfo;
                SizeChanged += CSDeskBandWin_SizeChanged;

                //Empty guid is a workaround for winforms designer because there will be no guid attribute
                _deskbandGuid = new Guid(GetType().GetCustomAttribute<GuidAttribute>(true)?.Value ?? Guid.Empty.ToString("B"));
            }
            catch (Exception e)
            {
                _logger.DebugException("Initialization Error", e);
                throw;
            }
        }

        private void CSDeskBandWin_SizeChanged(object sender, EventArgs e)
        {
            if (TaskbarInfo.Orientation == TaskbarOrientation.Horizontal)
            {
                Options.Horizontal = Size;
            }
            else
            {
                Options.Vertical = Size;
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

        protected virtual void OnClose() {}

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

        protected void CloseDeskBand()
        {
            _impl.CloseDeskBand();
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

        public int GetClassID(out Guid pClassID)
        {
            pClassID = _deskbandGuid;
            return HRESULT.S_OK;
        }

        public int GetSizeMax(out ulong pcbSize)
        {
            return _impl.GetSizeMax(out pcbSize);
        }

        public int IsDirty()
        {
            return _impl.IsDirty();
        }

        public new int Load(object pStm)
        {
            return _impl.Load(pStm);
        }

        public int Save(object pStm, bool fClearDirty)
        {
            return _impl.Save(pStm, fClearDirty);
        }

        public void UIActivateIO(int fActivate, ref MSG msg)
        {
            _impl.UIActivateIO(fActivate, ref msg);
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