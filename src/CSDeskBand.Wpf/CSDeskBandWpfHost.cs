using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using UserControl = System.Windows.Controls.UserControl;

namespace CSDeskBand.Wpf
{
    /// <summary>
    /// Host for the wpf deskband.
    /// </summary>
    internal class CSDeskBandWpfHost : Form
    {
        private ContainerWindow _containerWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSDeskBandWpfHost"/> class.
        /// </summary>
        /// <param name="control">The control to host.</param>
        public CSDeskBandWpfHost(UserControl control)
        {
            FormBorderStyle = FormBorderStyle.None;
            AllowTransparency = true;
            TransparencyKey = Color.Black;
            BackColor = Color.Black;

            _containerWindow = new ContainerWindow(control);
            _containerWindow.SizeChanged += ContainerWindow_ControlSizeChanged;

            var interopHelper = new WindowInteropHelper(_containerWindow);
            interopHelper.EnsureHandle();
            interopHelper.Owner = Handle;

            if (NativeMethods.DwmIsCompositionEnabled())
            {
                var status = Marshal.AllocCoTaskMem(sizeof(uint));
                Marshal.Copy(new[] { (int)NativeMethods.DwmncRenderingPolicy.DWMNCRP_ENABLED }, 0, status, 1);
                NativeMethods.DwmSetWindowAttribute(interopHelper.Handle, NativeMethods.DwmWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK, status, sizeof(uint));
            }

            _containerWindow.Show();

            UpdateWindow();
        }

        /// <inheritdoc/>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateWindow();
        }

        /// <inheritdoc/>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            UpdateWindow();
        }

        private void ContainerWindow_ControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size = new System.Drawing.Size((int)e.NewSize.Width, (int)e.NewSize.Height);
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            _containerWindow.Width = Width;
            _containerWindow.Height = Height;
            _containerWindow.Left = Left;
            _containerWindow.Top = Top;
            _containerWindow.UpdateChild();
            _containerWindow.Topmost = true;
        }

        private class ContainerWindow : Window
        {
            private UserControl _child;

            public ContainerWindow(UserControl child)
            {
                _child = child;

                ShowInTaskbar = false;
                WindowStyle = WindowStyle.None;
                AllowsTransparency = true;
                Background = System.Windows.Media.Brushes.Transparent;
                Content = child;
                SizeToContent = SizeToContent.WidthAndHeight;
            }

            public void UpdateChild()
            {
                if (Width != _child.Width)
                {
                    _child.Width = Width;
                }

                if (Height != _child.Height)
                {
                    _child.Height = Height;
                }
            }
        }
    }
}
