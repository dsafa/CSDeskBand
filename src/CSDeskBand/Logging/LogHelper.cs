using System;

namespace CSDeskBand.Logging
{
    internal static class LogHelper
    {
        public static ILog GetLogger(Type type)
        {
            return new ConditionalLogger(LogProvider.GetLogger(type));
        }
    }
}
