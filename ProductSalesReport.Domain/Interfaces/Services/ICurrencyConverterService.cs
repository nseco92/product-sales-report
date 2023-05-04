using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.Domain.Interfaces.Services
{
    public interface ICurrencyConverterService
    {
        void ConvertTransactionsToCurrency(string currencyTarget, IEnumerable<Transaction> transactions, IEnumerable<Rate> rates);
    }
}
