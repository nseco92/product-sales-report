using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Domain.Interfaces.Repositories
{
    public interface IRateRepository
    {
        Task<IEnumerable<Rate>> GetAll();
    }
}
