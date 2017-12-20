using System.Drawing;

namespace CSDeskband
{
    public class CSDeskBandOptions
    {
        /// <summary>
        /// If true, the Increment value can be used. Otherwise ignored.
        /// </summary>
        public bool VariableHeight { get; set; } = false;

        /// <summary>
        /// Step size for resizing
        /// </summary>
        public int Increment { get; set; } = 0;

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
        /// if true, Title is shown next to the deskband
        /// </summary>
        public bool ShowTitle { get; set; } = false;

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Min Size veritcally
        /// </summary>
        public Size MinVertical { get; set; } = new Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, 100);

        /// <summary>
        /// Max size vertically. int.MaxValue - 1 for no limit
        /// </summary>
        public Size MaxVertical { get; set; } = new Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, CSDeskBandImpl.NO_LIMIT);

        /// <summary>
        /// Ideal size vertically
        /// </summary>
        public Size Vertical { get; set; } = new Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, 100);

        /// <summary>
        /// Min size horizontal
        /// </summary>
        public Size MinHorizontal { get; set; } = new Size(100, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);

        /// <summary>
        /// Max size horizontal
        /// </summary>
        public Size MaxHorizontal { get; set; } = new Size(CSDeskBandImpl.NO_LIMIT, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);

        /// <summary>
        /// Ideal size horizontally
        /// </summary>
        public Size Horizontal { get; set; } = new Size(100, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);
    }
}