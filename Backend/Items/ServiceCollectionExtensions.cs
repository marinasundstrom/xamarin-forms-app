using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Items.Application;
using ShellApp.Items.Infrastructure;

namespace ShellApp.Items
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItems(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddInfrastructure(configuration)
                .AddApplication();
        }
    }
}
