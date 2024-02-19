using Microsoft.Extensions.Logging;

namespace CleanCode.Util.Logging
{
    // High-performance logging with LoggerMessage in ASP.NET Core - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage
    // LoggingHelpers Performance Measurement - https://www.stevejgordon.co.uk/high-performance-logging-in-net-core
    public static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, Exception> TraceLog;

        private static readonly Action<ILogger, string, Exception> DebugLog;

        private static readonly Action<ILogger, string, Exception> InformationLog;

        private static readonly Action<ILogger, string, Exception> WarningLog;

        private static readonly Action<ILogger, string, Exception> ErrorLog;

        private static readonly Action<ILogger, string, Exception> CriticalLog;

        private static readonly Action<ILogger, string, string, long, Exception> RoutePerformance;

        private static readonly Action<ILogger, string, string, Exception> UnauthorizedAccess;

        static LoggerExtensions()
        {
            TraceLog = LoggerMessage.Define<string>(LogLevel.Trace, 0, "{logMessage}");
            DebugLog = LoggerMessage.Define<string>(LogLevel.Debug, 0, "{logMessage}");
            InformationLog = LoggerMessage.Define<string>(LogLevel.Information, 0, "{logMessage}");
            WarningLog = LoggerMessage.Define<string>(LogLevel.Warning, 0, "{logMessage}");
            ErrorLog = LoggerMessage.Define<string>(LogLevel.Error, 0, "{logMessage}");
            CriticalLog = LoggerMessage.Define<string>(LogLevel.Critical, 0, "{logMessage}");
            RoutePerformance = LoggerMessage.Define<string, string, long>(LogLevel.Information, 0,
                "{method} - {routeName} - code took {elapsedMilliseconds} milliseconds.");
            UnauthorizedAccess =
                LoggerMessage.Define<string, string>(LogLevel.Critical, 0,
                    "Unauthorized Access - {eventType} - {description}");
        }

        public static void LogTraceExtension(this ILogger logger, string logMessage)
        {
            TraceLog(logger, logMessage, null);
        }

        public static void LogDebugExtension(this ILogger logger, string logMessage)
        {
            DebugLog(logger, logMessage, null);
        }

        public static void LogInformationExtension(this ILogger logger, string logMessage)
        {
            InformationLog(logger, logMessage, null);
        }

        public static void LogWarningExtension(this ILogger logger, string logMessage)
        {
            WarningLog(logger, logMessage, null);
        }

        public static void LogErrorExtension(this ILogger logger, string logMessage, Exception ex)
        {
            ErrorLog(logger, logMessage, ex);
        }

        public static void LogCriticalExtension(this ILogger logger, string logMessage, Exception ex)
        {
            CriticalLog(logger, logMessage, ex);
        }

        public static void LogRoutePerformance(this ILogger logger, string method, string routeName,
            long elapsedMilliseconds)
        {
            RoutePerformance(logger, method, routeName, elapsedMilliseconds, null);
        }

        public static void LogUnauthorizedAccess(this ILogger logger, string eventType, string description)
        {
            UnauthorizedAccess(logger, eventType, description, null);
        }

        public static void LogHttpResponse(this ILogger logger, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.RequestMessage != null)
                    logger.LogDebug("Received a success response from {Url}", response.RequestMessage.RequestUri);
            }
            else
            {
                if (response.RequestMessage != null)
                    logger.LogWarning("Received a non-success status code {StatusCode} from {Url}",
                        (int)response.StatusCode, response.RequestMessage.RequestUri);
            }
        }
    }
}
