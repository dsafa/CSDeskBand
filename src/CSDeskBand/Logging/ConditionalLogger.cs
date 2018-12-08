using System;
using System.Diagnostics;

namespace CSDeskBand.Logging
{
    internal class ConditionalLogger : ILog
    {
        private ILog _logger;

        public ConditionalLogger(ILog logger)
        {
            _logger = logger;
        }

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
