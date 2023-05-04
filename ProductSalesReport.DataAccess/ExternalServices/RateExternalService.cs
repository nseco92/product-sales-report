using Newtonsoft.Json;
using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.DistributedServices.ExternalServices
{
    public class RateExternalService : IRateExternalService
    {
        private readonly HttpClient _httpClient;

        public RateExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Rate>> GetAllRates()
        {
            // Obtiene los rates y los deserializa con una clase de entidad de dominio Rate
            using (var response = await _httpClient.GetAsync("rates.json"))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<Rate>>(responseString);
            }
        }
    }
}
