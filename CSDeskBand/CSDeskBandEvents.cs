using System;

namespace CSDeskBand
{
    public class VisibilityChangedEventArgs : EventArgs
    {
        public bool IsVisible { get; set; }
    }

    public class TaskbarOrientationChangedEventArgs : EventArgs
    {
        public TaskbarOrientation Orientation { get; set; }
    }

    public class TaskbarSizeChangedEventArgs : EventArgs
    {
        public Size Size { get; set; }
    }

    public class TaskbarEdgeChangedEventArgs : EventArgs
    {
        public Edge Edge { get; set; }
    }
}