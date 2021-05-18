using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShellApp.Domain.Entities;

namespace ShellApp.Application.Common.Interfaces
{
    public interface IApplicationDataContext
    {
        DbSet<Item> Items { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}