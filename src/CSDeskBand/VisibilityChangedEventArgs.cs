using System;

namespace CSDeskBand
{
    /// <summary>
    /// Provides data for a deskband visibility changed event.
    /// </summary>
    public sealed class VisibilityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisibilityChangedEventArgs"/> class.
        /// with a value indicating if the deskband is visible.
        /// </summary>
        /// <param name="isVisible">A value indicating if the deskband is visible.</param>
        public VisibilityChangedEventArgs(bool isVisible)
        {
            IsVisible = isVisible;
        }

        /// <summary>
        /// Gets a value indicating whether the deskband is visible.
        /// </summary>
        public bool IsVisible { get; }
    }
}
