namespace CSDeskBand
{
    using CSDeskBand.Interop;

    /// <summary>
    /// Deskband Interface
    /// </summary>
    public interface ICSDeskBand : IDeskBand2, IObjectWithSite, IContextMenu3, IPersistStream, IInputObject
    {
    }
}
