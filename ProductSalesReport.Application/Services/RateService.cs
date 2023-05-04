using ProductSalesReport.Application.Interfaces.Services;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Domain.Interfaces.Repositories;

namespace ProductSalesReport.Application.Services
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;

        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<IEnumerable<Rate>> GetAll()
        {
            return await _rateRepository.GetAll();
        }
    }
}
