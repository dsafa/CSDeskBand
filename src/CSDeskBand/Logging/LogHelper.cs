using System;

namespace CSDeskBand.Logging
{
    /// <summary>
    /// Helper for logging.
    /// </summary>
    internal static class LogHelper
    {
        /// <summary>
        /// Gets a logger by type.
        /// </summary>
        /// <param name="type">Type of the logger.</param>
        /// <returns>A logger.</returns>
        internal static ILog GetLogger(Type type)
        {
            return new ConditionalLogger(LogProvider.GetLogger(type));
        }
    }
}
