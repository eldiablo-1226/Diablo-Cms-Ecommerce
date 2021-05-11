using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class PhotoUrlModelConfiguration : IEntityTypeConfiguration<PhotoUrl>
    {
        public void Configure(EntityTypeBuilder<PhotoUrl> builder)
        {
            builder.ToTable("PhotoUrls");

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.Photos)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();

            builder
                .HasOne(x => x.Files)
                .WithMany(x => x.PhotoUrl)
                .HasForeignKey(x => x.FilesId)
                .IsRequired();
        }
    }
}