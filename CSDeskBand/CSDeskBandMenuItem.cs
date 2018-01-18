using System;
using System.Collections.Generic;

namespace CSDeskBand
{
    public abstract class CSDeskBandMenuItem
    {
        internal abstract void AddToMenu(IntPtr menu, uint pos, ref uint firstCmdId, Dictionary<uint, CSDeskBandMenuAction> callbacks);
    }
}