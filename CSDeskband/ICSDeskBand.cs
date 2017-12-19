using CSDeskband.Interop.COM;
using System;
using System.Drawing;

namespace CSDeskband
{
    public interface ICSDeskBand : IDeskBand2, IObjectWithSite
    {
        /// <summary>
        /// Min Size veritcally
        /// </summary>
        Size MinVertical { get; set; }

        /// <summary>
        /// Max size vertically. int.MaxValue - 1 for no limit
        /// </summary>
        Size MaxVertical { get; set; }

        /// <summary>
        /// Ideal size vertically
        /// </summary>
        Size Vertical { get; set; }

        /// <summary>
        /// Min size horizontal
        /// </summary>
        Size MinHorizontal { get; set; }

        /// <summary>
        /// Max size horizontal
        /// </summary>
        Size MaxHorizontal { get; set; }

        /// <summary>
        /// Ideal size horizontally
        /// </summary>
        Size Horizontal { get; set; }

        /// <summary>
        /// Step size for resizing
        /// </summary>
        int Increment { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Deskband options
        /// </summary>
        CSDeskBandOptions Options { get; set; }
    }
}