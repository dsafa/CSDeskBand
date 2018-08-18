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

        /// <summary>
        /// Initializes an instance of <see cref="Size"/>.
        /// </summary>
        /// <param name="width">The <see cref="Width"/> component.</param>
        /// <param name="height">The <see cref="Height"/> component.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Converts from <see cref="System.Windows.Size"/> to <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The <see cref="System.Windows.Size"/> to convert.</param>
        public static implicit operator Size(System.Windows.Size size)
        {
            return new Size(Convert.ToInt32(size.Width), Convert.ToInt32(size.Height));
        }

        /// <summary>
        /// Converts from <see cref="Size"/> to <see cref="System.Windows.Size"/>.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to convert.</param>
        public static implicit operator System.Windows.Size(Size size)
        {
            return new System.Windows.Size(size.Width, size.Height);
        }

        /// <summary>
        /// Converts from <see cref="System.Drawing.Size"/> to <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The <see cref="System.Drawing.Size"/> to convert.</param>
        public static implicit operator Size(System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }

        /// <summary>
        /// Converts from <see cref="Size"/> to <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to convert.</param>
        public static implicit operator System.Drawing.Size(Size size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }

        /// <summary>
        /// Occurs when one of the properties has changed its value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
