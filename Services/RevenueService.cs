using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class RevenueService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IContractRepository _contractRepository;
    private readonly CurrencyService _currencyService;

    public RevenueService(IPaymentRepository paymentRepository, IContractRepository contractRepository, CurrencyService currencyService)
    {
        _paymentRepository = paymentRepository;
        _contractRepository = contractRepository;
        _currencyService = currencyService;
    }

    public async Task<decimal> CalculateCurrentRevenueAsync(string currency = "PLN")
    {
        var payments = await _paymentRepository.GetAllAsync();
        var revenue = payments.Sum(p => p.Amount);
        if (currency != "PLN")
        {
            var rate = await _currencyService.GetExchangeRateAsync(currency);
            revenue *= rate;
        }
        return revenue;
    }

    public async Task<decimal> CalculatePredictedRevenueAsync(string currency = "PLN")
    {
        var contracts = await _contractRepository.GetAllAsync();
        var totalContractValue = contracts.Sum(c => c.Price);

        var payments = await _paymentRepository.GetAllAsync();
        var receivedPayments = payments.Sum(p => p.Amount);

        var predictedRevenue = totalContractValue + receivedPayments;
        if (currency != "PLN")
        {
            var rate = await _currencyService.GetExchangeRateAsync(currency);
            predictedRevenue *= rate;
        }
        return predictedRevenue;
    }

    public async Task<decimal> CalculateCurrentRevenueForProductAsync(int softwareId, string currency = "PLN")
    {
        var payments = await _paymentRepository.GetAllAsync();
        var relevantPayments = payments.Where(p =>
        {
            if (p.Contract == null)
            {
                return false;
            }

            if (p.Contract.Software == null)
            {
                return false;
            }

            return p.Contract.SoftwareId == softwareId;
        }).ToList();

        var revenue = relevantPayments.Sum(p => p.Amount);
        if (currency != "PLN")
        {
            var rate = await _currencyService.GetExchangeRateAsync(currency);
            revenue *= rate;
        }
        return revenue;
    }

    public async Task<decimal> CalculatePredictedRevenueForProductAsync(int softwareId, string currency = "PLN")
    {
        var contracts = await _contractRepository.GetAllAsync();
        var relevantContracts = contracts.Where(c => c.SoftwareId == softwareId);
        var totalContractValue = relevantContracts.Sum(c => c.Price);

        var payments = await _paymentRepository.GetAllAsync();
        var relevantPayments = payments.Where(p =>
        {
            if (p.Contract == null)
            {
                return false;
            }

            if (p.Contract.Software == null)
            {
                return false;
            }

            return p.Contract.SoftwareId == softwareId;
        }).ToList();

        var receivedPayments = relevantPayments.Sum(p => p.Amount);

        var predictedRevenue = totalContractValue + receivedPayments;
        if (currency != "PLN")
        {
            var rate = await _currencyService.GetExchangeRateAsync(currency);
            predictedRevenue *= rate;
        }
        return predictedRevenue;
    }
}