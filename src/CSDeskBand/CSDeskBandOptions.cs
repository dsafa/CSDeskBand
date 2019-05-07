namespace CSDeskBand
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using CSDeskBand.ContextMenu;

    /// <summary>
    /// Options to configure the deskband
    /// </summary>
    public sealed class CSDeskBandOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Height for a default horizontal taskbar.
        /// </summary>
        public static readonly int TaskbarHorizontalHeightLarge = 40;

        /// <summary>
        /// Height for a default horizontal taskbar with small icons.
        /// </summary>
        public static readonly int TaskbarHorizontalHeightSmall = 30;

        /// <summary>
        /// Width for a default vertical taskbar. There is no small vertical taskbar.
        /// </summary>
        public static readonly int TaskbarVerticalWidth = 62;

        /// <summary>
        /// Value that represents no limit for deskband size.
        /// </summary>
        /// <seealso cref="MaxHorizontalHeight"/>
        /// <seealso cref="MaxVerticalWidth"/>
        public static readonly int NoLimit = -1;

        private DeskBandSize _horizontalSize;
        private int _maxHorizontalHeight;
        private DeskBandSize _minHorizontalSize;
        private DeskBandSize _verticalSize;
        private int _maxVerticalWidth;
        private DeskBandSize _minVerticalSize;
        private string _title = "";
        private bool _showTitle = false;
        private bool _isFixed = false;
        private int _heightIncrement = 1;
        private bool _heightCanChange = true;
        private ICollection<DeskBandMenuItem> _contextMenuItems = new List<DeskBandMenuItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CSDeskBandOptions"/> class.
        /// </summary>
        public CSDeskBandOptions()
        {
            // Initialize in constructor to hook up property change events
            HorizontalSize = new DeskBandSize(200, TaskbarHorizontalHeightLarge);
            MaxHorizontalHeight = NoLimit;
            MinHorizontalSize = new DeskBandSize(NoLimit, NoLimit);

            VerticalSize = new DeskBandSize(TaskbarVerticalWidth, 200);
            MaxVerticalWidth = NoLimit;
            MinVerticalSize = new DeskBandSize(NoLimit, NoLimit);
        }

        /// <summary>
        /// Occurs when a property has change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the height of the horizontal deskband is allowed to change.
        /// <para/>
        /// Or for a deskband in the vertical orientation, if the width can change.
        /// Works alongside with the property <see cref="HeightIncrement"/>.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the height / width of the deskband can be changed. <see langword="false"/> to prevent changes.
        /// The default value is <see langword="true"/>.
        /// </value>
        public bool HeightCanChange
        {
            get => _heightCanChange;
            set
            {
                if (value == _heightCanChange)
                {
                    return;
                }

                _heightCanChange = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the height step size of a horizontal deskband when it is being resized.
        /// For a deskband in the vertical orientation, it will be the step size of the width.
        /// <para/>
        /// The deskband will only be resized to multiples of this value.
        /// </summary>
        /// <example>
        /// If increment is 50, then the height of the deskband can only be resized to 50, 100 ...
        /// </example>
        /// <value>
        /// The step size for resizing. This value is only used if <see cref="HeightCanChange"/> is true. If the value is less than 0, the height / width can be any size.
        /// The default value is 1.
        /// </value>
        public int HeightIncrement
        {
            get => _heightIncrement;
            set
            {
                if (value == _heightIncrement)
                {
                    return;
                }

                _heightIncrement = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the deskband has a fixed position and size.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the deskband is fixed. <see langword="false"/> if the deskband can be adjusted.
        /// The default value is <see langword="false"/>.
        /// </value>
        public bool IsFixed
        {
            get => _isFixed;
            set
            {
                if (value == _isFixed)
                {
                    return;
                }

                _isFixed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the value of <see cref="Title"/> is shown next to the deskband.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the title should be shown. <see langword="false"/> if the title is hidden.
        /// The default value is <see langword="false"/>.
        /// </value>
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                if (value == _showTitle)
                {
                    return;
                }

                _showTitle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title of the deskband. This will be shown if <see cref="ShowTitle"/> is <see langword="true"/>.
        /// </summary>
        /// <value>
        /// The title to display. If the title is null, it will be converted to an empty string.
        /// The default value is an empty string.
        /// </value>
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                {
                    return;
                }

                _title = value ?? "";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the minimum <see cref="DeskBandSize"/> of the deskband in the vertical orientation.
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        /// <value>
        /// The default value is <see cref="NoLimit"/> for the width and height.
        /// </value>
        public DeskBandSize MinVerticalSize
        {
            get => _minVerticalSize;
            set
            {
                if (value.Equals(_minVerticalSize))
                {
                    return;
                }

                _minVerticalSize = value;
                _minVerticalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the maximum width of the deskband in the vertical orientation.
        /// </summary>
        /// <remarks>
        /// The maximum height will have to be addressed in your code as there is no limit to the height of the deskband when vertical.
        /// </remarks>
        /// <seealso cref="TaskbarOrientation"/>
        /// <value>
        /// The default value is <see cref="NoLimit"/>.
        /// </value>
        public int MaxVerticalWidth
        {
            get => _maxVerticalWidth;
            set
            {
                if (value.Equals(_maxVerticalWidth))
                {
                    return;
                }

                _maxVerticalWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ideal <see cref="DeskBandSize"/> of the deskband in the vertical orientation.
        /// There is no guarantee that the deskband will be this size.
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        /// <value>
        /// The default value is <see cref="TaskbarVerticalWidth"/> for the width and 200 for the height.
        /// </value>
        public DeskBandSize VerticalSize
        {
            get => _verticalSize;
            set
            {
                if (value.Equals(_verticalSize))
                {
                    return;
                }

                _verticalSize = value;
                _verticalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the minimum <see cref="DeskBandSize"/> of the deskband in the horizontal orientation.
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        /// <value>
        /// The default value is <see cref="NoLimit"/>.
        /// </value>
        public DeskBandSize MinHorizontalSize
        {
            get => _minHorizontalSize;
            set
            {
                if (value.Equals(_minHorizontalSize))
                {
                    return;
                }

                _minHorizontalSize = value;
                _minHorizontalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the maximum height of the deskband in the horizontal orientation.
        /// </summary>
        /// <remarks>
        /// The maximum width will have to be addressed in your code as there is no limit to the width of the deskband when horizontal.
        /// </remarks>
        /// <seealso cref="TaskbarOrientation"/>
        /// <value>
        /// The default value is <see cref="NoLimit"/>.
        /// </value>
        public int MaxHorizontalHeight
        {
            get => _maxHorizontalHeight;
            set
            {
                if (value.Equals(_maxHorizontalHeight))
                {
                    return;
                }

                _maxHorizontalHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ideal <see cref="DeskBandSize"/> of the deskband in the horizontal orientation.
        /// There is no guarantee that the deskband will be this size.
        /// </summary>
        /// <seealso cref="TaskbarOrientation"/>
        /// <value>
        /// The default value is 200 for the width and <see cref="TaskbarHorizontalHeightLarge"/> for the height.
        /// </value>
        public DeskBandSize HorizontalSize
        {
            get => _horizontalSize;
            set
            {
                if (value.Equals(_horizontalSize))
                {
                    return;
                }

                _horizontalSize = value;
                _horizontalSize.PropertyChanged += (sender, args) => OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="DeskBandMenuItem"/> the comprise the deskbands context menu.
        /// </summary>
        /// <value>
        /// A list of <see cref="DeskBandMenuItem"/> for the context menu. An empty collection indicates no context menu.
        /// </value>
        /// <remarks>
        /// These context menu items are in addition of the default ones that windows provides.
        /// The items will appear in their enumerated order.
        /// </remarks>
        public ICollection<DeskBandMenuItem> ContextMenuItems
        {
            get => _contextMenuItems;
            set
            {
                if (Equals(value, _contextMenuItems))
                {
                    return;
                }

                _contextMenuItems = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
