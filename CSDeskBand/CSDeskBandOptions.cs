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

        private Size _horizontal;
        private Size _maxHorizontal;
        private Size _minHorizontal;
        private Size _vertical;
        private Size _maxVertical;
        private Size _minVertical;
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
        /// Min Size veritcally
        /// </summary>
        public Size MinVertical
        {
            get => _minVertical;
            set
            {
                if (value.Equals(_minVertical)) return;
                _minVertical = value;
                _minVertical.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Max size vertically. int.MaxValue - 1 for no limit
        /// </summary>
        public Size MaxVertical
        {
            get => _maxVertical;
            set
            {
                if (value.Equals(_maxVertical)) return;
                _maxVertical = value;
                _maxVertical.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ideal size vertically. Not guaranteed to be this size.
        /// </summary>
        public Size Vertical
        {
            get => _vertical;
            set
            {
                if (value.Equals(_vertical)) return;
                _vertical = value;
                _vertical.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Min size horizontal
        /// </summary>
        public Size MinHorizontal
        {
            get => _minHorizontal;
            set
            {
                if (value.Equals(_minHorizontal)) return;
                _minHorizontal = value;
                _minHorizontal.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Max size horizontal
        /// </summary>
        public Size MaxHorizontal
        {
            get => _maxHorizontal;
            set
            {
                if (value.Equals(_maxHorizontal)) return;
                _maxHorizontal = value;
                _maxHorizontal.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ideal size horizontally. Not guaranteed to be this size.
        /// </summary>
        public Size Horizontal
        {
            get => _horizontal;
            set
            {
                if (value.Equals(_horizontal)) return;
                _horizontal = value;
                _horizontal.PropertyChanged += (sender, args) => OnPropertyChanged();
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
            Horizontal = new Size(200, TaskbarHorizontalHeightLarge);
            MaxHorizontal = new Size(NoLimit, NoLimit);
            MinHorizontal = new Size(NoLimit, NoLimit);

            Vertical = new Size(TaskbarVerticalWidth, 200);
            MaxVertical = new Size(NoLimit, NoLimit);
            MinVertical = new Size(NoLimit, NoLimit);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}