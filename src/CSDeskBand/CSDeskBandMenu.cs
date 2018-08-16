using CSDeskBand.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSDeskBand
{
    public class CSDeskBandMenu : CSDeskBandMenuItem
    {
        public List<CSDeskBandMenuItem> Items { get; } = new List<CSDeskBandMenuItem>();
        public bool Enabled { get; set; } = true;
        public string Text { get; set; }

        private IntPtr _menu;
        private MENUITEMINFO _menuiteminfo;

        public CSDeskBandMenu(string text) : this(text, null) { }
        public CSDeskBandMenu(string text, IEnumerable<CSDeskBandMenuItem> items)
        {
            Text = text;
            if (items != null)
            {
                Items.AddRange(items);
            }
        }

        ~CSDeskBandMenu()
        {
            ClearMenu();
        }

        internal override void AddToMenu(IntPtr menu, uint pos, ref uint firstCmdId, Dictionary<uint, CSDeskBandMenuAction> callbacks)
        {
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
                fMask = MENUITEMINFO.MIIM.MIIM_SUBMENU | MENUITEMINFO.MIIM.MIIM_STRING | MENUITEMINFO.MIIM.MIIM_STATE,
                fType = MENUITEMINFO.MFT.MFT_MENUBREAK | MENUITEMINFO.MFT.MFT_STRING,
                fState = Enabled ? MENUITEMINFO.MFS.MFS_ENABLED : MENUITEMINFO.MFS.MFS_DISABLED,
                dwTypeData = Text,
                cch = (uint)Text.Length,
                hSubMenu = _menu,
            };

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
