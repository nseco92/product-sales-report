using Microsoft.EntityFrameworkCore;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Infrastructure.EntityConfigurations;

namespace ProductSalesReport.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RateEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
        }
    }
}
