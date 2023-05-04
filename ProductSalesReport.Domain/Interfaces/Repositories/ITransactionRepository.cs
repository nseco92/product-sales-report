using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAll();
    }
}
