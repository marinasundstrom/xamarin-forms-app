using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Infrastructure.Services;

namespace ShellApp.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
