using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductSalesReport.Application.Controllers;
using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Application.Services;
using Xunit;

namespace ProductSalesReport.Test
{
    public class ProductsControllerTest
    {
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly Mock<IProductTransactionService> _mockProductTransactionService;
        public ProductsControllerTest()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockProductTransactionService = new Mock<IProductTransactionService>();
        }

        [Fact]
        public async Task GetTransactionsCollectionByProduct_ReturnsOkResultWithTransactions()
        {
            // Arrange
            _mockProductTransactionService.Setup(m => m.CreateModel(It.IsAny<string>()))
                .ReturnsAsync(TestRepository.TestGetProductTransactionsDto("T2006"));

            var controller = new ProductsController(_mockProductTransactionService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetCollectionByProduct("T2006");
            var dtoAsObjectResult = Assert.IsType<ObjectResult>(result);
            var dtoResult = Assert.IsType<ProductTransactionsDto>(dtoAsObjectResult.Value);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, dtoAsObjectResult.StatusCode);
            Assert.Equal(2, dtoResult.Transactions.Count());
        }

        [Fact]
        public async Task GetTransactionsCollectionByProduct_ReturnsInternalServerError()
        {
            // Arrange
            _mockProductTransactionService.Setup(m => m.CreateModel(It.IsAny<string>()))
                .Throws(new Exception());

            var controller = new ProductsController(_mockProductTransactionService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetCollectionByProduct("T2006");
            var internalServerErrorCode = Assert.IsType<StatusCodeResult>(result);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorCode.StatusCode);
        }
    }
}
