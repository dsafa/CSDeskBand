﻿using CSDeskBand.Interop.COM;

namespace CSDeskBand
{
    /// <summary>
    /// Deskband Interface
    /// </summary>
    public interface ICSDeskBand : IDeskBand2, IObjectWithSite, IContextMenu3, IPersistStream
    {
    }
}