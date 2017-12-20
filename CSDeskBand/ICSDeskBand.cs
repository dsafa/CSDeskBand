using CSDeskBand.Interop.COM;

namespace CSDeskBand
{
    public interface ICSDeskBand : IDeskBand2, IObjectWithSite
    {
        /// <summary>
        /// Deskband options
        /// </summary>
        CSDeskBandOptions Options { get; set; }
    }
}