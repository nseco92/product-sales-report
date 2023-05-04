using Newtonsoft.Json;
using ProductSalesReport.Domain.Entities;

namespace ProductSalesReport.DistributedServices.ExternalServices
{
    public class TransactionExternalService: ITransactionExternalService
    {
        private readonly HttpClient _httpClient;

        public TransactionExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            // Obtiene las transacciones y los deserializa con una clase de entidad de dominio Transaction
            using (var response = await _httpClient.GetAsync("transactions.json"))
            {
                response.EnsureSuccessStatusCode();
                var responseAsString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<Transaction>>(responseAsString);
            }
        }
    }
}
