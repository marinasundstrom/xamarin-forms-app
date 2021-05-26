using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Identity.Application.Common.Interfaces;

namespace ShellApp.Identity.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            Identity.ServiceCollectionExtensions.AddIdentity(services, configuration);

            return services;
        }
    }
}
