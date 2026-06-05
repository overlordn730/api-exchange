using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("ADDRESSES", "FINANCES");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.UserId).HasColumnName("USER_ID").IsRequired();
        builder.Property(e => e.Street).HasColumnName("STREET").HasMaxLength(200).IsRequired().IsUnicode(false);
        builder.Property(e => e.City).HasColumnName("CITY").HasMaxLength(100).IsRequired().IsUnicode(false);
        builder.Property(e => e.Country).HasColumnName("COUNTRY").HasMaxLength(100).IsRequired().IsUnicode(false);
        builder.Property(e => e.ZipCode).HasColumnName("ZIP_CODE").HasMaxLength(20).IsUnicode(false);
    }
}