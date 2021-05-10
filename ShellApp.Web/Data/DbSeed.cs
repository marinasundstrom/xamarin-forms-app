using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Web.Data
{
    public static class DbSeed
    {
        public static async Task SeedAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();

            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
