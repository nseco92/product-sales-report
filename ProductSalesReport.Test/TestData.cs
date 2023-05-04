using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProductSalesReport.Test
{
    public class TestData
    {
        internal static List<Rate> GetRates()
        {
            return new List<Rate>()
            {
                new Rate("USD", "EUR", 0.736m),
                new Rate("EUR", "USD", 1.359m),
                new Rate("JPY", "USD", 0.0091m),
                new Rate("CAD", "EUR", 0.732m),
            };
        }

        internal static List<Transaction> GetTransactions()
        {
            return new List<Transaction>()
            {
                new Transaction("T2006", 10m, "USD"),
                new Transaction("T2006", 7.63m, "USD"),
                new Transaction("M2010", 120.00m, "CAD"),
                new Transaction("T2018", 15.45m, "JPY")
            };
        }

        internal static List<Transaction> GetTransactionsBySku(string sku)
        {
            return GetTransactions().Where(x => x.Sku == sku).ToList();
        }
        internal static ProductTransactionsDto GetProductTransactions(string? sku)
        {
            var transactions = GetTransactionsBySku(sku)
            .Select(t => new TransactionDto
            {
                Sku = t.Sku,
                Amount = t.Amount,
                Currency = t.Currency,
            })
            .ToList();

            var total = transactions.Sum(t => t.Amount);

            return new ProductTransactionsDto
            {
                Transactions = transactions,
                Total = total,
            };
        }

    }
}
