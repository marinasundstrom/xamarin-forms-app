﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Application.Items
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItems(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}