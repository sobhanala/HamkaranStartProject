using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure
{
    public static class AppLogger
    {
        private static ILoggerFactory _loggerFactory;

        public static void Initialize(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static ILogger CreateLogger(string category) =>
            _loggerFactory?.CreateLogger(category) ?? NullLogger.Instance;
    }
}
