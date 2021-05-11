using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiabloCms.Shared.ConstContent.ModelConstants.Common;
using static DiabloCms.Shared.ConstContent.ModelConstants.Product;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class ProductModelConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder
                .Property(x => x.Name)
                .HasMaxLength(NameLength)
                .IsRequired();

            builder.Property(x => x.Article).HasMaxLength(NameLength);
            builder.Property(x => x.Description).HasMaxLength(DescriptionLength);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.VideoUrl).HasMaxLength(UrlLength).IsRequired();

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasIndex(c => c.IsDeleted);
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}