using System;

namespace CSDeskband
{
    [AttributeUsage(AttributeTargets.Class)]
    class CSDeskBandRegistrationAttribute : Attribute
    {
        public string Name { get; set; }
    }
}