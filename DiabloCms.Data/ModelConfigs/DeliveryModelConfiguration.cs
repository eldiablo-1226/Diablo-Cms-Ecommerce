using DiabloCms.Entities.Models;
using DiabloCms.Shared.ConstContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    using static ModelConstants.Common;
    using static ModelConstants.Product;

    internal class DeliveryModelConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(NameLength).IsRequired();
            builder.Property(x => x.Logo).HasMaxLength(UrlLength).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        }
    }
}