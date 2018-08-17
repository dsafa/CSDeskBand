using CSDeskBand.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSDeskBand
{
    /// <summary>
    /// A context menu seperator.
    /// </summary>
    public sealed class CSDeskBandMenuSeparator : CSDeskBandMenuItem
    {
        private MENUITEMINFO _menuiteminfo;

        internal override void AddToMenu(IntPtr menu, uint pos, ref uint firstCmdId, Dictionary<uint, CSDeskBandMenuAction> callbacks)
        {
            _menuiteminfo = new MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf<MENUITEMINFO>(),
                fMask = MENUITEMINFO.MIIM.MIIM_TYPE,
                fType = MENUITEMINFO.MFT.MFT_SEPARATOR,
            };

            User32.InsertMenuItem(menu, pos, true, ref _menuiteminfo);
        }
    }
}
