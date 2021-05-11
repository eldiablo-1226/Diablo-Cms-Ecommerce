using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiabloCms.Entities.Contracts;
using DiabloCms.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.MsSql
{
    public class CmsDbContext : IdentityDbContext<CmsUser, CmsRole, string>
    {
        public CmsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Fit> Fit { get; set; }
        public DbSet<FitItem> FitIteam { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PhotoUrl> PhotoUrl { get; set; }
        public DbSet<ProductAttribute> ProductAttribute { get; set; }
        public DbSet<ProductProperty> ProductProperty { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Files> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        #region Audit

        public override int SaveChanges()
        {
            ApplyAuditInfoRules();
            ApplyDeletableEntityRules();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfoRules();
            ApplyDeletableEntityRules();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfoRules()
        {
            ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified))
                .ToList()
                .ForEach(entry =>
                {
                    var entity = (IAuditInfo) entry.Entity;

                    if (entry.State == EntityState.Added)
                        entity.CreatedOn = DateTime.UtcNow;
                    else
                        entity.ModifiedOn = DateTime.UtcNow;
                });
        }

        private void ApplyDeletableEntityRules()
        {
            ChangeTracker
                .Entries()
                .Where(e => e.Entity is IDeletableEntity && e.State == EntityState.Deleted)
                .ToList()
                .ForEach(entry =>
                {
                    var entity = (IDeletableEntity) entry.Entity;

                    entity.IsDeleted = true;
                    entity.DeletedOn = DateTime.UtcNow;
                    entry.State = EntityState.Modified;
                });
        }

        #endregion Audit
    }
}