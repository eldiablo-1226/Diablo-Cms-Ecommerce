using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class FitIteamModelConfiguration : IEntityTypeConfiguration<FitItem>
    {
        public void Configure(EntityTypeBuilder<FitItem> builder)
        {
            builder.ToTable("FitItems");

            builder
                .HasOne(x => x.Fit)
                .WithMany(x => x.FitItems)
                .HasForeignKey(x => x.FitId);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.FitIteams)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();
        }
    }
}