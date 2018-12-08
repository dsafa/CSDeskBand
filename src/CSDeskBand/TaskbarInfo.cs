using System;
using System.Runtime.InteropServices;
using CSDeskBand.Interop;
using CSDeskBand.Logging;

namespace CSDeskBand
{
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
        private static readonly ILog _logger = LogHelper.GetLogger(typeof(TaskbarInfo));
        private TaskbarOrientation _orientation = TaskbarOrientation.Horizontal;
        private Edge _edge = Edge.Bottom;
        private Size _size;

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
                _logger.Debug($"Taskbar orientation: {Enum.GetName(typeof(TaskbarOrientation), value)}");
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
                _logger.Debug($"Taskbar edge: {Enum.GetName(typeof(Edge), value)}");
                if (value == _edge)
                {
                    return;
                }

                _edge = value;
                TaskbarEdgeChanged?.Invoke(this, new TaskbarEdgeChangedEventArgs(value));
            }
        }

        /// <summary>
        /// Gets the current <see cref="CSDeskBand.Size"/> of the main taskbar.
        /// </summary>
        /// <value>
        /// The current size.
        /// </value>
        public Size Size
        {
            get => _size;
            private set
            {
                _logger.Debug($"Taskbar Size: width - {value.Width} height - {value.Height}");
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
            _logger.Debug("Getting taskbar information");

            APPBARDATA data = new APPBARDATA
            {
                hWnd = IntPtr.Zero,
                cbSize = Marshal.SizeOf<APPBARDATA>()
            };
            var res = Shell32.SHAppBarMessage(APPBARMESSAGE.ABM_GETTASKBARPOS, ref data);
            if (!Convert.ToBoolean((int)res))
            {
                _logger.Warn("Calling SHAppBarMessage failed");
            }

            var rect = data.rc;
            Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            Edge = (Edge)data.uEdge;
            Orientation = (Edge == Edge.Bottom || Edge == Edge.Top) ? TaskbarOrientation.Horizontal : TaskbarOrientation.Vertical;
        }
    }
}
