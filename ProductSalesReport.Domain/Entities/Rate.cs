using Newtonsoft.Json;

namespace ProductSalesReport.Domain.Entities
{
    public class Rate : ParentEntity
    {


        [JsonProperty]
        public string From { get; private set; }

        [JsonProperty]
        public string To { get; private set; }

        [JsonProperty("Rate")]
        public decimal RateValue { get; private set; }
        public Rate()
        {

        }

        public Rate(string from, string to, decimal rate)
        {
            From = from;
            To = to;
            RateValue = rate;
        }
    }
}
