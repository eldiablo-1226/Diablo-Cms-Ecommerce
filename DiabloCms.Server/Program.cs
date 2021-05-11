using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiabloCms.Server
{
    public static class Program
    {
        private static readonly Action<IWebHostBuilder> Configure = webHostBuilder =>
        {
            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseSentry(o =>
            {
                o.Dsn = "https://e99063915daa41709b6ed52f37b6ea44@o542316.ingest.sentry.io/5661873";
                o.MaxBreadcrumbs = 50;
                o.Debug = true;
                o.SendDefaultPii = true;
                o.ServerName = "WebApi";

                o.TracesSampleRate = 1.0;
                o.TracesSampler = _ => 1.0;
            });
        };

        private static readonly Action<HostBuilderContext, ILoggingBuilder> ConfigureLogger = (c, l) =>
        {
            l.AddConfiguration(c.Configuration);
            l.AddSentry();
        };

        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(Configure)
                .ConfigureLogging(ConfigureLogger)
                .Build().Run();
        }
    }
}