using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Items.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItems(this IServiceCollection services, IConfiguration configuration)
        {
            Items.ServiceCollectionExtensions.AddItems(services, configuration);

            return services;
        }
    }
}
