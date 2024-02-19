using System.Reflection;
using CleanCode.Util.Logging.Enrichers;
using CleanCode.Util.Logging.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

namespace CleanCode.Util.Logging
{
    public static class LoggingHelpers
    {
        /// <summary>
        /// Provides standardized, centralized Serilog wire-up for a suite of applications.
        /// </summary>
        /// <param name="loggerConfiguration">Provide this value from the UseSerilog method param</param>
        /// <param name="hostingContext">Hosting Context</param>
        /// <param name="provider">Provider</param>
        /// <param name="config">IConfiguration settings -- generally read this from appsettings.json</param>
        public static void LogConfiguration(this LoggerConfiguration loggerConfiguration,
            HostBuilderContext hostingContext, IServiceProvider provider, IConfiguration config)
        {
            var env = hostingContext.HostingEnvironment;
            var assembly = Assembly.GetExecutingAssembly().GetName();

            var applicationName = config.GetValue<string>("ApplicationName");
            var applicationVersion = config.GetValue<string>("ApplicationVersion");

            loggerConfiguration
                .ReadFrom.Configuration(config) // minimum log levels defined per project in appsettings.json files 
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", applicationName)
                .Enrich.WithProperty("ApplicationVersion", applicationVersion)
                .Enrich.WithProperty("Environment", env.EnvironmentName)
                .Enrich.WithProperty("LoggerName", assembly.Name)
                .Enrich.WithHttpContextInfo(provider, (logEvent, propertyFactory, httpContext) =>
                {
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestMethod",
                        httpContext.Request.Method));
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Referer",
                        httpContext.Request.Headers["Referer"].ToString()));
                    if (httpContext.Response.HasStarted)
                    {
                        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ResponseStatus",
                            httpContext.Response.StatusCode));
                    }
                })
                .Enrich.WithCorrelationId()
                .Enrich.WithMachineName()
                .Enrich.WithClientIp()
                .Enrich.WithClientAgent()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName();


            #region Write Logs to Console

            var isConsoleLogEnabled = config.GetValue<bool>("Serilog:ConsoleLog:Enabled");

            if (isConsoleLogEnabled)
                loggerConfiguration.WriteTo.Console(
                    new CustomElasticSearchJsonFormatter(inlineFields: true, renderMessageTemplate: false,
                        formatStackTraceAsArray: true));

            #endregion

            #region Write Logs to File

            var isFileLogEnabled = config.GetValue<bool>("Serilog:FileLog:Enabled");

            if (isFileLogEnabled)
            {
                loggerConfiguration.WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(Matching.WithProperty("elapsedMilliseconds"))
                    .WriteTo.File(new CustomLogEntryFormatter(),
                        $"logs/File/performance-{env.ApplicationName.Replace(".", "-").ToLower()}-{env.EnvironmentName?.ToLower()}-.txt",
                        rollingInterval: RollingInterval.Day));

                loggerConfiguration.WriteTo.Logger(lc => lc
                    .Filter.ByExcluding(Matching.WithProperty("elapsedMilliseconds"))
                    .WriteTo.File(new CustomLogEntryFormatter(),
                        $"logs/File/usage-{env.ApplicationName.Replace(".", "-").ToLower()}-{env.EnvironmentName?.ToLower()}-.txt",
                        rollingInterval: RollingInterval.Day)
                );
            }

            #endregion

            # region Write Logs to Elastic Search - https: //github.com/serilog/serilog-sinks-elasticsearch

            var isElasticLogEnabled = config.GetValue<bool>("Serilog:ElasticLog:Enabled");

            if (isElasticLogEnabled)
            {
                var elasticUrl = config.GetValue<string>("Serilog:ElasticUrl");

                if (!string.IsNullOrEmpty(elasticUrl))
                {
                    loggerConfiguration.WriteTo.Logger(lc => lc
                            .Filter.ByIncludingOnly(Matching.WithProperty("elapsedMilliseconds"))
                            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
                            {
                                AutoRegisterTemplate = true,
                                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                                IndexFormat =
                                        $"performance-{env.ApplicationName.Replace(".", "-").ToLower()}-{env.EnvironmentName?.ToLower()}-{{0:yyyy.MM.dd}}",
                                InlineFields = true,
                                CustomFormatter = new CustomElasticSearchJsonFormatter(inlineFields: true,
                                        renderMessageTemplate: false, formatStackTraceAsArray: true),
                                FailureCallback = e =>
                                    Console.WriteLine("Unable to submit log event to ElasticSearch. Log Level - " +
                                                      e.Level),
                                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                                       EmitEventFailureHandling.WriteToFailureSink |
                                                       EmitEventFailureHandling.RaiseCallback,
                                FailureSink =
                                        new FileSink(
                                            $"logs/FailureCallback/performance-{env.ApplicationName.Replace(".", "-").ToLower()}-{env.EnvironmentName?.ToLower()}/failures.txt",
                                            new JsonFormatter(), null)
                            }
                            ))
                        .WriteTo.Logger(lc => lc
                            .Filter.ByExcluding(Matching.WithProperty("elapsedMilliseconds"))
                            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
                            {
                                AutoRegisterTemplate = true,
                                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                                IndexFormat =
                                        $"usage-{env.ApplicationName.Replace(".", "-").ToLower()}-{env.EnvironmentName?.ToLower()}-{{0:yyyy.MM.dd}}",
                                InlineFields = true,
                                CustomFormatter = new CustomElasticSearchJsonFormatter(inlineFields: true,
                                        renderMessageTemplate: false, formatStackTraceAsArray: true),
                                FailureCallback = e =>
                                    Console.WriteLine("Unable to submit log event to ElasticSearch. Log Level - " +
                                                      e.Level),
                                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                                       EmitEventFailureHandling.WriteToFailureSink |
                                                       EmitEventFailureHandling.RaiseCallback,
                                FailureSink =
                                        new FileSink(
                                            $"logs/FailureCallback/usage-{env.ApplicationName.Replace(".", "-").ToLower()}-{env.EnvironmentName?.ToLower()}/failures.txt",
                                            new JsonFormatter(), null)
                            }
                            ));
                }
            }

            #endregion
        }
    }
}