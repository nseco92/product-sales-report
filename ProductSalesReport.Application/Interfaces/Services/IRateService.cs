using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Application.Interfaces.Services
{
    public interface IRateService
    {
        Task<IEnumerable<Rate>> GetAll();
    }
}
