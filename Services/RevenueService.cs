using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class RevenueService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IContractRepository _contractRepository;

    public RevenueService(IPaymentRepository paymentRepository, IContractRepository contractRepository)
    {
        _paymentRepository = paymentRepository;
        _contractRepository = contractRepository;
    }

    public async Task<decimal> GetCurrentRevenue()
    {
        var payments = await _paymentRepository.GetAllAsync();
        return payments.Sum(p => p.Amount);
    }

    public async Task<decimal> GetPredictedRevenue()
    {
        var payments = await _paymentRepository.GetAllAsync();
        var predictedRevenue = payments.Sum(p => p.Amount);

        var contracts = await _contractRepository.GetAllPendingContractsAsync();
        predictedRevenue += contracts.Sum(c => c.Price);

        return predictedRevenue;
    }

    public async Task<decimal> GetRevenueInCurrency(string currency)
    {
        var payments = await _paymentRepository.GetAllAsync();
        var currentRevenue = payments.Sum(p => p.Amount);

        var exchangeRate = await GetExchangeRate(currency); // Implement this method to fetch exchange rate from a public service
        var revenueInCurrency = currentRevenue * exchangeRate;

        return revenueInCurrency;
    }

    private async Task<decimal> GetExchangeRate(string currency)
    {
        // Fetch exchange rate from a public service
        // Example: using HttpClient to call an external API
        // For now, return a dummy exchange rate
        return await Task.FromResult(1.0m);
    }
}