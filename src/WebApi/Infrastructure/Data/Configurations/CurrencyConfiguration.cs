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
        builder.Property(e => e.RateToBase).HasColumnName("RATE_TO_BASE").HasColumnType("NUMBER(18,6)").IsRequired();
    }
}