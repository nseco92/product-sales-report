using System.Collections.Generic;
using ProductSalesReport.Application.Constants;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Domain.Services;
using ProductSalesReport.Test;
using Xunit;

namespace ProductSalesReport.Tests
{
    public class CurrencyConverterServiceTests
    {
        [Fact]
        public void ConvertTransactionsCurrency_NormalCurrency()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction("T2006", 10m, "USD"),
                new Transaction("M2010", 120.00m, "CAD")
            };

            var rates = TestData.GetRates();

            var currencyConverterService = new CurrencyConverterService();

            // Act
            currencyConverterService.ConvertTransactionsToCurrency(CurrencyCodes.EUR, transactions, rates);

            // Assert
            Assert.Equal(7.36m, transactions[0].Amount);
            Assert.Equal(87.84m, transactions[1].Amount);
        }

        [Fact]
        public void ConvertTransactionsCurrency_CalculatedCurrency()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction("T2018", 15.45m, "JPY")
            };

            var rates = TestData.GetRates();

            var currencyConverterService = new CurrencyConverterService();

            // Act
            currencyConverterService.ConvertTransactionsToCurrency(CurrencyCodes.EUR, transactions, rates);

            // Assert
            Assert.Equal(0.10m, transactions[0].Amount);
        }
    }
}