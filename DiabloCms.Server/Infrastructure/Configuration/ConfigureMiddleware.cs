using DiabloCms.Entities.Models;
using DiabloCms.MsSql;
using DiabloCms.Server.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiabloCms.Server.Infrastructure.Configuration
{
    public static class ConfigureMiddleware
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            return app;
        }

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            return app;
        }

        public static IApplicationBuilder InitializeDbContext(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<CmsUser>>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<CmsRole>>();

            CmsDbInitializer.SeedData(userManager, roleManager).Wait();

            return app;
        }

        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseCorsAngular(this IApplicationBuilder app)
        {
            return app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
        }

        public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Kicks Project Api"); });

            return app;
        }
    }
}