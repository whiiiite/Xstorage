using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Xstorage.Entities.Models;

namespace Xstorage.Configurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.Host)
                .WithMany(u => u.Storages)
                .HasForeignKey(e => e.HostId)
                .IsRequired();
        }
    }
}
