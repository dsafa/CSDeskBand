using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleWinforms
{
    public partial class UserControl1: UserControl
    {
        public UserControl1(CSDeskBand.CSDeskBandWin w)
        {
            InitializeComponent();
            textBox1.GotFocus += (o, e) => w.UpdateFocus(true);
            
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if ((Keys)m.WParam == Keys.Tab)
            {
                var selected = SelectNextControl(ActiveControl, true, true, true, true);
                return true;
            }

            return base.ProcessKeyPreview(ref m);
        }
    }
}
