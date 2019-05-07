namespace CSDeskBand
{
    using System;

    /// <summary>
    /// Specifies registration configuration for a deskband.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class CSDeskBandRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the deskband in the toolbar menu.
        /// </summary>
        /// <value>
        /// The name is used to select the deskband from the toolbars menu.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to automatically show the deskband after registration.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the deskband should be automatically shown after registration; <see langword="false"/> otherwise.
        /// </value>
        public bool ShowDeskBand { get; set; }
    }
}
