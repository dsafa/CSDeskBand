using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Drawing;
using Microsoft.Win32;
using CSDeskband.Interop;
using CSDeskband.Interop.COM;
using static CSDeskband.Interop.DESKBANDINFO.DBIM;
using static CSDeskband.Interop.DESKBANDINFO.DBIMF;
using static CSDeskband.Interop.DESKBANDINFO.DBIF;

namespace CSDeskband
{
    /// <summary>
    /// Default implementation for icsdeskband
    /// </summary>
    public class CSDeskBandImpl : ICSDeskBand
    {
        public static readonly int S_OK = 0;
        public static readonly int E_NOTIMPL = unchecked((int)0x80004001);
        public static readonly int TASKBAR_DEFAULT_LARGE = 40;
        public static readonly int TASKBAR_DEFAULT_SMALL = 30;
        public static readonly int NO_LIMIT = int.MaxValue - 1;

        public Size MinVertical { get; set; } = new Size(TASKBAR_DEFAULT_SMALL, 100);
        public Size MaxVertical { get; set; } = new Size(TASKBAR_DEFAULT_SMALL, 100);
        public Size Vertical { get; set; } = new Size(TASKBAR_DEFAULT_SMALL, 100);
        public Size MinHorizontal { get; set; } = new Size(100, TASKBAR_DEFAULT_SMALL);
        public Size MaxHorizontal { get; set; } = new Size(100, TASKBAR_DEFAULT_SMALL);
        public Size Horizontal { get; set; } = new Size(100, TASKBAR_DEFAULT_SMALL);
        public int Increment { get; set; } = NO_LIMIT;
        public string Title { get; set; } = "";
        public CSDeskBandOptions Options { get; set; } = new CSDeskBandOptions();

        private IntPtr _handle;
        private IInputObjectSite _site;
        
        private static readonly Guid CATID_DESKBAND = new Guid("00021492-0000-0000-C000-000000000046");

        public CSDeskBandImpl(IntPtr handle)
        {
            _handle = handle;
        }

        public int GetWindow(out IntPtr phwnd)
        {
            phwnd = _handle;
            return S_OK;
        }

        public int ContextSensitiveHelp(bool fEnterMode)
        {
            return S_OK;
        }

        public int ShowDW([In] bool fShow)
        {
            return S_OK;
        }

        public int CloseDW([In] uint dwReserved)
        {
            return S_OK;
        }

        public int ResizeBorderDW(RECT prcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] IntPtr punkToolbarSite, bool fReserved)
        {
            //must to return notimpl
            return E_NOTIMPL;
        }

        public int GetBandInfo(uint dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi)
        {
            if (pdbi.dwMask.HasFlag(DBIM_MINSIZE))
            {
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_FLOATING) || dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptMinSize.Y = MinVertical.Width;
                    pdbi.ptMinSize.X = MinVertical.Height;
                }
                else
                {
                    pdbi.ptMinSize.X = MinHorizontal.Width;
                    pdbi.ptMinSize.Y = MinHorizontal.Height;
                }
            }

            if (pdbi.dwMask.HasFlag(DBIM_MAXSIZE))
            {
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_FLOATING) || dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptMaxSize.Y = MaxVertical.Width;
                    pdbi.ptMaxSize.X = MaxVertical.Height;
                }
                else
                {
                    pdbi.ptMaxSize.X = MaxHorizontal.Width;
                    pdbi.ptMaxSize.Y = MaxHorizontal.Height;
                }
            }

            // x member is ignored
            if (pdbi.dwMask.HasFlag(DBIM_INTEGRAL))
            {
                pdbi.ptIntegral.Y = Increment;
                pdbi.ptIntegral.X = 0;
            }

            if (pdbi.dwMask.HasFlag(DBIM_ACTUAL))
            {
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_FLOATING) || dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptActual.Y = Vertical.Width;
                    pdbi.ptActual.X = Vertical.Height;
                }
                else
                {
                    pdbi.ptActual.X = Vertical.Width;
                    pdbi.ptActual.Y = Vertical.Height;
                }
            }

            if (pdbi.dwMask.HasFlag(DBIM_TITLE))
            {
                pdbi.wszTitle = Title;
            }

            pdbi.dwMask = pdbi.dwMask & ~DBIM_BKCOLOR | DBIM_TITLE;

            if (pdbi.dwMask.HasFlag(DBIM_MODEFLAGS))
            {
                pdbi.dwModeFlags = DBIMF_NORMAL;
                pdbi.dwModeFlags |= Options.AlwaysShowGripper ? DBIMF_ALWAYSGRIPPER : 0;
                pdbi.dwModeFlags |= Options.Fixed ? DBIMF_FIXED | DBIMF_NOGRIPPER : 0;
                pdbi.dwModeFlags |= Options.NoMargins ? DBIMF_NOMARGINS : 0;
                pdbi.dwModeFlags |= Options.Sunken ? DBIMF_DEBOSSED : 0;
                pdbi.dwModeFlags |= Options.Undeleteable ? DBIMF_UNDELETEABLE : 0;
                pdbi.dwModeFlags |= Options.VariableHeight ? DBIMF_VARIABLEHEIGHT : 0;
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
            if (_site != null)
            {
                Marshal.ReleaseComObject(_site);
            }

            _site = (IInputObjectSite)pUnkSite;
        }

        public void GetSite(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvSite)
        {
            ppvSite = _site;
        }

        [ComRegisterFunction]
        private static void Register(Type t)
        {
            string guid = t.GUID.ToString("B");
            RegistryKey rkClass = Registry.ClassesRoot.CreateSubKey($@"CLSID\{guid}");
            rkClass.SetValue(null, GetToolbarName(t));

            RegistryKey rkCat = rkClass.CreateSubKey("Implemented Categories");
            rkCat.CreateSubKey(CATID_DESKBAND.ToString("B"));

            Console.WriteLine($"Succesfully registered deskband {GetToolbarName(t)} - GUID: {guid}");
        }

        [ComUnregisterFunction]
        private static void Unregister(Type t)
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
