using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Application.Interfaces.Services;

namespace ProductSalesReport.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionService transactionService,
            IMapper mapper,
            ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all Transactions
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Transactions
        ///
        /// </remarks>
        /// <response code="200">Returns all the Transactions</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionDto>))]
        public async Task<IActionResult> GetAll()
        {
            // Obtiene las Transactions y los devuelve mapeados en Dto
            var transactionEntities = await _transactionService.GetAll();
            return Ok(_mapper.Map<IEnumerable<TransactionDto>>(transactionEntities));
        }
    }
}
