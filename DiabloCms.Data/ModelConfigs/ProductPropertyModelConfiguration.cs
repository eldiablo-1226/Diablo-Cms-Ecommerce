using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiabloCms.Shared.ConstContent.ModelConstants.Common;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class ProductPropertyModelConfiguration : IEntityTypeConfiguration<ProductProperty>
    {
        public void Configure(EntityTypeBuilder<ProductProperty> builder)
        {
            builder.ToTable("ProductProperties");

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductProperties)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.Property)
                .HasMaxLength(NameLength).IsRequired();

            builder.Property(x => x.Value)
                .HasMaxLength(NameLength).IsRequired();
        }
    }
}