using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiabloCms.Shared.ConstContent.ModelConstants.Common;
using static DiabloCms.Shared.ConstContent.ModelConstants.Product;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class ProductAttributeModelConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttributes");

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductAttributes)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasMaxLength(NameLength)
                .IsRequired();

            builder
                .Property(x => x.Color)
                .HasMaxLength(NameLength);

            builder
                .Property(x => x.Size)
                .HasMaxLength(NameLength);

            builder
                .Property(x => x.Photo)
                .HasMaxLength(UrlLength);

            builder
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}