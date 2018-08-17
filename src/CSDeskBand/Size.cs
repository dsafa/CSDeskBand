using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CSDeskBand.Annotations;

namespace CSDeskBand
{
    /// <summary>
    /// Size class that is used so winforms or wpf is not used.
    /// </summary>
    public sealed class Size : INotifyPropertyChanged
    {
        private int _width;
        private int _height;

        /// <summary>
        /// The width component of the size.
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (value == _width) return;
                _width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The height component of the size.
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (value == _height) return;
                _height = value;
                OnPropertyChanged();
            }
        }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static implicit operator Size(System.Windows.Size size)
        {
            return new Size(Convert.ToInt32(size.Width), Convert.ToInt32(size.Height));
        }

        public static implicit operator System.Windows.Size(Size size)
        {
            return new System.Windows.Size(size.Width, size.Height);
        }

        public static implicit operator Size(System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }

        public static implicit operator System.Drawing.Size(Size size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
