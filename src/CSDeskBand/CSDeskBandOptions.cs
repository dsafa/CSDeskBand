using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CSDeskBand.Annotations;

namespace CSDeskBand
{
    /// <summary>
    /// Options for the deskband
    /// </summary>
    public class CSDeskBandOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Height for a default horizontal taskbar
        /// </summary>
        public static readonly int TaskbarHorizontalHeightLarge = 40;

        /// <summary>
        /// Height for a default horizontal taskbar with small icons
        /// </summary>
        public static readonly int TaskbarHorizontalHeightSmall = 30;

        /// <summary>
        /// Width for a default vertical taskbar. There is no small vertical taskbar
        /// </summary>
        public static readonly int TaskbarVerticalWidth = 62;

        /// <summary>
        /// No limit for deskband size
        /// </summary>
        public static readonly int NoLimit = -1;

        private Size _horizontalSize;
        private int _maxHorizontalHeight;
        private Size _minHorizontalSize;
        private Size _verticalSize;
        private int _maxVerticalWidth;
        private Size _minVerticalSize;
        private bool _newRow = false;
        private bool _addToFront = false;
        private bool _topRow = false;
        private string _title = "";
        private bool _showTitle = false;
        private bool _noMargins = true;
        private bool _alwaysShowGripper = false;
        private bool _undeleteable = false;
        private bool _fixed = false;
        private bool _sunken = false;
        private int _increment = 1;
        private bool _variableHeight = true;
        private List<CSDeskBandMenuItem> _contextMenuItems = new List<CSDeskBandMenuItem>();

        /// <summary>
        /// The deskband will use up as much space as possible following the contraints set by max and min size
        /// </summary>
        public bool VariableHeight
        {
            get => _variableHeight;
            set
            {
                if (value == _variableHeight) return;
                _variableHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Height step size when deskband is being resized. The deskband will only be resized to multiples of this value
        /// </summary>
        /// <example>
        /// If increment is 50, then the height of the deskband can only be 50, 100 ...
        /// </example>
        public int Increment
        {
            get => _increment;
            set
            {
                if (value == _increment) return;
                _increment = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gives the deskband a sunken appearance
        /// </summary>
        public bool Sunken
        {
            get => _sunken;
            set
            {
                if (value == _sunken) return;
                _sunken = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// If the deskband is fixed, the gripper is not shown when taskbar is editable
        /// </summary>
        public bool Fixed
        {
            get => _fixed;
            set
            {
                if (value == _fixed) return;
                _fixed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Deskband cannot be removed
        /// </summary>
        public bool Undeleteable
        {
            get => _undeleteable;
            set
            {
                if (value == _undeleteable) return;
                _undeleteable = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Always show gripper even when taskbar isnt editable
        /// </summary>
        public bool AlwaysShowGripper
        {
            get => _alwaysShowGripper;
            set
            {
                if (value == _alwaysShowGripper) return;
                _alwaysShowGripper = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The deskband does not display margins
        /// </summary>
        public bool NoMargins
        {
            get => _noMargins;
            set
            {
                if (value == _noMargins) return;
                _noMargins = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// If true, <see cref="Title"/> is shown next to the deskband
        /// </summary>
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                if (value == _showTitle) return;
                _showTitle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Title of the deskband
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Band will be displayed in the top row
        /// </summary>
        public bool TopRow
        {
            get => _topRow;
            set
            {
                if (value == _topRow) return;
                _topRow = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Band will be added to the front
        /// </summary>
        public bool AddToFront
        {
            get => _addToFront;
            set
            {
                if (value == _addToFront) return;
                _addToFront = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Band will be displayed in a new row
        /// </summary>
        public bool NewRow
        {
            get => _newRow;
            set
            {
                if (value == _newRow) return;
                _newRow = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Mininum <see cref="Size"/> of the deskband in the vertical orientation.
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        public Size MinVerticalSize
        {
            get => _minVerticalSize;
            set
            {
                if (value.Equals(_minVerticalSize)) return;
                _minVerticalSize = value;
                _minVerticalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Maximum width of the deskband in the vertical orientation
        /// </summary>
        /// <remarks>
        /// The maximum height will have to be addressed in your code as there is no limit to the height of the deskband when vertical
        /// </remarks>
        /// <seealso cref="TaskbarOrientation"/>
        public int MaxVerticalWidth
        {
            get => _maxVerticalWidth;
            set
            {
                if (value.Equals(_maxVerticalWidth)) return;
                _maxVerticalWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ideal <see cref="Size"/> of the deskband in the vertical orientation. There is no guarantee that the deskband will be this size
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        public Size VerticalSize
        {
            get => _verticalSize;
            set
            {
                if (value.Equals(_verticalSize)) return;
                _verticalSize = value;
                _verticalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Minimum <see cref="Size"/> of the deskband in the horizontal orientation
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        public Size MinHorizontalSize
        {
            get => _minHorizontalSize;
            set
            {
                if (value.Equals(_minHorizontalSize)) return;
                _minHorizontalSize = value;
                _minHorizontalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Maximum height of the deskband in the horizontal orientation
        /// </summary>
        /// <remarks>
        /// The maximum width will have to be addressed in your code as there is no limit to the width of the deskband when horizontal
        /// </remarks>
        /// <seealso cref="TaskbarOrientation"/>
        public int MaxHorizontalHeight
        {
            get => _maxHorizontalHeight;
            set
            {
                if (value.Equals(_maxHorizontalHeight)) return;
                _maxHorizontalHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ideal <see cref="Size"/> of the deskband in the horizontal orientation. There is no guarantee that the deskband will be this size
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        public Size HorizontalSize
        {
            get => _horizontalSize;
            set
            {
                if (value.Equals(_horizontalSize)) return;
                _horizontalSize = value;
                _horizontalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Context Menu
        /// </summary>
        public List<CSDeskBandMenuItem> ContextMenuItems
        {
            get => _contextMenuItems;
            set
            {
                if (Equals(value, _contextMenuItems)) return;
                _contextMenuItems = value;
                OnPropertyChanged();
            }
        }

        public CSDeskBandOptions()
        {
            //initialize in constructor to hook up property change events
            HorizontalSize = new Size(200, TaskbarHorizontalHeightLarge);
            MaxHorizontalHeight = NoLimit;
            MinHorizontalSize = new Size(NoLimit, NoLimit);

            VerticalSize = new Size(TaskbarVerticalWidth, 200);
            MaxVerticalWidth = NoLimit;
            MinVerticalSize = new Size(NoLimit, NoLimit);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}