using System;

namespace CSDeskBand
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CSDeskBandRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Name of the deskband in the toolbar menu
        /// </summary>
        public string Name { get; set; }
    }
}