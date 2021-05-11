using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiabloCms.Shared.ConstContent.ModelConstants.Common;
using static DiabloCms.Shared.ConstContent.ModelConstants.Product;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class FitModelConfiguration : IEntityTypeConfiguration<Fit>
    {
        public void Configure(EntityTypeBuilder<Fit> builder)
        {
            builder.ToTable("Fits");

            builder.Property(x => x.Name).HasMaxLength(NameLength).IsRequired();
            builder.Property(x => x.Photo).HasMaxLength(UrlLength).IsRequired();
        }
    }
}