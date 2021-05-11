using System;
using DiabloCms.Entities.Models;
using DiabloCms.MsSql;
using DiabloCms.Server.Infrastructure.Filter;
using DiabloCms.Server.Infrastructure.Service.CurrentUser;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Addresses;
using DiabloCms.UseCases.Contracts.CartItems;
using DiabloCms.UseCases.Contracts.Categories;
using DiabloCms.UseCases.Contracts.Deliveries;
using DiabloCms.UseCases.Contracts.File;
using DiabloCms.UseCases.Contracts.Fit;
using DiabloCms.UseCases.Contracts.Identity;
using DiabloCms.UseCases.Contracts.Orders;
using DiabloCms.UseCases.Contracts.Payments;
using DiabloCms.UseCases.Contracts.Products;
using DiabloCms.UseCases.Contracts.Wishlists;
using DiabloCms.UseCases.Services.Addresses;
using DiabloCms.UseCases.Services.CartItems;
using DiabloCms.UseCases.Services.Categories;
using DiabloCms.UseCases.Services.Deliveries;
using DiabloCms.UseCases.Services.Files;
using DiabloCms.UseCases.Services.Fits;
using DiabloCms.UseCases.Services.Identity;
using DiabloCms.UseCases.Services.Orders;
using DiabloCms.UseCases.Services.Payments;
using DiabloCms.UseCases.Services.Products;
using DiabloCms.UseCases.Services.Wishlists;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sentry;

namespace DiabloCms.Server.Infrastructure.Configuration
{
    using static ModelConstants.Identity;

    public static class ConfigureService
    {
        public static IServiceCollection AddLogTracker(this IServiceCollection services)
        {
            SentrySdk.Init(options =>
            {
                options.Dsn = "https://e99063915daa41709b6ed52f37b6ea44@o542316.ingest.sentry.io/5661873";

                options.TracesSampleRate = 1.0;

                options.TracesSampler = _ => 1.0;
            });

            return services;
        }

        public static IServiceCollection AddController(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers(options => options
                .Filters
                .Add<ModelOrNotFoundActionFilter>());

            return services;
        }

        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(_ => CmsDbContextFactory.CreateCmsDbContext());

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<CmsUser, CmsRole>(options =>
                {
                    options.Password.RequiredLength = MinPasswordLength;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                    //options.SignIn.RequireConfirmedPhoneNumber = true;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<CmsDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var settings = new ApplicationSettingJwt(config.GetValue<string>("JWTToken"));
            services.AddTransient(_ => settings);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;

                    //TODO Enable production
                    bearer.RequireHttpsMetadata = false;

                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = settings.GetSymmetricSecurityKey(),

                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {
            service.AddTransient<IAddressesService, AddressesService>();
            service.AddTransient<ICartItemsService, CartItemsService>();
            service.AddTransient<ICategoriesService, CategoriesService>();
            service.AddTransient<IDeliveriesService, DeliveriesService>();
            service.AddTransient<IIdentityService, IdentityService>();
            service.AddTransient<IJwtGeneratorService, JwtGeneratorService>();
            service.AddTransient<IOrdersService, OrdersService>();
            service.AddTransient<IPaymentsService, PaymentsService>();
            service.AddTransient<IProductsService, ProductsService>();
            service.AddTransient<IWishlistsService, WishlistsService>();
            service.AddTransient<IFilesManagerService, FilesManagerService>();
            service.AddTransient<IFitsService, FitsService>();

            return service;
        }
    }
}