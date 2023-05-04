namespace ProductSalesReport.Application.Dtos
{
    public class RateDto
        {
        /// <summary>
        /// The currency from which it is converted
        /// </summary>
        /// <example>EUR</example>
        public string From { get; set; }
        /// <summary>
        /// The currency to be converted to
        /// </summary>
        /// <example>UDS</example>
        public string To { get; set; }
        /// <summary>
        /// The rate value
        /// </summary>
        /// <example>1.359</example>
        public decimal Rate { get; set; }

        public RateDto()
        {

        }

        public RateDto(string from, string to, decimal rate)
        {
            From = from;
            To = to;
            Rate = rate;
        }
    }
}
