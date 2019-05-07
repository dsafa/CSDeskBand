namespace CSDeskBand
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using CSDeskBand.Interop;
    using Microsoft.Win32;

    /// <summary>
    /// Helper class to register deskband.
    /// </summary>
    internal static class RegistrationHelper
    {
        /// <summary>
        /// Register the deskband.
        /// </summary>
        /// <param name="t">Type of the deskband.</param>
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

                    // https://www.pinvoke.net/default.aspx/Interfaces.ITrayDeskband
                    ITrayDeskband csdeskband = null;
                    try
                    {
                        Type trayDeskbandType = Type.GetTypeFromCLSID(new Guid("E6442437-6C68-4f52-94DD-2CFED267EFB9"));
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

                                if (csdeskband.DeskBandRegistrationChanged() == HRESULT.S_OK)
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

        /// <summary>
        /// Unregister the deskband.
        /// </summary>
        /// <param name="t">Type of the deskband.</param>
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

        /// <summary>
        /// Gets the name of the toolbar for the deskband.
        /// </summary>
        /// <param name="t">Type of the deskband.</param>
        /// <returns>The name of the toolbar.</returns>
        internal static string GetToolbarName(Type t)
        {
            return t.GetCustomAttribute<CSDeskBandRegistrationAttribute>(true)?.Name ?? t.Name;
        }

        /// <summary>
        /// Gets if the deskband should be shown after registration.
        /// </summary>
        /// <param name="t">Type of the deskband.</param>
        /// <returns>The value if it should be shown.</returns>
        internal static bool GetToolbarRequestToShow(Type t)
        {
            return t.GetCustomAttribute<CSDeskBandRegistrationAttribute>(true)?.ShowDeskBand ?? false;
        }
    }
}
