using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.DistributedServices.ExternalServices
{
    public interface IRateExternalService
    {
        Task<IEnumerable<Rate>> GetAllRates();
    }
}
