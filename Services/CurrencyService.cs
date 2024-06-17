using Newtonsoft.Json.Linq;

namespace APBD_Project.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CurrencyService> _logger;

    public CurrencyService(HttpClient httpClient, ILogger<CurrencyService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<decimal> GetExchangeRateAsync(string currency)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/PLN");
            _logger.LogInformation($"Fetched exchange rates: {response}");
            var data = JObject.Parse(response);
            if (data["rates"][currency] != null)
            {
                var rate = data["rates"][currency].Value<decimal>();
                _logger.LogInformation($"Exchange rate for {currency}: {rate}");
                return rate;
            }
            else
            {
                _logger.LogError($"Exchange rate for {currency} not found.");
                throw new Exception($"Exchange rate for {currency} not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching exchange rate: {ex.Message}");
            throw;
        }
    }
}