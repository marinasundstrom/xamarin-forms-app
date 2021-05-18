using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Application.Common.Interfaces;

namespace ShellApp.Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDataContext>(
                (sp, options) => {
                    options.UseSqlServer(configuration.GetConnectionString("mssql"));
#if DEBUG
                    options.EnableSensitiveDataLogging();
#endif
                });

            services.AddScoped<IApplicationDataContext>(sp => sp.GetRequiredService<ApplicationDataContext>());

            return services;
        }
    }
}
