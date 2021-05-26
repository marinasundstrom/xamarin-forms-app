using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShellApp.Identity.Domain.Entities;

namespace ShellApp.Identity.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(m => m.Id);

            /*
            builder.Property(m => m.Text).IsRequired();

            builder.Property(m => m.Description).IsRequired();

            builder.HasQueryFilter(m => m.Deleted == null);

            builder.Ignore(m => m.DomainEvents);
            */
        }
    }
}
