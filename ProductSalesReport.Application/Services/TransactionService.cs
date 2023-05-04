using ProductSalesReport.Application.Interfaces.Services;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Domain.Interfaces.Repositories;

namespace ProductSalesReport.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _transactionRepository.GetAll();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByProduct(string productSku)
        {
            var transactions = await GetAll();
            return transactions.Where(t => t.Sku.ToLower() == productSku.ToLower());
        }
    }
}
