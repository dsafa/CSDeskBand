using System;

namespace CSDeskBand
{
    /// <summary>
    /// Provides data for a taskbar size change event.
    /// </summary>
    public sealed class TaskbarSizeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskbarSizeChangedEventArgs"/> class
        /// with the new size of the taskbar.
        /// </summary>
        /// <param name="size">The new size of the taskbar.</param>
        public TaskbarSizeChangedEventArgs(Size size)
        {
            Size = size;
        }

        /// <summary>
        /// Gets the new size of the taskbar.
        /// </summary>
        public Size Size { get; }
    }
}
