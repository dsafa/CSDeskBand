using System;

namespace CSDeskBand
{
    /// <summary>
    /// Provides data for a taskbar orientation change event.
    /// </summary>
    public sealed class TaskbarOrientationChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskbarOrientationChangedEventArgs"/> class
        /// with the new orientation.
        /// </summary>
        /// <param name="orientation">The new taskbar orientation.</param>
        public TaskbarOrientationChangedEventArgs(TaskbarOrientation orientation)
        {
            Orientation = orientation;
        }

        /// <summary>
        /// Gets the new orientation of the taskbar.
        /// </summary>
        public TaskbarOrientation Orientation { get; }
    }
}
