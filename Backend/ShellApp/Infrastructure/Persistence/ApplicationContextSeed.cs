using Microsoft.Extensions.DependencyInjection;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Domain.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApp.Infrastructure.Persistence
{
    public static class ApplicationContextSeed
    {
        public static async Task SeedAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            //await dbContext.Database.EnsureDeletedAsync();

            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
