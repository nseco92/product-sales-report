using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Application.Interfaces.Services;

namespace ProductSalesReport.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService _rateService;
        private readonly IMapper _mapper;
        readonly ILogger<RatesController> _logger;

        public RatesController(IRateService rateService,
            IMapper mapper,
            ILogger<RatesController> logger)
        {
            _rateService = rateService;
            _mapper = mapper;
            _logger = logger;

        }

        /// <summary>
        /// Get all Rates
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Rates
        ///
        /// </remarks>
        /// <response code="200">Returns all the Rates</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<RateDto>))]
        public async Task<IActionResult> GetAll()
        {
            // Obtiene los Rates y los devuelve mapeados en Dto
            var ratesEntities = await _rateService.GetAll();
            return Ok(_mapper.Map<IEnumerable<RateDto>>(ratesEntities));
        }
    }
}
