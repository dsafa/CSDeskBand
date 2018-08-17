using System;
using CSDeskBand.Interop;
using System.Runtime.InteropServices;
using CSDeskBand.Logging;

namespace CSDeskBand
{
    /// <summary>
    /// The orientation of the taskbar.
    /// </summary>
    public enum TaskbarOrientation
    {
        Vertical,
        Horizontal,
    }

    /// <summary>
    /// The edge where the taskbar is located.
    /// </summary>
    public enum Edge : uint
    {
        Left,
        Top,
        Right,
        Bottom,
    }

    /// <summary>
    /// Provides information about the main taskbar.
    /// </summary>
    public sealed class TaskbarInfo
    {
        /// <summary>
        /// Get the current <see cref="TaskbarOrientation"/> of the main taskbar.
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
                TaskbarOrientationChanged?.Invoke(this, new TaskbarOrientationChangedEventArgs { Orientation = value });
            }
        }

        /// <summary>
        /// Get the current <see cref="CSDeskBand.Edge"/> of the main taskbar.
        /// </summary>
        /// <value>
        /// The current edge.
        /// </value>
        public Edge Edge
        {
            get => _edge;
            private set
            {
                _logger.Debug($"Taskbar edge: {Enum.GetName(typeof(TaskbarOrientation), value)}");
                if (value == _edge)
                {
                    return;
                }

                _edge = value;
                TaskbarEdgeChanged?.Invoke(this, new TaskbarEdgeChangedEventArgs { Edge = value });
            }
        }

        /// <summary>
        /// Get the current <see cref="CSDeskBand.Size"/> of the main taskbar.
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
                TaskbarSizeChanged?.Invoke(this, new TaskbarSizeChangedEventArgs { Size = value });
            }
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

        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private TaskbarOrientation _orientation = TaskbarOrientation.Horizontal;
        private Edge _edge = Edge.Bottom;
        private Size _size;

        internal TaskbarInfo()
        {
            UpdateInfo();
        }

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
