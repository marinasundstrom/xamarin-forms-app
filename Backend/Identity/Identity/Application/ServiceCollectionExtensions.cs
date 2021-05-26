using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Identity.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}
