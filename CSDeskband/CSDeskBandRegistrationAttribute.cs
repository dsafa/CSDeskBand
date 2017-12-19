using System;

namespace CSDeskband
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CSDeskBandRegistrationAttribute : Attribute
    {
        public string Name { get; set; }
    }
}