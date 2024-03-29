using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xstorage.Configurations;
using Xstorage.Entities.Models;

namespace Xstorage.Data
{
    public class XstorageDbContext : IdentityDbContext<User>
    {
        public XstorageDbContext (DbContextOptions<XstorageDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new StorageConfiguration());
            builder.ApplyConfiguration(new ApiKeyConfiguration());
            builder.ApplyConfiguration(new SubscriptionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            builder.Entity<Subscription>()
                .HasIndex(x => x.UserId)
                .IsUnique();
        }

        public DbSet<Storage> Storage { get; set; } = default!;
        public DbSet<Subscription> Subscriptions { get; set; } = default!;
    }
}
