using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShellApp.Items.Domain.Entities;

namespace ShellApp.Items.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Item> Items { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}