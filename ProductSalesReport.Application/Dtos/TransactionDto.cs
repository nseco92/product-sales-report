namespace ProductSalesReport.Application.Dtos
{ 
public class TransactionDto
    {
        /// <summary>
        /// Identifier for the transaction
        /// </summary>
        /// <example>T2006</example>
        public string Sku { get; set; }
        /// <summary>
        /// Price of the transaction
        /// </summary>
        /// <example>7.63</example>
        public decimal Amount { get; set; }
        /// <summary>
        /// Currency of the transaction
        /// </summary>
        /// <example>EUR</example>
        public string Currency { get; set; }
        public TransactionDto()
        {

        }
        public TransactionDto(string sku, decimal amount, string currency)
        {
            Sku = sku;
            Amount = amount;
            Currency = currency;
        }
    }
}
