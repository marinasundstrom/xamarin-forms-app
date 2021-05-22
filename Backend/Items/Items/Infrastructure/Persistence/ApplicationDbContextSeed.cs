using Microsoft.Extensions.DependencyInjection;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Items.Domain.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApp.Items.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //await dbContext.Database.EnsureDeletedAsync();

            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
