using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Infrastructure.EntityConfigurations
{
    class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
               .HasKey(u => u.Id);

            builder
               .Property(u => u.Amount)
               .HasPrecision(10, 5)
               .IsRequired();

            builder
               .Property(u => u.Currency)
               .IsRequired()
               .HasMaxLength(512);

            builder
              .Property(u => u.Sku)
              .IsRequired()
              .HasMaxLength(512);
        }
    }
}
