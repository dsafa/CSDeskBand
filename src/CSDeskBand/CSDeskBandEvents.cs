using System;

namespace CSDeskBand
{
    public sealed class VisibilityChangedEventArgs : EventArgs
    {
        public bool IsVisible { get; set; }
    }

    public sealed class TaskbarOrientationChangedEventArgs : EventArgs
    {
        public TaskbarOrientation Orientation { get; set; }
    }

    public sealed class TaskbarSizeChangedEventArgs : EventArgs
    {
        public Size Size { get; set; }
    }

    public sealed class TaskbarEdgeChangedEventArgs : EventArgs
    {
        public Edge Edge { get; set; }
    }
}