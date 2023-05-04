namespace ProductSalesReport.Domain.Entities
{
    public class Transaction : ParentEntity
    {
        public string Sku { get; private set; }

        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public Transaction(string sku, decimal amount, string currency)
        {
            Sku = sku;
            Amount = amount;
            Currency = currency;
        }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        public void SetCurrency(string currency)
        {
            Currency = currency;
        }
    }
}