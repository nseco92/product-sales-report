using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Threading.Tasks;
using ProductSalesReport.Test;
using ProductSalesReport.Presentation.Controllers;
using ProductSalesReport.Application.Interfaces.Services;

namespace ProductSalesReport.Tests
{
    public class TransactionControllerTests
    {
        private readonly Mock<ILogger<TransactionsController>> _mockLogger;
        private readonly Mock<ITransactionService> _mockTransactionsService;
        private readonly IMapper _mockMapper;

        public TransactionControllerTests()
        {
            _mockLogger = new Mock<ILogger<TransactionsController>>();
            _mockTransactionsService = new Mock<ITransactionService>();
            _mockMapper = new Mock<IMapper>().Object;
        }

        [Fact]
        public async Task GetAll_ReturnsListOfTransactions()
        {
            // Arrange
            _mockTransactionsService
                .Setup(service => service.GetAll())
                .ReturnsAsync(TestData.GetTransactions());

            var controller = new TransactionsController(_mockTransactionsService.Object, _mockMapper, _mockLogger.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.NotNull(result);
        }
    }
}
