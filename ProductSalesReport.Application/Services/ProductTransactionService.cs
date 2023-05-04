using AutoMapper;
using ProductSalesReport.Application.Dtos;
using ProductSalesReport.Domain.Interfaces.Services;
using ProductSalesReport.Application.Constants;
using ProductSalesReport.Application.Interfaces.Services;

namespace ProductSalesReport.Application.Services
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly IRateService _rateService;
        private readonly ITransactionService _transactionService;
        private readonly ICurrencyConverterService _CurrencyConverterService;
        private readonly IMapper _mapper;

        public ProductTransactionService(ITransactionService transactionService,
            IRateService rateService,
            ICurrencyConverterService CurrencyConverterService,
            IMapper mapper)
        {
            _rateService = rateService;
            _transactionService = transactionService;
            _mapper = mapper;
            _CurrencyConverterService = CurrencyConverterService;
        }

        public async Task<ProductTransactionsDto> GetProductTransaction(string productSku)
        {
            var productTransactions = new ProductTransactionsDto();

            // Obtengo los rates y transacciones y conviero estas últimas a la moneda objetivo
            var transactions = await _transactionService.GetTransactionsByProduct(productSku);
            var rates = await _rateService.GetAll();

            _CurrencyConverterService.ConvertTransactionsToCurrency(CurrencyCodes.EUR, transactions, rates);

            // Construyo el objeto productTransactions a raiz de las transacciones previas y calculo el total
            productTransactions.Transactions = _mapper.Map<IList<TransactionDto>>(transactions);
            productTransactions.Total = productTransactions.Transactions.Select(t => t.Amount).Sum();

            return productTransactions;
        }
    }
}
