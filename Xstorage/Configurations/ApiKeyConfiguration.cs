using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xstorage.Entities.Models;

namespace Xstorage.Configurations
{
    public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.HasOne(k=>k.User)
                .WithOne(u=>u.ApiKey)
                .HasForeignKey<ApiKey>(k=>k.UserId)
                .IsRequired();

            builder.HasIndex(k=>k.Key).IsUnique();
        }
    }
}
