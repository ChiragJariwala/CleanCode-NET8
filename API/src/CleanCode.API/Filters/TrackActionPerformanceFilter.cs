using System.Diagnostics;
using CleanCode.Util.Logging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanCode.Api.Filters
{
    public class TrackActionPerformanceFilter : IActionFilter
    {
        private Stopwatch _timer;
        private readonly ILogger<TrackActionPerformanceFilter> _logger;
        private IDisposable _userScope;

        public TrackActionPerformanceFilter(ILogger<TrackActionPerformanceFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();

            var userDict = new Dictionary<string, string>
            {
                {"UserId", context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value},
                {
                    "OAuth2 Scopes",
                    string.Join(",", context.HttpContext.User.Claims.Where(c => c.Type == "scope").Select(c => c.Value))
                },
                {"ClientIP", context.HttpContext.Connection.RemoteIpAddress?.ToString()},
                {"UserAgent", context.HttpContext.Request.Headers["User-Agent"]}
            };
            _userScope = _logger.BeginScope(userDict);
            _timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if (context.Exception == null)
            {
                _logger.LogRoutePerformance(context.HttpContext.Request.Path, context.HttpContext.Request.Method,
                    _timer.ElapsedMilliseconds);
            }

            _userScope?.Dispose();
        }
    }
}