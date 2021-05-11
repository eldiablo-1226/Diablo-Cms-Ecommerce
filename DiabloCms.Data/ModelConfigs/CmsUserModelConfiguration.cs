using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiabloCms.Shared.ConstContent.ModelConstants.Common;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class CmsUserModelConfiguration : IEntityTypeConfiguration<CmsUser>
    {
        public void Configure(EntityTypeBuilder<CmsUser> builder)
        {
            builder.ToTable("Users");

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(NameLength)
                .IsRequired();

            builder
                .Property(x => x.LastName)
                .HasMaxLength(NameLength)
                .IsRequired();

            builder.HasIndex(x => x.IsDeleted);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}