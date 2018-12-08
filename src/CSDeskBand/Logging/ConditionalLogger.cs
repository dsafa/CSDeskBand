using System;
using System.Diagnostics;

namespace CSDeskBand.Logging
{
    /// <summary>
    /// Logger wrapper than can be turned off with a symbol.
    /// </summary>
    internal class ConditionalLogger : ILog
    {
        private ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalLogger"/> class
        /// with the logger to wrap.
        /// </summary>
        /// <param name="logger">Logger to wrap.</param>
        internal ConditionalLogger(ILog logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            var logged = false;
            LogInternal(ref logged, logLevel, messageFunc, exception, formatParameters);

            return logged;
        }

        [Conditional("CSDESKBAND_ENABLE_LOG")]
        private void LogInternal(ref bool logged, LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            logged = _logger.Log(logLevel, messageFunc, exception, formatParameters);
        }
    }
}
