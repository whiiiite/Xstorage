using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xstorage.Entities.Models;

namespace Xstorage.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.Property(o => o.Id).IsRequired();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.Level).IsRequired();
            builder.Property(o => o.DateStart).IsRequired();
            builder.Property(o => o.DateExpiration).IsRequired();

            builder.HasOne(o => o.User)
                .WithOne(o => o.Subscription)
                .HasForeignKey<Subscription>(o=>o.UserId);
        }
    }
}
