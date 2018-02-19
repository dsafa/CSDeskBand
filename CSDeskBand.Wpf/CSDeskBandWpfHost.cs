using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CSDeskBand.Wpf
{
    internal class CSDeskBandWpfHost : Form
    {
        private ElementHost _host;

        public CSDeskBandWpfHost(System.Windows.Controls.UserControl control)
        {
            FormBorderStyle = FormBorderStyle.None;
            AllowTransparency = true;
            TransparencyKey = Color.Black;
            BackColor = Color.Black;

            _host = new ElementHost
            {
                Child = control,
                AutoSize = true,
                Dock = DockStyle.Fill, //This is required or else it will crash
                BackColorTransparent = true
            };

            Controls.Add(_host);
        }
    }
}