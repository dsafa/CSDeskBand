using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CSDeskBand
{
    /// <summary>
    /// Size class that is independent of winforms or wpf.
    /// </summary>
    public sealed class Size : INotifyPropertyChanged
    {
        private int _width;
        private int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class.
        /// </summary>
        /// <param name="width">The <see cref="Width"/> component.</param>
        /// <param name="height">The <see cref="Height"/> component.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the width component of the size.
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (value == _width)
                {
                    return;
                }

                _width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the height component of the size.
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (value == _height)
                {
                    return;
                }

                _height = value;
                OnPropertyChanged();
            }
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
