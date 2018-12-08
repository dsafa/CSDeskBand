using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CSDeskBand.Interop;

namespace CSDeskBand.ContextMenu
{
    /// <summary>
    /// A context menu item that can be clicked.
    /// </summary>
    public sealed class DeskBandMenuAction : DeskBandMenuItem
    {
        private MENUITEMINFO _menuiteminfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeskBandMenuAction"/> class
        /// with its display text.
        /// </summary>
        /// <param name="text">The text that is shown for this item in the context menu.</param>
        public DeskBandMenuAction(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Occurs when the menu item has been clicked.
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// Gets or sets a value indicating whether there is a checkmark next to the menu item.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the menu should have a checkmark. <see langword="false"/> if there should be no checkmark.
        /// The default value is <see langword="false"/>.
        /// </value>
        public bool Checked { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the menu item is enabled.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the menu item can be interacted with. <see langword="false"/> to disable interactions.
        /// The default value is <see langword="true"/>.
        /// </value>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the text shown for this item in the context menu.
        /// </summary>
        /// <value>
        /// The text that will be displayed for this item in the context menu.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Performs the click action for this item.
        /// </summary>
        internal void DoAction()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc/>
        internal override void AddToMenu(IntPtr menu, uint itemPosition, ref uint itemId, Dictionary<uint, DeskBandMenuAction> callbacks)
        {
            _menuiteminfo = new MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf<MENUITEMINFO>(),
                fMask = MENUITEMINFO.MIIM.MIIM_TYPE | MENUITEMINFO.MIIM.MIIM_STATE | MENUITEMINFO.MIIM.MIIM_ID,
                fType = MENUITEMINFO.MFT.MFT_STRING,
                dwTypeData = Text,
                cch = (uint)Text.Length,
                wID = itemId++,
            };

            _menuiteminfo.fState |= Enabled ? MENUITEMINFO.MFS.MFS_ENABLED : MENUITEMINFO.MFS.MFS_DISABLED;
            _menuiteminfo.fState |= Checked ? MENUITEMINFO.MFS.MFS_CHECKED : MENUITEMINFO.MFS.MFS_UNCHECKED;

            callbacks[_menuiteminfo.wID] = this;

            User32.InsertMenuItem(menu, itemPosition, true, ref _menuiteminfo);
        }
    }
}
