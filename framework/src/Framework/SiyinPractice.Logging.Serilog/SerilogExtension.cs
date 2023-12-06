using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Microsoft.AspNetCore.Builder
{
    public static class SerilogExtension
    {
        public static void UseContelWorksSerilog(this IApplicationBuilder app)
        {
            var configuation = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                  .Enrich.FromLogContext()
                  .WriteTo.File(System.IO.Path.Combine("Logs", @"log.txt"), rollingInterval: RollingInterval.Hour)
                  .WriteTo.File(System.IO.Path.Combine("Logs", @"error.txt"), LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                  .WriteTo.File(System.IO.Path.Combine("Logs", @"warning.txt"), LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
                  .WriteTo.File(System.IO.Path.Combine("Logs", @"fatal.txt"), LogEventLevel.Fatal, rollingInterval: RollingInterval.Day);

            Log.Logger = configuation.CreateLogger();
            app.ApplicationServices.GetRequiredService<ILoggerFactory>().AddSerilog();
        }
    }
}