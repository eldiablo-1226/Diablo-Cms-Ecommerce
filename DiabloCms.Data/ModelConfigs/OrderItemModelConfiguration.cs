using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class OrderItemModelConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            builder
                .HasOne(x => x.ProductAttribute)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.ProductAttributeId);

            builder.HasIndex(x => x.IsDeleted);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}