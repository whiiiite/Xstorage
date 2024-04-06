using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xstorage.Entities.Models;
using Xstorage.Shared;

namespace Xstorage.Configurations
{
    public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.Property(k=>k.Key).HasMaxLength(Consts.MaxApiKeyLength);
            builder.HasIndex(k=>k.Key).IsUnique();
        }
    }
}
