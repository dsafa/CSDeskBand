#pragma warning disable 1591
#if DESKBAND_WPF
namespace CSDeskBand
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Forms.Integration;
    using CSDeskBand.Interop;

    /// <summary>
    /// Wpf implementation of <see cref="ICSDeskBand"/>
    /// The deskband should also have these attributes <see cref="ComVisibleAttribute"/>, <see cref="GuidAttribute"/>, <see cref="CSDeskBandRegistrationAttribute"/>.
    /// </summary>
    public abstract class CSDeskBandWpf : ICSDeskBand, IDeskBandProvider
    {
        private readonly CSDeskBandImpl _impl;
        private readonly CSDeskBandWpfHost _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSDeskBandWpf"/> class.
        /// </summary>
        public CSDeskBandWpf()
        {
            Options.Title = RegistrationHelper.GetToolbarName(GetType());
            _host = new CSDeskBandWpfHost(UIElement);
            _impl = new CSDeskBandImpl(this);
            _impl.Closed += (o, e) => DeskbandOnClosed();
            Guid = new Guid(GetType().GetCustomAttribute<GuidAttribute>(true)?.Value ?? Guid.Empty.ToString("B"));
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
        /// Gets main UI element for the deskband.
        /// </summary>
        protected abstract System.Windows.UIElement UIElement { get; }

        /// <summary>
        /// Gets the options for this deskband.
        /// </summary>
        /// <seealso cref="CSDeskBandOptions"/>
        public CSDeskBandOptions Options { get; } = new CSDeskBandOptions();

        /// <summary>
        /// Gets the handle
        /// </summary>
        public IntPtr Handle => _host.Handle;

        /// <summary>
        /// Gets the deskband guid
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// Handle closing of the deskband.
        /// </summary>
        protected virtual void DeskbandOnClosed()
        {
        }

#if DESKBAND_WPF_TRANSPARENCY
        // Requires reference to WindowsFormsIntegration.dll

        private class CSDeskBandWpfHost : Form
        {
            private ElementHost _host;

            public CSDeskBandWpfHost(System.Windows.UIElement uiElement)
            {
                FormBorderStyle = FormBorderStyle.None;
                AllowTransparency = true;
                TransparencyKey = System.Drawing.Color.Black;
                BackColor = System.Drawing.Color.Black;

                _host = new ElementHost
                {
                    Child = uiElement,
                    AutoSize = true,
                    Dock = DockStyle.Fill, // This is required or else it will crash
                    BackColorTransparent = true
                };

                Controls.Add(_host);
            }
        }

        /// <summary>
        /// Determines if transparency is enabled. Note this is color key transparency.
        /// Use <see cref="TransparencyColorKey"/> so set the color key.
        /// </summary>
        public bool TransparencyEnabled
        {
            get => _host.AllowTransparency;
            set => _host.AllowTransparency = value;
        }

        /// <summary>
        /// Color to be used for transparency.
        /// </summary>
        public System.Windows.Media.Color TransparencyColorKey
        {
            get => ToWpfColor(_host.TransparencyKey);
            set
            {
                _host.TransparencyKey = ToWinColor(value);
                _host.BackColor = ToWinColor(value);
            }
        }

        public static System.Windows.Media.Color ToWpfColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static System.Drawing.Color ToWinColor(System.Windows.Media.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
#else
        private class CSDeskBandWpfHost : ElementHost
        {
            public CSDeskBandWpfHost(System.Windows.UIElement uiElement)
            {
                Child = uiElement;
                AutoSize = true;
                Dock = DockStyle.Fill;
                BackColorTransparent = true;
            }
        }
#endif

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

        public int Load([In, MarshalAs(UnmanagedType.IUnknown)] object pStm)
        {
            return _impl.Load(pStm);
        }

        public int Save([In, MarshalAs(UnmanagedType.IUnknown)] object pStm, bool fClearDirty)
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
#endif