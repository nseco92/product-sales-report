using ProductSalesReport.Domain.Entities;
using ProductSalesReport.Domain.Interfaces.Services;
namespace ProductSalesReport.Domain.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private Dictionary<(string, string), decimal> _currencyRateMap;

        public CurrencyConverterService()
        {
            _currencyRateMap = new Dictionary<(string, string), decimal>();
        }

        public void ConvertTransactionsToCurrency(string currencyTarget, IEnumerable<Transaction> transactions, IEnumerable<Rate> rates)
        {
            // Inicializamos el mapa de tipos de moneda y tasas de conversión
            InitializeCurrencyRateMap(rates);

            foreach (var transaction in transactions)
            {
                var currencyFrom = transaction.Currency.ToLower();
                var currencyTo = currencyTarget.ToLower();

                if(currencyFrom == currencyTo)
                {
                    // No se necesita conversión, saltá a siguiente iteración
                    continue; 
                }

                var conversionKey = (currencyFrom, currencyTo);

                if (_currencyRateMap.ContainsKey(conversionKey))
                {
                    // Si ya tenemos la conversión necesaria, hacemos el cálculo con ella
                    var newAmount = transaction.Amount * _currencyRateMap[conversionKey];
                    transaction.SetAmount(RoundToEven(newAmount));
                    transaction.SetCurrency(currencyTarget);
                }
                else
                {
                    // Si no tenemos la conversión en el mapa, tenemos que calcularla
                    var conversionPath = FindConversionPath(currencyFrom, currencyTo);
                    if (conversionPath != null)
                    {
                        var newAmount = transaction.Amount * CalculateConversionRate(conversionPath);
                        transaction.SetAmount(RoundToEven(newAmount));
                        transaction.SetCurrency(currencyTarget);
                    }
                    else
                    {
                        throw new Exception($"Cannot find a conversion rate or path from {transaction.Currency} to {currencyTarget}");
                    }
                }
            }
        }
        private void InitializeCurrencyRateMap(IEnumerable<Rate> rates)
        {
            if (_currencyRateMap.Count > 0)
            {
                return;
            }

            foreach (var rate in rates)
            {
                var key = (rate.From.ToLower(), rate.To.ToLower());
                if (!_currencyRateMap.ContainsKey(key))
                {
                    _currencyRateMap.Add(key, rate.RateValue);
                }
            }
        }

        private decimal CalculateConversionRate(List<(string, string)> conversionPath)
        {
            decimal conversionRate = 1;

            foreach (var conversion in conversionPath)
            {
                conversionRate *= _currencyRateMap[conversion];
            }

            return conversionRate;
        }

        // Método para redondear con Banker's Rounding
        private decimal RoundToEven(decimal newAmount, int decimals = 2)
        {
            return Math.Round(newAmount, decimals, MidpointRounding.ToEven);
        }

        private List<(string, string)>? FindConversionPath(string currencyFrom, string currencyTo, HashSet<string>? visitedCurrencies = null)
        {
            // Se guardan las conversión ya probadas en visitedCurrencies para no repetirlas
            if (visitedCurrencies == null)
            {
                visitedCurrencies = new HashSet<string>();
            }

            visitedCurrencies.Add(currencyFrom);

            // Las conversiones potenciales que se recorrerán en el bucle, se quitan las que están en visitedCurrencies
            var potentialConversions = _currencyRateMap.Keys.Where(k => k.Item1 == currencyFrom && !visitedCurrencies.Contains(k.Item2)).ToList();

            foreach (var conversion in potentialConversions)
            {
                // Ha encontrado la conversión a currencyTo
                if (conversion.Item2 == currencyTo)
                {
                    return new List<(string, string)> { conversion };
                }

                // Llama recursivamente a FindConversionPath hasta que no queden potentialConversions que iterar y no hace nada (valor null) o 
                // hasta que encuentre la conversión a currencyTo, la cual guardará en conversionPath junto con las conversiones recursivas que se han
                // usado hasta llegar a la correcta.
                var conversionPath = FindConversionPath(conversion.Item2, currencyTo, visitedCurrencies);
                if (conversionPath != null)
                {
                    conversionPath.Insert(0, conversion);
                    return conversionPath;
                }
            }

            // Si no hay ninguna coincidencia, devuelve null
            return null;
        }
    }
}
