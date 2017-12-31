using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSDeskBand.Interop;

namespace CSDeskBand
{
    public abstract class CSDeskBandMenuItem
    {
        public string Text { get; set; } = "";
        public bool Enabled { get; set; } = true;

        internal abstract uint ItemCount { get; }
        internal abstract void AddToMenu(IntPtr menu, uint pos, ref uint firstCmdId, Dictionary<uint, CSDeskBandMenuAction> callbacks);
    }
}