using System;

namespace CSDeskBand
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

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
    }
}
