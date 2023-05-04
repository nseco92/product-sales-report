using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Infrastructure.EntityConfigurations
{
    public class RateEntityConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder
               .HasKey(u => u.Id);

            builder
               .Property(u => u.From)
               .IsRequired()
               .HasMaxLength(512);

            builder
               .Property(u => u.To)
               .IsRequired()
               .HasMaxLength(512);

            builder
                .Property(u => u.RateValue)
                .IsRequired()
                .HasPrecision(10, 5);
        }
    }
}
