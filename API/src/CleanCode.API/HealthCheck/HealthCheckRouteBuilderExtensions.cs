using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CleanCode.Api.HealthCheck
{
    public static class HealthCheckRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapDefaultHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = HealthCheckResponses.WriteJsonResponse
            });

            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponses.WriteJsonResponse
            });

            return endpoints;
        }
    }
}
