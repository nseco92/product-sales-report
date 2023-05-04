using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.DistributedServices.ExternalServices
{
    public interface ITransactionExternalService
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
    }
}
