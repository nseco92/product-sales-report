using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductSalesReport.Test
{
    public class TestRepository
    {
        internal static IEnumerable<Transaction> TestGetTransactionsByProduct(string productId)
        {
            return TestGetAllTransactions().Where(x => x.Sku == productId);
        }

        internal static ProductTransactionsDto TestGetProductTransactionsDto(string productId)
        {
            var model = new ProductTransactionsDto();
            model.Transactions = TestGetTransactionsByProduct(productId)
                                 .Select(t => new TransactionDto()
                                 {
                                     Amount = t.Amount,
                                     Currency = t.Currency,
                                     Sku = t.Sku
                                 })
                                 .ToList();

            model.Total = model.Transactions.Sum(x => x.Amount);
            return model;
        }

        [Fact]
        internal static IEnumerable<Rate> TestGetAllRates()
        {
            //Arrange
            var rates = new List<Rate>
            {
                new Rate("EUR","USD", 1.359m),
                new Rate("CAD","EUR", 0.732m),
                new Rate("USD","EUR", 0.736m),
                new Rate("EUR","CAD", 0.732m),
                new Rate("CAD","EUR", 1.366m)
            };
            return rates;
        }

        [Fact]
        internal static IEnumerable<Transaction> TestGetAllTransactions()
        {
            //Arrange
            var transactions = new List<Transaction>
            {
                new Transaction("T2006", 10.00M, "USD"),
                new Transaction("M2007", 34.57M, "CAD"),
                new Transaction("R2008", 17.95M, "USD"),
                new Transaction("T2006", 7.63M, "EUR"),
                new Transaction("B2009", 21.23M, "USD")
            };
            return transactions;
        }

        [Fact]
        internal static IEnumerable<Rate> TestGetComplexRatesDataSource()
        {
            //Arrange
            var expectedRates = new List<Rate>
            {
                new Rate("USD", "EUR", 2),
                new Rate("EUR", "USD", 4),

                new Rate("USD", "NZ", 3),
                new Rate("NZ", "USD", 4),

                new Rate("USD", "LIBRA", 5),
                new Rate("LIBRA", "USD", 9),

                new Rate("CAD", "EUR", 2),
                new Rate("EUR", "CAD", 4),

                new Rate("LIBRA", "EUR", 6),
                new Rate("EUR", "LIBRA", 2),

                new Rate("LIBRA", "CAD", 7),
                new Rate("CAD", "LIBRA", 8),

                new Rate("NZ", "COL", 2),
                new Rate("COL", "NZ", 5),

                new Rate("NZ", "SOL", 3),
                new Rate("SOL", "NZ", 9),

                new Rate("SOL", "PESOS", 2),
                new Rate("PESOS", "SOL", 3)
            };

            return expectedRates;
        }
    }
}
