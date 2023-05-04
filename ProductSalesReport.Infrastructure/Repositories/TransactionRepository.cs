using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Domain.Interfaces.Repositories;
using ProductSalesReport.DistributedServices.ExternalServices;

namespace ProductSalesReport.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ITransactionExternalService _externalTransactionService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(ITransactionExternalService externalTransactionService,
            ApplicationDbContext dbContext,
            ILogger<TransactionRepository> logger)
        {
            _externalTransactionService = externalTransactionService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            IEnumerable<Transaction> transactions = new List<Transaction>();
            try
            {
                // Obtiene los transactions del servicio externo y los guarda en persistencia
                transactions = await _externalTransactionService.GetAllTransactions();
                await SyncPersistedData(transactions);
                return transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch transactions from external service", ex);
            }

            // Si falla al obtener datos irá a coger los datos a los datos que hay en persistencia
            _logger.LogInformation("Retrieving transactions from local storage");
            transactions = await GetPersistedTransactions();
            return transactions;
        }

        private async Task<IEnumerable<Transaction>> GetPersistedTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        private async Task SyncPersistedData(IEnumerable<Transaction> transactionsFromService)
        {
            await RemoveAllTransactions();
            await AddTransactions(transactionsFromService);
        }

        private async Task RemoveAllTransactions()
        {
            var allTransactions = await GetPersistedTransactions();
            _dbContext.Transactions.RemoveRange(allTransactions);
            await _dbContext.SaveChangesAsync();
        }

        private async Task AddTransactions(IEnumerable<Transaction> transactionsFromService)
        {
            await _dbContext.Transactions.AddRangeAsync(transactionsFromService);
            await _dbContext.SaveChangesAsync();
        }
    }
}
