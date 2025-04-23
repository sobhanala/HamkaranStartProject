using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class ExceptionLoggingExtensions
    {
        public static void LogException(this ILogger logger, BaseException ex)
        {
            var logMessage = $"[{ex.ErrorCode}] {ex.Message}\n" +
                             $"Severity: {ex.Severity}\n" +
                             $"Stack Trace: {ex.StackTrace}\n";
            var level = LogLevel.Error;

            switch (ex.Severity)
            {
                case ExceptionSeverity.Critical:
                    level = LogLevel.Critical;
                    break;
                case ExceptionSeverity.Warning:
                    level = LogLevel.Warning;
                    break;
                default:
                    level = LogLevel.Error;
                    break;
            }

            logger.Log(level, ex, logMessage);
        }
    }
}