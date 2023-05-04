using Microsoft.AspNetCore.Mvc;
using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Application.Services;

namespace ProductSalesReport.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductTransactionService _productTransactionService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductTransactionService productTransactionService,
            ILogger<ProductsController> logger)
        {
            _productTransactionService = productTransactionService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Transactions of the products filtered by the sku provided
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/{productSku}/transactions
        ///
        /// </remarks>
        /// <description>test</description>
        /// <response code="200">Returns all the Transactions of the products filtered by the sku provided</response>
        [HttpGet("{productSku}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductTransactionsDto>))]
        public async Task<IActionResult> GetProductTransactions(string productSku)
        {
            if (string.IsNullOrEmpty(productSku))
            {
                return BadRequest();
            }

            return Ok(await _productTransactionService.GetProductTransaction(productSku));
        }
    }
}
