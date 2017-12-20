using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDeskband
{
    public class CSDeskBandOptions
    {
        /// <summary>
        /// If true, the <see cref="ICSDeskBand.Increment"/> value can be used. Otherwise ignored.
        /// </summary>
        public bool VariableHeight { get; set; } = false;

        /// <summary>
        /// Gives the deskband a sunked border
        /// </summary>
        public bool Sunken { get; set; } = false;

        /// <summary>
        /// If the deskband is fixed, the gripper is not shown when taskbar is editable
        /// </summary>
        public bool Fixed { get; set; } = false;

        /// <summary>
        /// Undeletable
        /// </summary>
        public bool Undeleteable { get; set; } = false;

        /// <summary>
        /// Always show gripper even when taskbar isnt editable
        /// </summary>
        public bool AlwaysShowGripper { get; set; } = false;

        /// <summary>
        /// No margins
        /// </summary>
        public bool NoMargins { get; set; } = false;

        /// <summary>
        /// if true, <see cref="ICSDeskBand.Title"/> is shown next to the deskband
        /// </summary>
        public bool ShowTitle { get; set; } = false;
    }
}