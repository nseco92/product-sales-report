using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductSalesReport.DistributedServices.ExternalServices;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Domain.Interfaces.Repositories;

namespace ProductSalesReport.Infrastructure.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly IRateExternalService _externalRateService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RateRepository> _logger;

        public RateRepository(IRateExternalService externalRateService,
            ApplicationDbContext dbContext,
            ILogger<RateRepository> logger)
        {
            _externalRateService = externalRateService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Rate>> GetAll()
        {
            IEnumerable<Rate> rates = new List<Rate>();
            try
            {
                // Obtiene los rates del servicio externo y los guarda en persistencia
                rates = await _externalRateService.GetAllRates();
                await SyncPersistedData(rates);
                return rates;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch rates from external service", ex);
            }

            // Si falla al obtener datos irá a coger los datos a los datos que hay en persistencia
            _logger.LogInformation("Retrieving rates from local storage");
            rates = await GetPersistedRates();
            return rates;
        }

        private async Task<IEnumerable<Rate>> GetPersistedRates()
        {
            return await _dbContext.Rates.ToListAsync();
        }

        private async Task SyncPersistedData(IEnumerable<Rate> ratesFromService)
        {
            await RemoveAllRates();
            await AddRates(ratesFromService);
        }

        private async Task RemoveAllRates()
        {
            var allRates = await GetPersistedRates();
            _dbContext.Rates.RemoveRange(allRates);
            await _dbContext.SaveChangesAsync();
        }

        private async Task AddRates(IEnumerable<Rate> ratesFromService)
        {
            await _dbContext.Rates.AddRangeAsync(ratesFromService);
            await _dbContext.SaveChangesAsync();
        }
    }
}
