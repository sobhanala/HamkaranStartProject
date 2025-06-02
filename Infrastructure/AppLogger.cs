using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure
{
    public static class AppLogger
    {
        public static ILoggerFactory LoggerFactory { get; set; }

        private static ILoggerFactory _loggerFactory;

        public static void Initialize(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static ILogger CreateLogger(string category) =>
            _loggerFactory?.CreateLogger(category) ?? NullLogger.Instance;
    }
}
