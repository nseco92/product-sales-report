using ProductSalesReport.Application.Dtos;

namespace ProductSalesReport.Application.Services
{
    public interface IProductTransactionService
    {
        Task<ProductTransactionsDto> GetProductTransaction(string productSku);
    }
}
