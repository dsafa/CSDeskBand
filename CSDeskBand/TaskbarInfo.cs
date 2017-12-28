using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSDeskBand.Interop;
using System.Runtime.InteropServices;

namespace CSDeskBand
{
    public enum TaskbarOrientation
    {
        Vertical,
        Horizontal,
    }

    public enum Edge : uint
    {
        Left,
        Top,
        Right,
        Bottom,
    }

    public class TaskbarInfo
    {
        public TaskbarOrientation Orientation
        {
            get
            {
                return _orientation;
            }

            private set
            {
                if (value == _orientation)
                {
                    return;
                }

                _orientation = value;
                TaskbarOrientationChanged?.Invoke(this, new TaskbarOrientationChangedEventArgs { Orientation = value });
            }
        }

        public Edge Edge
        {
            get
            {
                return _edge;
            }

            private set
            {
                if (value == _edge)
                {
                    return;
                }

                _edge = value;
                TaskbarEdgeChanged?.Invoke(this, new TaskbarEdgeChangedEventArgs { Edge = value });
            }
        }

        public Size Size
        {
            get
            {
                return _size;
            }

            private set
            {
                if (value.Equals(_size))
                {
                    return;
                }

                _size = value;
                TaskbarSizeChanged?.Invoke(this, new TaskbarSizeChangedEventArgs { Size = value });
            }
        }

        public EventHandler<TaskbarOrientationChangedEventArgs> TaskbarOrientationChanged;
        public EventHandler<TaskbarEdgeChangedEventArgs> TaskbarEdgeChanged;
        public EventHandler<TaskbarSizeChangedEventArgs> TaskbarSizeChanged;

        private TaskbarOrientation _orientation = TaskbarOrientation.Horizontal;
        private Edge _edge = Edge.Bottom;
        private Size _size;

        internal TaskbarInfo()
        {
            UpdateInfo();
        }

        internal void UpdateInfo()
        {
            APPBARDATA data = new APPBARDATA();
            data.hWnd = IntPtr.Zero;
            data.cbSize = Marshal.SizeOf(typeof(APPBARDATA));
            var res = Shell32.SHAppBarMessage((uint)APPBARMESSAGE.ABM_GETTASKBARPOS, ref data);

            var rect = data.rc;
            Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            Edge = (Edge)data.uEdge;
            Orientation = (Edge == Edge.Bottom || Edge == Edge.Top) ? TaskbarOrientation.Horizontal : TaskbarOrientation.Vertical;
        }
    }
}
