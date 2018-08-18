using System;

namespace CSDeskBand
{
    /// <summary>
    /// Provides data for a visibility changed event.
    /// </summary>
    public sealed class VisibilityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Status of visibility.
        /// </summary>
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// Provides data for a taskbar orientation change event.
    /// </summary>
    public sealed class TaskbarOrientationChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Orientation of the taskbar.
        /// </summary>
        public TaskbarOrientation Orientation { get; set; }
    }

    /// <summary>
    /// Provides data for a taskbar size change event.
    /// </summary>
    public sealed class TaskbarSizeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Size of the taskbar.
        /// </summary>
        public Size Size { get; set; }
    }

    /// <summary>
    /// Provides data for a taskbar edge change event.
    /// </summary>
    public sealed class TaskbarEdgeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Edge location of the taskbar.
        /// </summary>
        public Edge Edge { get; set; }
    }
}