using System;

namespace CSDeskBand
{
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
