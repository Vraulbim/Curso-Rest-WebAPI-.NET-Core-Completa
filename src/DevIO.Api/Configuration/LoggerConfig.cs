using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddElmahIo(credentials =>
            {
                credentials.ApiKey = "";
                credentials.LogId = new Guid("");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher("", new Guid(""))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI();


            //services.AddLogging(builder => {
            //    builder.AddElmahIo(credentials => {
            //        credentials.ApiKey = "c3e072b8cba9488c82c01be413900b49";
            //        credentials.LogId = new Guid("89519617-7d4a-4a54-b392-1f32ca874459");
            //    });

            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(settings => {

                settings.UIPath = "/api/healthUI";
            });

            return app;
        }
    }
}
