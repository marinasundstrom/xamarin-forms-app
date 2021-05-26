using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Identity.Application.Common.Interfaces;
using ShellApp.Infrastructure;

namespace ShellApp.Identity.Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("mssql", "Identity"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
#if DEBUG
                    options.EnableSensitiveDataLogging();
#endif
                });

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
