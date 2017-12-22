using System.Drawing;

namespace CSDeskBand
{
    public class CSDeskBandOptions
    {
        /// <summary>
        /// The deskband will use up as much space as possible following the contraints set by max and min size
        /// </summary>
        public bool VariableHeight { get; set; } = true;

        /// <summary>
        /// Height step size when deskband is being resized. The deskband will only be resized to multiples of this value
        /// </summary>
        /// <example>
        /// If increment is 50, then the height of the deskband can only be 50, 100 ...
        /// </example>
        public int Increment { get; set; } = 1;

        /// <summary>
        /// Gives the deskband a sunken appearance
        /// </summary>
        public bool Sunken { get; set; } = false;

        /// <summary>
        /// If the deskband is fixed, the gripper is not shown when taskbar is editable
        /// </summary>
        public bool Fixed { get; set; } = false;

        /// <summary>
        /// Deskband cannot be removed
        /// </summary>
        public bool Undeleteable { get; set; } = false;

        /// <summary>
        /// Always show gripper even when taskbar isnt editable
        /// </summary>
        public bool AlwaysShowGripper { get; set; } = false;

        /// <summary>
        /// The deskband does not display margins
        /// </summary>
        public bool NoMargins { get; set; } = true;

        /// <summary>
        /// If true, <see cref="Title"/> is shown next to the deskband
        /// </summary>
        public bool ShowTitle { get; set; } = false;

        /// <summary>
        /// Title of the deskband
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Band will be displayed in the top row
        /// </summary>
        public bool TopRow { get; set; } = false;

        /// <summary>
        /// Band will be added to the front
        /// </summary>
        public bool AddToFront { get; set; } = false;

        /// <summary>
        /// Band will be displayed in a new row
        /// </summary>
        public bool NewRow { get; set; } = false;

        /// <summary>
        /// Min Size veritcally
        /// </summary>
        public Size MinVertical { get; set; } = new Size(CSDeskBandImpl.TASKBAR_DEFAULT_SMALL, 200);

        /// <summary>
        /// Max size vertically. int.MaxValue - 1 for no limit
        /// </summary>
        public Size MaxVertical { get; set; } = new Size(CSDeskBandImpl.TASKBAR_DEFAULT_LARGE, CSDeskBandImpl.NO_LIMIT);

        /// <summary>
        /// Ideal size vertically. Not guaranteed to be this size.
        /// </summary>
        public Size Vertical { get; set; } = new Size(CSDeskBandImpl.TASKBAR_DEFAULT_LARGE, 200);

        /// <summary>
        /// Min size horizontal
        /// </summary>
        public Size MinHorizontal { get; set; } = new Size(200, CSDeskBandImpl.TASKBAR_DEFAULT_SMALL);

        /// <summary>
        /// Max size horizontal
        /// </summary>
        public Size MaxHorizontal { get; set; } = new Size(CSDeskBandImpl.NO_LIMIT, CSDeskBandImpl.TASKBAR_DEFAULT_LARGE);

        /// <summary>
        /// Ideal size horizontally. Not guaranteed to be this size.
        /// </summary>
        public Size Horizontal { get; set; } = new Size(200, CSDeskBandImpl.TASKBAR_DEFAULT_LARGE);
    }
}