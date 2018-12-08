using System;

namespace CSDeskBand.Logging
{
    internal static class LogHelper
    {
        static ILog GetLogger(Type type)
        {
            return new ConditionalLogger(LogProvider.GetLogger(type));
        }
    }
}
