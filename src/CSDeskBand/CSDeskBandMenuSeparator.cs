using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSDeskBand.Interop;

namespace CSDeskBand
{
    /// <summary>
    /// A context menu seperator
    /// </summary>
    public class CSDeskBandMenuSeparator : CSDeskBandMenuItem
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
