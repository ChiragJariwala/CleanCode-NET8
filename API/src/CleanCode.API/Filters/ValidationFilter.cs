using System.Net;
using CleanCode.Util.Models;
using CleanCode.Util.Middleware;
using CleanCode.Util.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanCode.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.LogWarningExtension("Model Validation Failed. Version: " + context.RouteData.Values["Version"] +
                                            ", Controller - " + context.RouteData.Values["Controller"] + ", Action: " +
                                            context.RouteData.Values["Action"]);

                var errorsInModelState = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new Response<ApiError>(null, false, "Model Validation Failed")
                    {Errors = new List<ApiError>()};

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        errorResponse.Errors.Add(new ApiError
                        {
                            ErrorId = error.Key,
                            StatusCode = (short) HttpStatusCode.BadRequest,
                            Message = subError
                        });
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
