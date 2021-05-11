using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class CardItemModelConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.ProductAttribute)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.ProductAttributeId);

            builder
                .Property(x => x.Quantity)
                .IsRequired();

            builder.HasIndex(x => x.IsDeleted);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}