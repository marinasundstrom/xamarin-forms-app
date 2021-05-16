using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Application;
using ShellApp.Application.Items;
using ShellApp.Infrastructure;

namespace ShellApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItems(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddInfrastructure(configuration)
                .AddItems();
        }
    }
}
