using System;
using System.Collections.Generic;

namespace CSDeskBand.ContextMenu
{
    /// <summary>
    /// Base class for deskband menu items.
    /// </summary>
    public abstract class DeskBandMenuItem
    {
        // This is used instead of an interface so that the methods can be kept internal
        /// <summary>
        /// Add items to the menu
        /// </summary>
        /// <param name="menu">The menu to add items to</param>
        /// <param name="itemPosition">The position of the item to insert into the menu. Id should be incremented if item is inserted</param>
        /// <param name="itemId">Unique id of the menu item</param>
        /// <param name="callbacks">Dictionary of callbacks assigned to <see cref="itemId"/></param>
        internal abstract void AddToMenu(IntPtr menu, uint itemPosition, ref uint itemId, Dictionary<uint, DeskBandMenuAction> callbacks);
    }
}