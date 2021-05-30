using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using ShellApp.Identity.Application.Common.Interfaces;
using ShellApp.Identity.Domain.Entities;
using ShellApp.Identity.Infrastructure.Persistence;

namespace ShellApp.Identity.Infrastructure
{
    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                         .AddEntityFrameworkStores<ApplicationDbContext>()
                         .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential(persistKey: false);

            services.AddLocalApiAuthentication();

            services.AddTransient<IRedirectUriValidator, DemoRedirectValidator>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.TryAddEnumerable(
               ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>,
                   ConfigureJwtBearerOptions>());

            return services;
        }
    }
}
