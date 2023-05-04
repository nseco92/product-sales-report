using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Threading.Tasks;
using ProductSalesReport.Test;
using ProductSalesReport.Application.Services;
using ProductSalesReport.Presentation.Controllers;

namespace ProductSalesReport.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly Mock<IProductTransactionService> _mockProductTransactionService;
        private readonly IMapper _mockMapper;

        public ProductsControllerTests()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockProductTransactionService = new Mock<IProductTransactionService>();
            _mockMapper = new Mock<IMapper>().Object;
        }

        [Fact]
        public async Task GetAll_ReturnListOfTransactions()
        {
            // Arrange
            var sku = "T2006";
            _mockProductTransactionService
                .Setup(service => service.GetProductTransaction(sku))
                .ReturnsAsync(TestData.GetProductTransactions("T2006"));

            var controller = new ProductsController(_mockProductTransactionService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetProductTransactions(sku);

            // Assert
            Assert.NotNull(result);
        }
    }
}
