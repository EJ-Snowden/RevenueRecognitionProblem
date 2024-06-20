using Newtonsoft.Json.Linq;

namespace APBD_Project.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string currency)
    {
        var response = await _httpClient.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/PLN");
        var data = JObject.Parse(response);
        if (data["rates"][currency] != null)
        {
            var rate = data["rates"][currency].Value<decimal>();
            return rate;
        }

        return 0;
    }
}