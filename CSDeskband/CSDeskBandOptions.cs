using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDeskband
{
    public class CSDeskBandOptions
    {
        public bool VariableHeight { get; set; } = false;
        public bool Sunken { get; set; } = false;
        public bool Fixed { get; set; } = false;
        public bool Undeleteable { get; set; } = false;
        public bool AlwaysShowGripper { get; set; } = false;
        public bool NoMargins { get; set; } = false;
    }
}