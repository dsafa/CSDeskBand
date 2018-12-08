using CSDeskBand;
using CSDeskBand.ContextMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace Sample.Wpf
{
    /// <summary>
    /// Example WPF deskband. Shows taskbar info capabilities and context menus
    /// </summary>
    [ComVisible(true)]
    [Guid("89BF6B36-A0B0-4C95-A666-87A55C226986")]
    [CSDeskBandRegistration(Name = "Sample WPF Deskband", ShowDeskBand = true)]
    public partial class UserControl1 : INotifyPropertyChanged
    {
        private Orientation _taskbarOrientation;
        private int _taskbarWidth;
        private int _taskbarHeight;
        private Edge _taskbarEdge;

        public Orientation TaskbarOrientation
        {
            get => _taskbarOrientation;
            set
            {
                if (value == _taskbarOrientation) return;
                _taskbarOrientation = value;
                OnPropertyChanged();
            }
        }

        public int TaskbarWidth
        {
            get => _taskbarWidth;
            set
            {
                if (value == _taskbarWidth) return;
                _taskbarWidth = value;
                OnPropertyChanged();
            }
        }

        public int TaskbarHeight
        {
            get => _taskbarHeight;
            set
            {
                if (value == _taskbarHeight) return;
                _taskbarHeight = value;
                OnPropertyChanged();
            }
        }

        public Edge TaskbarEdge
        {
            get => _taskbarEdge;
            set
            {
                if (value == _taskbarEdge) return;
                _taskbarEdge = value;
                OnPropertyChanged();
            }
        }

        private List<DeskBandMenuItem> ContextMenuItems
        {
            get
            {
                var action = new DeskBandMenuAction("Action - Toggle submenu");
                var separator = new DeskBandMenuSeparator();
                var submenuAction = new DeskBandMenuAction("Submenu Action - Toggle checkmark");
                var submenu = new DeskBandMenu("Submenu")
                {
                    Items = { submenuAction }
                };

                action.Clicked += (sender, args) => submenu.Enabled = !submenu.Enabled;
                submenuAction.Clicked += (sender, args) => submenuAction.Checked = !submenuAction.Checked;

                return new List<DeskBandMenuItem>() {action, separator, submenu};
            }
        }

        public UserControl1()
        {
            InitializeComponent();
            Options.MinHorizontalSize.Width = 0;
            Options.MinVerticalSize.Width = 130;
            Options.MinVerticalSize.Height = 200;

            TaskbarInfo.TaskbarEdgeChanged += (sender, args) => TaskbarEdge = args.Edge;
            TaskbarInfo.TaskbarOrientationChanged += (sender, args) => TaskbarOrientation = args.Orientation == CSDeskBand.TaskbarOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;
            TaskbarInfo.TaskbarSizeChanged += (sender, args) =>
            {
                TaskbarWidth = args.Size.Width;
                TaskbarHeight = args.Size.Height;
            };

            TaskbarEdge = TaskbarInfo.Edge;
            TaskbarOrientation = TaskbarInfo.Orientation == CSDeskBand.TaskbarOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;
            TaskbarWidth = TaskbarInfo.Size.Width;
            TaskbarHeight = TaskbarInfo.Size.Height;

            Options.ContextMenuItems = ContextMenuItems;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
