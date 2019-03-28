namespace CSDeskBand
{
    using System;

    internal interface IDeskBandProvider
    {
        IntPtr Handle { get; }
        CSDeskBandOptions Options { get; }
        Guid Guid { get; }
        bool HasFocus { get; set; }
    }
}
