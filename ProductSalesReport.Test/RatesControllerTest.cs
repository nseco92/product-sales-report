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
    public class RatesControllerTests
    {
        private readonly Mock<ILogger<RatesController>> _mockLogger;
        private readonly Mock<IRateService> _mockRatesService;
        private readonly IMapper _mockMapper;

        public RatesControllerTests()
        {
            _mockLogger = new Mock<ILogger<RatesController>>();
            _mockRatesService = new Mock<IRateService>();
            _mockMapper = new Mock<IMapper>().Object;
        }

        [Fact]
        public async Task GetAll_ReturnListOfRates()
        {
            // Arrange
            _mockRatesService
                .Setup(service => service.GetAll())
                .ReturnsAsync(TestData.GetRates());

            var controller = new RatesController(_mockRatesService.Object, _mockMapper, _mockLogger.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.NotNull(result);
        }
    }
}
