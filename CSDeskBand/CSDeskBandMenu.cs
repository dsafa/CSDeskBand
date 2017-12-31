using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSDeskBand.Interop;
using CSDeskBand.Logging;

namespace CSDeskBand
{
    public class CSDeskBandMenu : CSDeskBandMenuItem
    {
        public List<CSDeskBandMenuItem> Items { get; }

        private IntPtr _menu;
        private MENUITEMINFO _menuiteminfo;

        public CSDeskBandMenu(string text) : this(text, null) { }
        public CSDeskBandMenu(string text, IEnumerable<CSDeskBandMenuItem> items)
        {
            Text = text;
            if (items == null)
            {
                Items = new List<CSDeskBandMenuItem>();
            }
            else
            {
                Items.AddRange(items);
            }
        }

        ~CSDeskBandMenu()
        {
            ClearMenu();
        }

        internal override uint ItemCount
        {
            get => Items.Aggregate(0u, (total, menuItem) => total + menuItem.ItemCount) + 1;
        }

        internal override void AddToMenu(IntPtr menu, uint pos, ref uint firstCmdId, Dictionary<uint, CSDeskBandMenuAction> callbacks)
        {
            //Create a submenu and add the items to it
            ClearMenu();
            _menu = User32.CreatePopupMenu();
            uint index = 0;
            foreach (var item in Items)
            {
                item.AddToMenu(_menu, index++, ref firstCmdId, callbacks);
            }

            _menuiteminfo = new MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf<MENUITEMINFO>(),
                fMask = MENUITEMINFO.MIIM.MIIM_SUBMENU | MENUITEMINFO.MIIM.MIIM_STRING,
                fType = MENUITEMINFO.MFT.MFT_MENUBREAK | MENUITEMINFO.MFT.MFT_STRING,
                dwTypeData = Text,
                cch = (uint)Text.Length,
                hSubMenu = _menu,
            };

            //Add submenu to menu
            User32.InsertMenuItem(menu, pos, true, ref _menuiteminfo);
        }

        private void ClearMenu()
        {
            if (_menu != IntPtr.Zero)
            {
                User32.DestroyMenu(_menu);
            }
        }
    }
}
