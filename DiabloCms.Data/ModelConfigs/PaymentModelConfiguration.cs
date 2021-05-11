using DiabloCms.Entities.Models;
using DiabloCms.Shared.ConstContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    using static ModelConstants.Common;
    using static ModelConstants.Product;

    internal class PaymentModelConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.Property(x => x.Name).HasMaxLength(NameLength).IsRequired();
            builder.Property(x => x.Logo).HasMaxLength(UrlLength).IsRequired();
            builder.Property(x => x.NormalizeName).HasMaxLength(NameLength).IsRequired();
            builder.Property(x => x.Percentage).HasDefaultValue(0);
        }
    }
}