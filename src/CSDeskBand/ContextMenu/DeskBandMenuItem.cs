namespace CSDeskBand.ContextMenu
{
    using CSDeskBand.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Base class for deskband menu items.
    /// </summary>
    public abstract class DeskBandMenuItem
    {
        /// <summary>
        /// Add this item to a menu.
        /// </summary>
        /// <param name="menu">The menu to add items to.</param>
        /// <param name="itemPosition">The position of the item to insert into the menu.</param>
        /// <param name="itemId">Unique id of the menu item. Should be incremented if used.</param>
        /// <param name="callbacks">Dictionary of callbacks assigned to a <paramref name="itemId"/>.</param>
        internal abstract void AddToMenu(IntPtr menu, uint itemPosition, ref uint itemId, Dictionary<uint, DeskBandMenuAction> callbacks);
    }

    /// <summary>
    /// A context menu seperator.
    /// </summary>
    internal sealed class DeskBandMenuSeparator : DeskBandMenuItem
    {
        private MENUITEMINFO _menuiteminfo;

        /// <inheritdoc/>
        internal override void AddToMenu(IntPtr menu, uint itemPosition, ref uint itemId, Dictionary<uint, DeskBandMenuAction> callbacks)
        {
            _menuiteminfo = new MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf<MENUITEMINFO>(),
                fMask = MENUITEMINFO.MIIM.MIIM_TYPE,
                fType = MENUITEMINFO.MFT.MFT_SEPARATOR,
            };

            User32.InsertMenuItem(menu, itemPosition, true, ref _menuiteminfo);
        }
    }

    /// <summary>
    /// A context menu item that can be clicked.
    /// </summary>
    internal sealed class DeskBandMenuAction : DeskBandMenuItem
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

    /// <summary>
    /// A sub menu item that can contain other <see cref="DeskBandMenuItem"/>.
    /// </summary>
    internal sealed class DeskBandMenu : DeskBandMenuItem
    {
        private IntPtr _menu;
        private MENUITEMINFO _menuiteminfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeskBandMenu"/> class
        /// with the display text.
        /// </summary>
        /// <param name="text">The text displayed for this item in the context menu.</param>
        public DeskBandMenu(string text)
            : this(text, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeskBandMenu"/> class
        /// with a display text and a list of submenu items.
        /// </summary>
        /// <param name="text">The text displayed for this item in the context menu.</param>
        /// <param name="items">A <see cref="IEnumerable{T}"/> of <see cref="DeskBandMenuItem"/> that will appear in this submenu.</param>
        public DeskBandMenu(string text, IEnumerable<DeskBandMenuItem> items)
        {
            Text = text;
            if (items != null)
            {
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DeskBandMenu"/> class.
        /// Frees up resoruces associated with the menu.
        /// </summary>
        ~DeskBandMenu()
        {
            ClearMenu();
        }

        /// <summary>
        /// Gets the collection of <see cref="DeskBandMenuItem"/> in the menu.
        /// </summary>
        public ICollection<DeskBandMenuItem> Items { get; } = new List<DeskBandMenuItem>();

        /// <summary>
        /// Gets or sets a value indicating whether the menu item is enabled.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the menu item can be interacted with. <see langword="false"/> to disable interactions.
        /// The default value is <see langword="true"/>;
        /// </value>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the text shown in the menu item.
        /// </summary>
        /// <value>
        /// The text that will be displayed for this menu item.
        /// </value>
        public string Text { get; set; }

        /// <inheritdoc/>
        internal override void AddToMenu(IntPtr menu, uint itemPosition, ref uint itemId, Dictionary<uint, DeskBandMenuAction> callbacks)
        {
            ClearMenu();

            _menu = User32.CreatePopupMenu();
            uint index = 0;
            foreach (var item in Items)
            {
                item.AddToMenu(_menu, index++, ref itemId, callbacks);
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

            User32.InsertMenuItem(menu, itemPosition, true, ref _menuiteminfo);
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
