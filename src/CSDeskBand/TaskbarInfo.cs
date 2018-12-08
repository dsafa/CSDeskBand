namespace CSDeskBand
{
    using System;
    using System.Runtime.InteropServices;
    using CSDeskBand.Interop;

    /// <summary>
    /// The orientation of the taskbar.
    /// </summary>
    public enum TaskbarOrientation
    {
        /// <summary>
        /// Vertical if the taskbar is either on top or bottom.
        /// </summary>
        Vertical,

        /// <summary>
        /// Horizontal if the taskbar is either on the left or right.
        /// </summary>
        Horizontal,
    }

    /// <summary>
    /// The edge where the taskbar is located.
    /// </summary>
    public enum Edge : uint
    {
        /// <summary>
        /// Taskbar is on the left edge.
        /// </summary>
        Left,

        /// <summary>
        /// Taskbar is on the top edge.
        /// </summary>
        Top,

        /// <summary>
        /// Taskbar is on the right edge.
        /// </summary>
        Right,

        /// <summary>
        /// Taskbar is on the bottom edge.
        /// </summary>
        Bottom,
    }

    /// <summary>
    /// Provides information about the main taskbar.
    /// </summary>
    public sealed class TaskbarInfo
    {
        private TaskbarOrientation _orientation = TaskbarOrientation.Horizontal;
        private Edge _edge = Edge.Bottom;
        private DeskBandSize _size;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskbarInfo"/> class.
        /// </summary>
        internal TaskbarInfo()
        {
            UpdateInfo();
        }

        /// <summary>
        /// Occurs when the orientation of the main taskbar is changed.
        /// </summary>
        public event EventHandler<TaskbarOrientationChangedEventArgs> TaskbarOrientationChanged;

        /// <summary>
        /// Occurs when the edge of the main taskbar is changed.
        /// </summary>
        public event EventHandler<TaskbarEdgeChangedEventArgs> TaskbarEdgeChanged;

        /// <summary>
        /// Occurs when the size of the taskbar is changed.
        /// </summary>
        public event EventHandler<TaskbarSizeChangedEventArgs> TaskbarSizeChanged;

        /// <summary>
        /// Gets the current <see cref="TaskbarOrientation"/> of the main taskbar.
        /// </summary>
        /// <value>
        /// The current orientation.
        /// </value>
        public TaskbarOrientation Orientation
        {
            get => _orientation;
            private set
            {
                if (value == _orientation)
                {
                    return;
                }

                _orientation = value;
                TaskbarOrientationChanged?.Invoke(this, new TaskbarOrientationChangedEventArgs(value));
            }
        }

        /// <summary>
        /// Gets the current <see cref="CSDeskBand.Edge"/> of the main taskbar.
        /// </summary>
        /// <value>
        /// The current edge.
        /// </value>
        public Edge Edge
        {
            get => _edge;
            private set
            {
                if (value == _edge)
                {
                    return;
                }

                _edge = value;
                TaskbarEdgeChanged?.Invoke(this, new TaskbarEdgeChangedEventArgs(value));
            }
        }

        /// <summary>
        /// Gets the current <see cref="CSDeskBand.DeskBandSize"/> of the main taskbar.
        /// </summary>
        /// <value>
        /// The current size.
        /// </value>
        public DeskBandSize Size
        {
            get => _size;
            private set
            {
                if (value.Equals(_size))
                {
                    return;
                }

                _size = value;
                TaskbarSizeChanged?.Invoke(this, new TaskbarSizeChangedEventArgs(value));
            }
        }

        /// <summary>
        /// Get the latest taskbar information.
        /// </summary>
        internal void UpdateInfo()
        {
            APPBARDATA data = new APPBARDATA
            {
                hWnd = IntPtr.Zero,
                cbSize = Marshal.SizeOf<APPBARDATA>()
            };

            var res = Shell32.SHAppBarMessage(APPBARMESSAGE.ABM_GETTASKBARPOS, ref data);
            var rect = data.rc;
            Size = new DeskBandSize(rect.right - rect.left, rect.bottom - rect.top);
            Edge = (Edge)data.uEdge;
            Orientation = (Edge == Edge.Bottom || Edge == Edge.Top) ? TaskbarOrientation.Horizontal : TaskbarOrientation.Vertical;
        }
    }

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
        public TaskbarSizeChangedEventArgs(DeskBandSize size)
        {
            Size = size;
        }

        /// <summary>
        /// Gets the new size of the taskbar.
        /// </summary>
        public DeskBandSize Size { get; }
    }

    /// <summary>
    /// Provides data for a taskbar edge change event.
    /// </summary>
    public sealed class TaskbarEdgeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskbarEdgeChangedEventArgs"/> class
        /// with the new edge.
        /// </summary>
        /// <param name="edge">The new edge.</param>
        public TaskbarEdgeChangedEventArgs(Edge edge)
        {
            Edge = edge;
        }

        /// <summary>
        /// Gets the new edge location of the taskbar.
        /// </summary>
        public Edge Edge { get; }
    }
}