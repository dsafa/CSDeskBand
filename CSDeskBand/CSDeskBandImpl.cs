using System;
using System.Runtime.InteropServices;
using System.Linq;
using Microsoft.Win32;
using CSDeskBand.Interop;
using CSDeskBand.Interop.COM;
using static CSDeskBand.Interop.DESKBANDINFO.DBIM;
using static CSDeskBand.Interop.DESKBANDINFO.DBIMF;
using static CSDeskBand.Interop.DESKBANDINFO.DBIF;

namespace CSDeskBand
{
    /// <summary>
    /// Default implementation for icsdeskband
    /// </summary>
    public class CSDeskBandImpl : ICSDeskBand
    {
        public static readonly int S_OK = 0;
        public static readonly int E_NOTIMPL = unchecked((int)0x80004001);

        public EventHandler<VisibilityChangedEventArgs> VisibilityChanged;
        public EventHandler<TaskbarOrientationChangedEventArgs> TaskbarOrientationChanged;
        public EventHandler OnClose;

        private TaskbarOrientation _orientation = TaskbarOrientation.Horizontal;
        public TaskbarOrientation Orientation
        {
            get
            {
                return _orientation;
            }

            private set
            {
                if (value == _orientation)
                {
                    return;
                }

                _orientation = value;
                TaskbarOrientationChanged?.Invoke(this, new TaskbarOrientationChangedEventArgs { Orientation = value });
            }
        }

        public CSDeskBandOptions Options { get; }

        private IntPtr _handle;
        private IntPtr _parentWindowHandle;
        //Has these interfaces: IInputObjectSite, IOleWindow, IOleCommandTarget
        private object _parentSite;
        private uint _id;
        private static readonly Guid CATID_DESKBAND = new Guid("00021492-0000-0000-C000-000000000046");
        ///Command group id for deskband
        private Guid CGID_DeskBand = new Guid("EB0FE172-1A3A-11D0-89B3-00A0C90A90AC");

        public CSDeskBandImpl(IntPtr handle, CSDeskBandOptions options)
        {
            _handle = handle;
            Options = options;
            Options.PropertyChanged += Options_PropertyChanged;
        }

        private void Options_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_parentSite == null)
            {
                return;
            }

            var parent = (IOleCommandTarget) _parentSite;
            //Set pvaln to the id that was passed in setsite
            //When int is marshalled to variant, it is marshalled to VT_i4 see default marshalling for objects
            parent.Exec(ref CGID_DeskBand, (uint) tagDESKBANDCID.DBID_BANDINFOCHANGED, 0, _id, null);
        }

        public int GetWindow(out IntPtr phwnd)
        {
            phwnd = _handle;
            return S_OK;
        }

        public int ContextSensitiveHelp(bool fEnterMode)
        {
            return E_NOTIMPL;
        }

        public int ShowDW([In] bool fShow)
        {
            VisibilityChanged?.Invoke(this, new VisibilityChangedEventArgs { IsVisible = fShow });
            return S_OK;
        }

        public int CloseDW([In] uint dwReserved)
        {
            OnClose?.Invoke(this, null);
            return S_OK;
        }

        public int ResizeBorderDW(RECT prcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] IntPtr punkToolbarSite, bool fReserved)
        {
            //must return notimpl
            return E_NOTIMPL;
        }

        public int GetBandInfo(uint dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi)
        {
            _id = dwBandID;

            //Sizing information is requested whenever the taskbar changes size/orientation
            if (pdbi.dwMask.HasFlag(DBIM_MINSIZE))
            {
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptMinSize.Y = Options.MinVertical.Width;
                    pdbi.ptMinSize.X = Options.MinVertical.Height;
                }
                else
                {
                    pdbi.ptMinSize.X = Options.MinHorizontal.Width;
                    pdbi.ptMinSize.Y = Options.MinHorizontal.Height;
                }
            }

            if (pdbi.dwMask.HasFlag(DBIM_MAXSIZE))
            {
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptMaxSize.Y = Options.MaxVertical.Width;
                    pdbi.ptMaxSize.X = Options.MaxVertical.Height;
                }
                else
                {
                    pdbi.ptMaxSize.X = Options.MaxHorizontal.Width;
                    pdbi.ptMaxSize.Y = Options.MaxHorizontal.Height;
                }
            }

            // x member is ignored
            if (pdbi.dwMask.HasFlag(DBIM_INTEGRAL))
            {
                pdbi.ptIntegral.Y = Options.Increment;
                pdbi.ptIntegral.X = 0;
            }

            if (pdbi.dwMask.HasFlag(DBIM_ACTUAL))
            {
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptActual.Y = Options.Vertical.Width;
                    pdbi.ptActual.X = Options.Vertical.Height;
                }
                else
                {
                    pdbi.ptActual.X = Options.Horizontal.Width;
                    pdbi.ptActual.Y = Options.Horizontal.Height;
                }
            }

            if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
            {
                Orientation = TaskbarOrientation.Vertical;
            }
            else if (dwViewMode.HasFlag(DBIF_VIEWMODE_NORMAL))
            {
                Orientation = TaskbarOrientation.Horizontal;
            }

            if (pdbi.dwMask.HasFlag(DBIM_TITLE))
            {
                pdbi.wszTitle = Options.ShowTitle ? Options.Title : "";
            }

            if (pdbi.dwMask.HasFlag(DBIM_MODEFLAGS))
            {
                pdbi.dwModeFlags = DBIMF_NORMAL;
                pdbi.dwModeFlags |= Options.AlwaysShowGripper ? DBIMF_ALWAYSGRIPPER : 0;
                pdbi.dwModeFlags |= Options.Fixed ? DBIMF_FIXED | DBIMF_NOGRIPPER : 0;
                pdbi.dwModeFlags |= Options.NoMargins ? DBIMF_NOMARGINS : 0;
                pdbi.dwModeFlags |= Options.Sunken ? DBIMF_DEBOSSED : 0;
                pdbi.dwModeFlags |= Options.Undeleteable ? DBIMF_UNDELETEABLE : 0;
                pdbi.dwModeFlags |= Options.VariableHeight ? DBIMF_VARIABLEHEIGHT : 0;
                pdbi.dwModeFlags |= Options.AddToFront ? DBIMF_ADDTOFRONT : 0;
                pdbi.dwModeFlags |= Options.NewRow ? DBIMF_BREAK : 0;
                pdbi.dwModeFlags |= Options.TopRow ? DBIMF_TOPALIGN : 0;
                pdbi.dwModeFlags &= ~DBIMF_BKCOLOR;
            }

            return S_OK;
        }

        public int CanRenderComposited(out bool pfCanRenderComposited)
        {
            pfCanRenderComposited = true;
            return S_OK;
        }

        public int SetCompositionState(bool fCompositionEnabled)
        {
            return S_OK;
        }

        public int GetCompositionState(out bool pfCompositionEnabled)
        {
            pfCompositionEnabled = true;
            return S_OK;
        }

        public void SetSite([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkSite)
        {
            if (_parentSite != null)
            {
                Marshal.ReleaseComObject(_parentSite);
            }

            if (pUnkSite == null)
            {
                OnClose?.Invoke(this, null);
                return;
            }

            var oleWindow = (IOleWindow)pUnkSite;
            oleWindow.GetWindow(out _parentWindowHandle);
            User32.SetParent(_handle, _parentWindowHandle);

            _parentSite = pUnkSite;
        }

        public void GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvSite)
        {
            ppvSite = _parentSite;
        }

        [ComRegisterFunction]
        public static void Register(Type t)
        {
            string guid = t.GUID.ToString("B");
            RegistryKey rkClass = Registry.ClassesRoot.CreateSubKey($@"CLSID\{guid}");
            rkClass.SetValue(null, GetToolbarName(t));

            RegistryKey rkCat = rkClass.CreateSubKey("Implemented Categories");
            rkCat.CreateSubKey(CATID_DESKBAND.ToString("B"));

            Console.WriteLine($"Succesfully registered deskband {GetToolbarName(t)} - GUID: {guid}");
        }

        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            string guid = t.GUID.ToString("B");
            Registry.ClassesRoot.CreateSubKey(@"CLSID").DeleteSubKeyTree(guid);

            Console.WriteLine($"Successfully unregistered deskband {GetToolbarName(t)} - GUID: {guid}");
        }

        private static string GetToolbarName(Type t)
        {
            var registrationInfo = (CSDeskBandRegistrationAttribute[])t.GetCustomAttributes(typeof(CSDeskBandRegistrationAttribute), true);
            return registrationInfo.FirstOrDefault()?.Name ?? t.Name;
        }
    }
}