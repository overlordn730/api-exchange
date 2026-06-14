using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Data.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("CURRENCIES", "FINANCES");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.Code).HasColumnName("CODE").HasMaxLength(10).IsRequired().IsUnicode(false);
        builder.Property(e => e.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired().IsUnicode(false);
        builder.Property(e => e.CountryCode).HasColumnName("COUNTRY_CODE").HasMaxLength(2).IsRequired().IsUnicode(false);
        builder.Property(e => e.BuyRate).HasColumnName("BUY_RATE").HasColumnType("NUMBER(18,6)").IsRequired();
        builder.Property(e => e.SellRate).HasColumnName("SELL_RATE").HasColumnType("NUMBER(18,6)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CREATED_BY").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").IsRequired();
        builder.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
        builder.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
    }
}