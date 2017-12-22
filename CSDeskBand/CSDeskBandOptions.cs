using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CSDeskBand.Annotations;

namespace CSDeskBand
{
    public class CSDeskBandOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Height for a default horizontal taskbar
        /// </summary>
        public static readonly int TASKBAR_HORIZONTAL_HEIGHT_LARGE = 40;

        /// <summary>
        /// Height for a default horizontal taskbar with small icons
        /// </summary>
        public static readonly int TASKBAR_HORIZONTAL_HEIGHT_SMALL = 30;

        /// <summary>
        /// Width for a default vertical taskbar. Small taskbar icons don't change the size
        /// </summary>
        public static readonly int TASKBAR_VERTICAL_WIDTH = 62;

        /// <summary>
        /// No limit for deskband size
        /// </summary>
        public static readonly int NO_LIMIT = Int32.MaxValue - 1;

        private Size _horizontal = new Size(200, TASKBAR_HORIZONTAL_HEIGHT_LARGE);
        private Size _maxHorizontal = new Size(NO_LIMIT, TASKBAR_HORIZONTAL_HEIGHT_LARGE);
        private Size _minHorizontal = new Size(200, TASKBAR_HORIZONTAL_HEIGHT_SMALL);
        private Size _vertical = new Size(TASKBAR_HORIZONTAL_HEIGHT_LARGE, 200);
        private Size _maxVertical = new Size(TASKBAR_HORIZONTAL_HEIGHT_LARGE, NO_LIMIT);
        private Size _minVertical = new Size(TASKBAR_HORIZONTAL_HEIGHT_SMALL, 200);
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

        /// <summary>
        /// The deskband will use up as much space as possible following the contraints set by max and min size
        /// </summary>
        public bool VariableHeight
        {
            get { return _variableHeight; }
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
            get { return _increment; }
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
            get { return _sunken; }
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
            get { return _fixed; }
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
            get { return _undeleteable; }
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
            get { return _alwaysShowGripper; }
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
            get { return _noMargins; }
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
            get { return _showTitle; }
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
            get { return _title; }
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
            get { return _topRow; }
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
            get { return _addToFront; }
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
            get { return _newRow; }
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
            get { return _minVertical; }
            set
            {
                if (value.Equals(_minVertical)) return;
                _minVertical = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Max size vertically. int.MaxValue - 1 for no limit
        /// </summary>
        public Size MaxVertical
        {
            get { return _maxVertical; }
            set
            {
                if (value.Equals(_maxVertical)) return;
                _maxVertical = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ideal size vertically. Not guaranteed to be this size.
        /// </summary>
        public Size Vertical
        {
            get { return _vertical; }
            set
            {
                if (value.Equals(_vertical)) return;
                _vertical = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Min size horizontal
        /// </summary>
        public Size MinHorizontal
        {
            get { return _minHorizontal; }
            set
            {
                if (value.Equals(_minHorizontal)) return;
                _minHorizontal = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Max size horizontal
        /// </summary>
        public Size MaxHorizontal
        {
            get { return _maxHorizontal; }
            set
            {
                if (value.Equals(_maxHorizontal)) return;
                _maxHorizontal = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ideal size horizontally. Not guaranteed to be this size.
        /// </summary>
        public Size Horizontal
        {
            get { return _horizontal; }
            set
            {
                if (value.Equals(_horizontal)) return;
                _horizontal = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}