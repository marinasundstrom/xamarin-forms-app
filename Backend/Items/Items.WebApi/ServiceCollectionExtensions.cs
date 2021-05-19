using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Items.WebApi.Services;

namespace ShellApp.Items.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItems(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IItemsNotificationClient, ItemsHubClient>();

            Items.ServiceCollectionExtensions.AddItems(services, configuration);

            return services;
        }
    }
}
