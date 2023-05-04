using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAll();
        Task<IEnumerable<Transaction>> GetTransactionsByProduct(string productSku);
    }
}
