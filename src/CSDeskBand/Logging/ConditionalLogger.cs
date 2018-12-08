using System;
using System.Diagnostics;

namespace CSDeskBand.Logging
{
    /// <summary>
    /// Logger wrapper than can be turned off with a symbol.
    /// </summary>
    internal class ConditionalLogger : ILog
    {
        private static readonly object Lock = new object();
        private static bool _logEnabled;
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

        /// <summary>
        /// Gets or sets a value indicating whether;
        /// </summary>
        internal static bool LogEnabled
        {
            get => _logEnabled;
            set
            {
                lock (Lock)
                {
                    _logEnabled = value;
                }
            }
        }

        /// <inheritdoc/>
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            if (LogEnabled)
            {
                return _logger.Log(logLevel, messageFunc, exception, formatParameters);
            }

            return false;
        }
    }
}
