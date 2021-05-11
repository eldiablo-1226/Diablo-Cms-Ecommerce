using DiabloCms.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiabloCms.Shared.ConstContent.ModelConstants.Common;

namespace DiabloCms.MsSql.ModelConfigs
{
    internal class CatgoryModelConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder
                .Property(x => x.Name)
                .HasMaxLength(NameLength)
                .IsRequired();
            builder
                .Property(x => x.ParentCategoryName)
                .HasMaxLength(NameLength);

            builder.HasIndex(c => c.IsDeleted);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}