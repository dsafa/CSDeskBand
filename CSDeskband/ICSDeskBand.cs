using CSDeskband.Interop.COM;
namespace CSDeskband
{
    public interface ICSDeskBand : IDeskBand2, IObjectWithSite
    {
        /// <summary>
        /// Deskband options
        /// </summary>
        CSDeskBandOptions Options { get; set; }
    }
}