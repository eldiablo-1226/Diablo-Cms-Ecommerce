using DiabloCms.Entities.Models;
using DiabloCms.Shared.ConstContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiabloCms.MsSql.ModelConfigs
{
    using static ModelConstants.Address;

    internal class AddressModelConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.UserId);

            builder.Property(x => x.Country)
                .HasMaxLength(CountryLength)
                .IsRequired();

            builder.Property(x => x.State)
                .HasMaxLength(StateLength)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(CityLength)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(PhoneNumberLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(DescriptionLength)
                .IsRequired();

            builder.Property(x => x.ZipCode)
                .HasMaxLength(PostalCodeLength);
        }
    }
}