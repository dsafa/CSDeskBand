using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSDeskband.Win;
using CSDeskband;
using System.Runtime.InteropServices;

namespace Sample.Win
{
    [ComVisible(true)]
    [Guid("5731FC61-8530-404C-86C1-86CCB8738D06")]
    [CSDeskBandRegistration(Name = "Sample Winforms Deskband")]
    public partial class UserControl1 : CSDeskBand
    {
        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
