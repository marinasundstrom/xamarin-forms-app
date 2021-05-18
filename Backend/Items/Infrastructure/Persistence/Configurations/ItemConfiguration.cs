using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShellApp.Items.Domain.Entities;

namespace ShellApp.Items.Infrastructure.Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(m => m.Id);

            builder.Property(m => m.Text).IsRequired();

            builder.Property(m => m.Description).IsRequired();

            builder.HasQueryFilter(m => m.Deleted == null);

            builder.Ignore(m => m.DomainEvents);
        }
    }
}
