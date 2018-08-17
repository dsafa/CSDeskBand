using CSDeskBand.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSDeskBand
{
    /// <summary>
    /// A context menu item that can be clicked.
    /// </summary>
    public sealed class CSDeskBandMenuAction : CSDeskBandMenuItem
    {
        /// <summary>
        /// Determines if there is a checkmark next to the menu item.
        /// </summary>
        /// <value>
        /// True if the menu should have a checkmark. False if there should be no checkmark.
        /// The default value is false.
        /// </value>
        public bool Checked { get; set; } = false;

        /// <summary>
        /// Determines if the menu item is enabled.
        /// </summary>
        /// <value>
        /// True if the menu item can be interacted with. False to disable interactions.
        /// The default value is true;
        /// </value>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The text shown in the menu item.
        /// </summary>
        /// <value>
        /// The text that will be displayed for this menu item.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Occurs when the menu item has been clicked.
        /// </summary>
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