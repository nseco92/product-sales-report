using Xunit;
using ProductSalesReport.Domain.Services;
using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Application.Constants;

namespace ProductSalesReport.Test
{
    [Collection("CurrencyConverterService")]
    public class CurrencyConverterTest
    {
        [Fact]
        public void ConvertTransactionsToCurrency_Simple_Test()
        {
            //Arrange
            var exchangeService = new CurrencyConverterService();
            var allTransactions = TestRepository.TestGetTransactionsByProduct("T2006").ToList();

            exchangeService.ConvertTransactionsToCurrency(CurrencyCodes.EUR,
                                                          allTransactions,
                                                          TestRepository.TestGetAllRates());

            //Act
            var total = allTransactions.Sum(x => x.Amount);

            // Assert
            Assert.Equal(14.99M, total);
        }

        [Fact]
        public void ConvertTransactionsToCurrency_RecursiveWay_Test()
        {
            //Arrange

            IEnumerable<Transaction> transactions = new List<Transaction>()
            {
                    new Transaction("T1",10, "USD")
            };

            //USD --> PESOS ==> The routine has to iterate recursively to get the currency exchange
            IEnumerable<Rate> rates = TestRepository.TestGetComplexRatesDataSource();

            var exchangeService = new CurrencyConverterService();


            exchangeService.ConvertTransactionsToCurrency(CurrencyCodes.CAD,
                                                          transactions,
                                                          rates);

            //Act
            var total = transactions.Sum(x => x.Amount);

            // Assert
            Assert.Equal(180M, total);
        }
    }
}