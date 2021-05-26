using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShellApp.Identity.Domain.Entities;

namespace ShellApp.Identity.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}