using System.Reflection;
using DiabloCms.Server.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sentry.AspNetCore;

namespace DiabloCms.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogTracker()
                .AddDataBase(Configuration)
                .AddIdentity()
                .AddJwtAuthentication()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddHttpClient()
                .AddApplicationService()
                .AddCors()
                .AddController()
                .AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseExceptionHandling(env)
                .UseValidationExceptionHandler()
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseCorsAngular()
                .UseRouting()
                .UseSentryTracing()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints()
                .AddSwagger()
                .InitializeDbContext();
        }
    }
}