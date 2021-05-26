using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Infrastructure;
using ShellApp.Items.Application.Common.Interfaces;

namespace ShellApp.Items.Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("mssql", "ShellApp"),
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
