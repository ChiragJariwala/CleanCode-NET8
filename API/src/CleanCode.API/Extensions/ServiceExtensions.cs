using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CleanCode.Api.Extensions.Swagger;
using CleanCode.Core.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using RestSharp;
using CleanCode.Util.Logging;
using CleanCode.Util.Models;
using Microsoft.Data.SqlClient;
using CleanCode.Infrastructure.Repositories.Base;
using CleanCode.Core.Repositories.Base;
using CleanCode.Infrastructure.Repositories;
using CleanCode.Core.Repositories;
using CleanCode.Business.Services;
using CleanCode.Business.Interfaces;
using CleanCode.Core.Entities;

namespace CleanCode.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Database
            ConfigureDatabases(services, configuration);

            // Using Scrutor to map the dependencies with scoped lifetime (https://github.com/khellang/Scrutor)
            //services.Scan(scan => scan
            //.FromCallingAssembly()
            //.FromApplicationDependencies(c => c.FullName != null && c.FullName.StartsWith("BNMS"))
            //.AddClasses()
            //.AsMatchingInterface().WithScopedLifetime());

            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
           
           // services.AddScoped<IMetadataRepository, MetadataRepository>();

            services.AddScoped<Core.Services.ISsoService, Infrastructure.Services.SsoService>();

            // Add Business Layer
           // services.AddScoped<IMetadataService, MetadataService>();
          

            //<Do not Delete this line>
          //  services.AddScoped<ISsoService, SsoService>();

            // Add AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // LoggingHelpers
            services.AddTransient<LoggingDelegatingHandler>();

            //External Service Dependency (Example: OrderService)
            services.AddTransient<IRestClient>(c => new RestClient(configuration["OrderServiceEndpoint"]));
            services.Configure<SsoApiModel>(configuration.GetSection("SsoService"));

            // HealthChecks
            services.AddHealthChecks().AddDbContextCheck<CleanCodeContext>();
        }

        private static void ConfigureDatabases(IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<CleanCodeContext>(options => options.UseSqlServer(dbConnectionString));
        }

        private static string CreateConnectionString(DbConnectionSettings databaseConnectionSettings)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = string.IsNullOrEmpty(databaseConnectionSettings.Port)
                    ? databaseConnectionSettings.Host
                    : databaseConnectionSettings.Host + "," + databaseConnectionSettings.Port,
                InitialCatalog = databaseConnectionSettings.DatabaseName,
                IntegratedSecurity = databaseConnectionSettings.IntegratedSecurity,
                MultipleActiveResultSets = databaseConnectionSettings.MultipleActiveResultSets,
                PersistSecurityInfo = true,
                UserID = databaseConnectionSettings.UserName,
                Password = databaseConnectionSettings.Password
            };
            return builder.ConnectionString;
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                // Supporting multiple versioning scheme
                // Route (api/v1/accounts)
                // Header (X-version=1)
                // Querystring (api/accounts?api-version=1)
                // Media Type (application/json;v=1)
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-version"), new QueryStringApiVersionReader("api-version"),
                    new MediaTypeApiVersionReader("v"));
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(ConfigureSwaggerGen);
        }

        private static void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            AddSwaggerDocs(options);

            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

            options.DocInclusionPredicate((version, desc) =>
            {
                if (!desc.TryGetMethodInfo(out var methodInfo))
                    return false;

                var versions = methodInfo
                    .DeclaringType?
                    .GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                var maps = methodInfo
                    .GetCustomAttributes(true)
                    .OfType<MapToApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions)
                    .ToList();

                return versions?.Any(v => $"v{v}" == version) == true
                       && (!maps.Any() || maps.Any(v => $"v{v}" == version));
            });
        }

        private static void AddSwaggerDocs(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1.0", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "BNMS API"
            });

            // Future Version
            //options.SwaggerDoc("v2", new OpenApiInfo
            //{
            //    Version = "v2",
            //    Title = "BNMS API"
            //});
        }

        public static void ConfigureRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();

            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);

            services.AddSingleton(redisCacheSettings);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisCacheSettings.ConnectionString;
                options.InstanceName = redisCacheSettings.InstanceName;
            });

            services.AddSingleton<Business.Interfaces.IRedisCacheService, Business.Services.RedisCacheService>();
            services.AddSingleton<Core.Services.IRedisCacheService, Infrastructure.Services.RedisCacheService>();
        }
    }
}