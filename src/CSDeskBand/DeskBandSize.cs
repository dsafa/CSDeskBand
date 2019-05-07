namespace CSDeskBand
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Size class that is independent of winforms or wpf.
    /// </summary>
    public sealed class DeskBandSize : INotifyPropertyChanged
    {
        private int _width;
        private int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeskBandSize"/> class.
        /// </summary>
        /// <param name="width">The <see cref="Width"/> component.</param>
        /// <param name="height">The <see cref="Height"/> component.</param>
        public DeskBandSize(int width, int height)
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

#if DESKBAND_WPF
        /// <summary>
        /// Converts from <see cref="System.Windows.Size"/> to <see cref="DeskBandSize"/>.
        /// </summary>
        /// <param name="size">The <see cref="System.Windows.Size"/> to convert.</param>
        public static implicit operator DeskBandSize(System.Windows.Size size)
        {
            return new DeskBandSize(Convert.ToInt32(size.Width), Convert.ToInt32(size.Height));
        }

        /// <summary>
        /// Converts from <see cref="DeskBandSize"/> to <see cref="System.Windows.Size"/>.
        /// </summary>
        /// <param name="size">The <see cref="DeskBandSize"/> to convert.</param>
        public static implicit operator System.Windows.Size(DeskBandSize size)
        {
            return new System.Windows.Size(size.Width, size.Height);
        }
#endif

#if DESKBAND_WINFORMS
        /// <summary>
        /// Converts from <see cref="System.Drawing.Size"/> to <see cref="DeskBandSize"/>.
        /// </summary>
        /// <param name="size">The <see cref="System.Drawing.Size"/> to convert.</param>
        public static implicit operator DeskBandSize(System.Drawing.Size size)
        {
            return new DeskBandSize(size.Width, size.Height);
        }

        /// <summary>
        /// Converts from <see cref="DeskBandSize"/> to <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <param name="size">The <see cref="DeskBandSize"/> to convert.</param>
        public static implicit operator System.Drawing.Size(DeskBandSize size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }
#endif
    }
}