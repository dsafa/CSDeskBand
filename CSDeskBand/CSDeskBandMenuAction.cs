using CSDeskBand.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSDeskBand
{
    /// <summary>
    /// A context menu item that can be clicked and invoke and action
    /// </summary>
    public class CSDeskBandMenuAction : CSDeskBandMenuItem
    {
        public bool Checked { get; set; } = false;
        public bool Enabled { get; set; } = true;
        public string Text { get; set; }
        public event EventHandler Clicked;

        private MENUITEMINFO _menuiteminfo;

        public CSDeskBandMenuAction(string text)
        {
            Text = text;
        }

        internal void DoAction()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        internal override void AddToMenu(IntPtr menu, uint pos, ref uint firstCmdId, Dictionary<uint, CSDeskBandMenuAction> callbacks)
        {
            _menuiteminfo = new MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf<MENUITEMINFO>(),
                fMask = MENUITEMINFO.MIIM.MIIM_TYPE | MENUITEMINFO.MIIM.MIIM_STATE | MENUITEMINFO.MIIM.MIIM_ID,
                fType = MENUITEMINFO.MFT.MFT_STRING,
                dwTypeData = Text,
                cch = (uint)Text.Length,
                wID = firstCmdId++,
            };

            _menuiteminfo.fState |= Enabled ? MENUITEMINFO.MFS.MFS_ENABLED : MENUITEMINFO.MFS.MFS_DISABLED;
            _menuiteminfo.fState |= Checked ? MENUITEMINFO.MFS.MFS_CHECKED : MENUITEMINFO.MFS.MFS_UNCHECKED;

            callbacks[_menuiteminfo.wID] = this;

            User32.InsertMenuItem(menu, pos, true, ref _menuiteminfo);
        }
    }
}