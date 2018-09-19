﻿using CSDeskBand.Interop;
using CSDeskBand.Interop.COM;
using CSDeskBand.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using CSDeskBand.ContextMenu;
using static CSDeskBand.Interop.DESKBANDINFO.DBIF;
using static CSDeskBand.Interop.DESKBANDINFO.DBIM;
using static CSDeskBand.Interop.DESKBANDINFO.DBIMF;

namespace CSDeskBand
{
    /// <summary>
    /// Default implementation for icsdeskband
    /// </summary>
    internal sealed class CSDeskBandImpl : ICSDeskBand
    {
        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;
        public event EventHandler Closed;

        public CSDeskBandOptions Options { get; }
        public TaskbarInfo TaskbarInfo { get; } = new TaskbarInfo();

        private readonly IntPtr _handle;
        private IntPtr _parentWindowHandle;
        private object _parentSite; // Has these interfaces: IInputObjectSite, IOleWindow, IOleCommandTarget, IBandSite
        private uint _id;
        private uint _menutStartId = 0;
        private bool _isDirty = true;
        private Guid CGID_DeskBand = new Guid("EB0FE172-1A3A-11D0-89B3-00A0C90A90AC"); // Command group id for deskband. Used for IOleCommandTarge.Exec
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private readonly Dictionary<uint, DeskBandMenuAction> _contextMenuActions = new Dictionary<uint, DeskBandMenuAction>();

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
            _logger.Debug("Deskband options have changed");

            var parent = (IOleCommandTarget)_parentSite;
            // Set pvaln to the id that was passed in SetSite
            // When int is marshalled to variant, it is marshalled as VT_i4. See default marshalling for objects
            parent.Exec(ref CGID_DeskBand, (uint)tagDESKBANDCID.DBID_BANDINFOCHANGED, 0, IntPtr.Zero, IntPtr.Zero);
        }

        public int GetWindow(out IntPtr phwnd)
        {
            phwnd = _handle;
            return HRESULT.S_OK;
        }

        public int ContextSensitiveHelp(bool fEnterMode)
        {
            return HRESULT.E_NOTIMPL;
        }

        public int ShowDW([In] bool fShow)
        {
            VisibilityChanged?.Invoke(this, new VisibilityChangedEventArgs { IsVisible = fShow });
            return HRESULT.S_OK;
        }

        public int CloseDW([In] uint dwReserved)
        {
            Closed?.Invoke(this, null);
            return HRESULT.S_OK;
        }

        public int ResizeBorderDW(RECT prcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] IntPtr punkToolbarSite, bool fReserved)
        {
            //must return notimpl
            return HRESULT.E_NOTIMPL;
        }

        public int GetBandInfo(uint dwBandID, DESKBANDINFO.DBIF dwViewMode, ref DESKBANDINFO pdbi)
        {
            _id = dwBandID;

            // Sizing information is requested whenever the taskbar changes size/orientation
            if (pdbi.dwMask.HasFlag(DBIM_MINSIZE))
            {
                _logger.Debug("Deskband minsize requested");
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptMinSize.Y = Options.MinVerticalSize.Width;
                    pdbi.ptMinSize.X = Options.MinVerticalSize.Height;
                }
                else
                {
                    pdbi.ptMinSize.X = Options.MinHorizontalSize.Width;
                    pdbi.ptMinSize.Y = Options.MinHorizontalSize.Height;
                }
            }

            // X is ignored
            if (pdbi.dwMask.HasFlag(DBIM_MAXSIZE))
            {
                _logger.Debug("Deskband maxheight requested");
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptMaxSize.Y = Options.MaxVerticalWidth;
                    pdbi.ptMaxSize.X = 0;
                }
                else
                {
                    pdbi.ptMaxSize.X = 0;
                    pdbi.ptMaxSize.Y = Options.MaxHorizontalHeight;
                }
            }

            // x member is ignored
            if (pdbi.dwMask.HasFlag(DBIM_INTEGRAL))
            {
                _logger.Debug("Deskband height increment requested");
                pdbi.ptIntegral.Y = Options.HeightIncrement;
                pdbi.ptIntegral.X = 0;
            }

            if (pdbi.dwMask.HasFlag(DBIM_ACTUAL))
            {
                _logger.Debug("Deskband actual size requested");
                if (dwViewMode.HasFlag(DBIF_VIEWMODE_VERTICAL))
                {
                    pdbi.ptActual.Y = Options.VerticalSize.Width;
                    pdbi.ptActual.X = Options.VerticalSize.Height;
                }
                else
                {
                    pdbi.ptActual.X = Options.HorizontalSize.Width;
                    pdbi.ptActual.Y = Options.HorizontalSize.Height;
                }
            }

            if (pdbi.dwMask.HasFlag(DBIM_TITLE))
            {
                _logger.Debug("Deskband tile requested");
                pdbi.wszTitle = Options.Title;
                if (!Options.ShowTitle)
                {
                    pdbi.dwMask &= ~DBIM_TITLE;
                }
            }

            if (pdbi.dwMask.HasFlag(DBIM_MODEFLAGS))
            {
                pdbi.dwModeFlags = DBIMF_NORMAL;
                pdbi.dwModeFlags |= Options.IsFixed ? DBIMF_FIXED | DBIMF_NOGRIPPER : 0;
                pdbi.dwModeFlags |= Options.HeightCanChange ? DBIMF_VARIABLEHEIGHT : 0;
                pdbi.dwModeFlags &= ~DBIMF_BKCOLOR; //Don't use background color
            }

            TaskbarInfo.UpdateInfo();

            return HRESULT.S_OK;
        }

        public int CanRenderComposited(out bool pfCanRenderComposited)
        {
            pfCanRenderComposited = true;
            return HRESULT.S_OK;
        }

        public int SetCompositionState(bool fCompositionEnabled)
        {
            return HRESULT.S_OK;
        }

        public int GetCompositionState(out bool pfCompositionEnabled)
        {
            pfCompositionEnabled = true;
            return HRESULT.S_OK;
        }

        public void SetSite([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkSite)
        {
            if (_parentSite != null)
            {
                Marshal.ReleaseComObject(_parentSite);
            }

            //pUnkSite null means deskband was closed
            if (pUnkSite == null)
            {
                _logger.Debug("Closing deskband");
                Closed?.Invoke(this, null);
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
            var guid = t.GUID.ToString("B");
            try
            {
                var registryKey = Registry.ClassesRoot.CreateSubKey($@"CLSID\{guid}");
                registryKey.SetValue(null, GetToolbarName(t));

                var subKey = registryKey.CreateSubKey("Implemented Categories");
                subKey.CreateSubKey(ComponentCategoryManager.CATID_DESKBAND.ToString("B"));

                Console.WriteLine($"Succesfully registered deskband `{GetToolbarName(t)}` - GUID: {guid}");

                if (GetToolbarRequestToShow(t))
                {
                    Console.WriteLine($"Request to show deskband.");
                    ///https://www.pinvoke.net/default.aspx/Interfaces.ITrayDeskband
                    ITrayDeskband csdeskband = null;
                    try
                    {
                        Type trayDeskbandType = System.Type.GetTypeFromCLSID(new Guid("E6442437-6C68-4f52-94DD-2CFED267EFB9"));
                        Guid deskbandGuid = t.GUID;
                        csdeskband = (ITrayDeskband)Activator.CreateInstance(trayDeskbandType);
                        if (csdeskband != null)
                        {
                            csdeskband.DeskBandRegistrationChanged();

                            if (csdeskband.IsDeskBandShown(ref deskbandGuid) == HRESULT.S_FALSE)
                            {
                                if (csdeskband.ShowDeskBand(ref deskbandGuid) != HRESULT.S_OK)
                                {
                                    Console.WriteLine($"Error while trying to show deskband.");
                                }
                                if(csdeskband.DeskBandRegistrationChanged() == HRESULT.S_OK)
                                {
                                    Console.WriteLine($"The deskband was Succesfully shown with taskbar.{Environment.NewLine}You may see the alert notice box from explorer call.");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error while trying to show deskband: {e.ToString()}");
                    }
                    finally
                    {
                        if (csdeskband != null && Marshal.IsComObject(csdeskband))
                        {
                            Marshal.ReleaseComObject(csdeskband);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.Error.WriteLine($"Failed to register deskband `{GetToolbarName(t)}` - GUID: {guid}");
                throw;
            }
        }

        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            var guid = t.GUID.ToString("B");
            try
            {
                Registry.ClassesRoot.OpenSubKey(@"CLSID", true)?.DeleteSubKeyTree(guid);

                Console.WriteLine($"Successfully unregistered deskband `{GetToolbarName(t)}` - GUID: {guid}");
            }
            catch (ArgumentException)
            {
                Console.Error.WriteLine($"Deskband `{GetToolbarName(t)}` is not registered");
            }
            catch (Exception)
            {
                Console.Error.WriteLine($"Failed to unregister deskband `{GetToolbarName(t)}` - GUID: {guid}");
                throw;
            }
        }

        internal static string GetToolbarName(Type t)
        {
            return t.GetCustomAttribute<CSDeskBandRegistrationAttribute>(true)?.Name ?? t.Name;
        }

        internal static bool GetToolbarRequestToShow(Type t)
        {
            return t.GetCustomAttribute<CSDeskBandRegistrationAttribute>(true)?.ShowDeskBand ?? false;
        }

        public int QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags)
        {
            _logger.Debug($"Was queried for context menu - index: {indexMenu} cmd first: {idCmdFirst}, cmd last: {idCmdLast}, flags: {uFlags}");
            if (uFlags.HasFlag(QueryContextMenuFlags.CMF_DEFAULTONLY))
            {
                return HRESULT.MakeHResult((uint)HRESULT.S_OK, 0, 0);
            }

            _menutStartId = idCmdFirst;
            foreach (var item in Options.ContextMenuItems)
            {
                item.AddToMenu(hMenu, indexMenu++, ref idCmdFirst, _contextMenuActions);
            }

            return HRESULT.MakeHResult((uint)HRESULT.S_OK, 0, idCmdFirst + 1); // #id of last command + 1
        }

        public int InvokeCommand(IntPtr pici)
        {
            _logger.Debug("Invoking context menu action");

            var commandInfo = Marshal.PtrToStructure<CMINVOKECOMMANDINFO>(pici);
            var isUnicode = false;
            var isExtended = false;
            var verbPtr = commandInfo.lpVerb;

            if (commandInfo.cbSize == Marshal.SizeOf<CMINVOKECOMMANDINFOEX>())
            {
                isExtended = true;

                var extended = Marshal.PtrToStructure<CMINVOKECOMMANDINFOEX>(pici);
                if (extended.fMask.HasFlag(CMINVOKECOMMANDINFOEX.CMIC.CMIC_MASK_UNICODE))
                {
                    isUnicode = true;
                    verbPtr = extended.lpVerbW;
                }
            }

            if (User32.HiWord(commandInfo.lpVerb.ToInt32()) != 0)
            {
                //TODO verbs
                return HRESULT.E_FAIL;
            }

            var cmdIndex = User32.LoWord(verbPtr.ToInt32());

            if (!_contextMenuActions.TryGetValue((uint)cmdIndex + _menutStartId, out var action))
            {
                return HRESULT.E_FAIL;
            }

            action.DoAction();
            return HRESULT.S_OK;
        }

        public int GetCommandString(ref uint idcmd, uint uflags, ref uint pwReserved, out string pcszName, uint cchMax)
        {
            pcszName = "";
            return HRESULT.E_NOTIMPL;
        }

        public int HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            return HandleMenuMsg2(uMsg, wParam, lParam, out var i);
        }

        public int HandleMenuMsg2(uint uMsg, IntPtr wParam, IntPtr lParam, out IntPtr plResult)
        {
            plResult = IntPtr.Zero;
            return HRESULT.S_OK;
        }

        public int GetClassID(out Guid pClassID)
        {
            throw new NotImplementedException();
        }

        public int GetSizeMax(out ulong pcbSize)
        {
            pcbSize = 0;
            return HRESULT.E_NOTIMPL;
        }

        public int IsDirty()
        {
            return _isDirty ? HRESULT.S_OK : HRESULT.S_FALSE;
        }

        public int Load(object pStm)
        {
            return HRESULT.S_OK;
        }

        public int Save(object pStm, bool fClearDirty)
        {
            _isDirty = !fClearDirty;
            return HRESULT.S_OK;
        }

        public void CloseDeskBand()
        {
            var bandSite = (IBandSite)_parentSite;
            bandSite.RemoveBand(_id);
        }

        public void UIActivateIO(int fActivate, ref MSG msg)
        {
            //TODO
        }

        public int HasFocusIO()
        {
            return HRESULT.E_NOTIMPL;
        }

        public int TranslateAcceleratorIO(ref MSG msg)
        {
            return HRESULT.E_NOTIMPL;
        }
    }
}