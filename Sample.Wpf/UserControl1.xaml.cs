﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSDeskBand.Wpf;
using System.Runtime.InteropServices;
using CSDeskBand;
using CSDeskBand.Annotations;

namespace Sample.Wpf
{
    /// <summary>
    /// Example WPF deskband. Shows taskbar info capabilities and context menus
    /// </summary>
    [ComVisible(true)]
    [Guid("89BF6B36-A0B0-4C95-A666-87A55C226986")]
    [CSDeskBandRegistration(Name = "Sample WPF Deskband")]
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

        private List<CSDeskBandMenuItem> ContextMenuItems
        {
            get
            {
                var action = new CSDeskBandMenuAction("Action - Toggle submenu");
                var separator = new CSDeskBandMenuSeparator();
                var submenuAction = new CSDeskBandMenuAction("Submenu Action - Toggle checkmark");
                var submenu = new CSDeskBandMenu("Submenu")
                {
                    Items = { submenuAction }
                };

                action.Clicked += (sender, args) => submenu.Enabled = !submenu.Enabled;
                submenuAction.Clicked += (sender, args) => submenuAction.Checked = !submenuAction.Checked;

                return new List<CSDeskBandMenuItem>() {action, separator, submenu};
            }
        }

        public UserControl1()
        {
            InitializeComponent();
            Options.MinHorizontal.Width = 500;
            Options.MinVertical.Width = 130;
            Options.MinVertical.Height = 200;

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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
