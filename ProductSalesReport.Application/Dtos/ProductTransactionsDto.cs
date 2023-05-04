namespace ProductSalesReport.Application.Dtos
{
    public class ProductTransactionsDto
    {
        /// <summary>
        /// All transactions for the product filtered by sku
        /// </summary>
        /// <example>
        /// { "sku": "R2008", "amount": "17.95", "currency": "USD" }
        /// </example>
        public IList<TransactionDto> Transactions { get; set; }
        /// <summary>
        /// Total sum of the transactions
        /// </summary>
        /// <example>13.21</example>
        public decimal Total { get; set; }
    }
}
