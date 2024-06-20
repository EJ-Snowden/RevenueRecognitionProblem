using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class RevenueService(
    IPaymentRepository paymentRepository,
    IContractRepository contractRepository,
    CurrencyService currencyService)
{
    public async Task<decimal> CalculateCurrentRevenueAsync(string currency = "PLN")
    {
        var payments = await paymentRepository.GetAllAsync();
        var revenue = payments.Sum(p => p.Amount);
        if (currency == "PLN") return revenue;
        var rate = await currencyService.GetExchangeRateAsync(currency);
        revenue *= rate;
        
        return revenue;
    }

    public async Task<decimal> CalculatePredictedRevenueAsync(string currency = "PLN")
    {
        var contracts = await contractRepository.GetAllAsync();
        var totalContractValue = contracts.Sum(c => c.Price);

        var payments = await paymentRepository.GetAllAsync();
        var receivedPayments = payments.Sum(p => p.Amount);

        var predictedRevenue = totalContractValue + receivedPayments;
        if (currency == "PLN") return predictedRevenue;
        var rate = await currencyService.GetExchangeRateAsync(currency);
        predictedRevenue *= rate;
        
        return predictedRevenue;
    }

    public async Task<decimal> CalculateCurrentRevenueForProductAsync(int softwareId, string currency = "PLN")
    {
        var payments = await paymentRepository.GetAllAsync();
        var relevantPayments = payments.Where(p => p.Contract.SoftwareId == softwareId).ToList();

        var revenue = relevantPayments.Sum(p => p.Amount);
        if (currency == "PLN") return revenue;
        var rate = await currencyService.GetExchangeRateAsync(currency);
        revenue *= rate;
        
        return revenue;
    }

    public async Task<decimal> CalculatePredictedRevenueForProductAsync(int softwareId, string currency = "PLN")
    {
        var contracts = await contractRepository.GetAllAsync();
        var relevantContracts = contracts.Where(c => c.SoftwareId == softwareId);
        var totalContractValue = relevantContracts.Sum(c => c.Price);

        var payments = await paymentRepository.GetAllAsync();
        var relevantPayments = payments.Where(p => p.Contract.SoftwareId == softwareId).ToList();

        var receivedPayments = relevantPayments.Sum(p => p.Amount);

        var predictedRevenue = totalContractValue + receivedPayments;
        if (currency == "PLN") return predictedRevenue;
        var rate = await currencyService.GetExchangeRateAsync(currency);
        predictedRevenue *= rate;
        
        return predictedRevenue;
    }
}