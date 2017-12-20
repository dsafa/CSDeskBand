using System;

namespace CSDeskBand
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CSDeskBandRegistrationAttribute : Attribute
    {
        public string Name { get; set; }
    }
}