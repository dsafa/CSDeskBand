using System;

namespace CSDeskBand
{
    /// <summary>
    /// Attributes that are used to define some properties of the deskband
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CSDeskBandRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Name of the deskband in the toolbar menu.
        /// </summary>
        /// <value>
        /// The name is used to select the deskband from the toolbars menu.
        /// </value>
        public string Name { get; set; }
    }
}