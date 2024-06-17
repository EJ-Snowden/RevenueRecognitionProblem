using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Services;

public class ContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IPaymentRepository _paymentRepository;

    public ContractService(
        IContractRepository contractRepository,
        IClientRepository clientRepository,
        ISoftwareRepository softwareRepository,
        IPaymentRepository paymentRepository)
    {
        _contractRepository = contractRepository;
        _clientRepository = clientRepository;
        _softwareRepository = softwareRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<Contract> AddContract(ContractDto contractDto)
    {
        var client = await _clientRepository.GetByIdAsync(contractDto.ClientId);
        if (client == null) throw new ArgumentException("Client not found");

        var software = await _softwareRepository.GetByIdAsync(contractDto.SoftwareId);
        if (software == null) throw new ArgumentException("Software not found");
        
        var activeContract = await _contractRepository.GetByClientAndSoftwareAsync(contractDto.ClientId, contractDto.SoftwareId);
        if (activeContract != null) throw new InvalidOperationException("Client already has an active contract for this software");

        
        if ((contractDto.EndDate - contractDto.StartDate).Days < 3 || (contractDto.EndDate - contractDto.StartDate).Days > 30)
        {
            throw new ArgumentException("The contract duration must be between 3 and 30 days.");
        }
        
        decimal price = contractDto.Price;
        bool isReturningClient = await _clientRepository.HasPreviousContractsAsync(contractDto.ClientId);
        if (isReturningClient)
        {
            price *= 0.95m;
        }
        
        price += contractDto.SupportYears * 1000m;
        
        var contract = new Contract
        {
            ClientId = contractDto.ClientId,
            Client = await _clientRepository.GetByIdAsync(contractDto.ClientId),
            SoftwareId = contractDto.SoftwareId,
            Software = await _softwareRepository.GetByIdAsync(contractDto.SoftwareId),
            StartDate = contractDto.StartDate,
            EndDate = contractDto.EndDate,
            Price = price,
            IsSigned = contractDto.IsSigned,
            SupportYears = contractDto.SupportYears
        };

        return await _contractRepository.AddAsync(contract);
    }

    public async Task<bool> MakePaymentAsync(int contractId, decimal amount)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);
        if (contract == null) throw new ArgumentException("Contract not found");

        if (contract.EndDate < DateTime.UtcNow)
        {
            throw new InvalidOperationException("Contract has expired");
        }

        var totalPayments = contract.Payments.Sum(p => p.Amount);
        if (totalPayments + amount > contract.Price)
        {
            throw new InvalidOperationException("Total payment exceeds contract price");
        }

        var payment = new Payment
        {
            ContractId = contractId,
            Contract = await _contractRepository.GetByIdAsync(contractId),
            Amount = amount,
            PaymentDate = DateTime.UtcNow
        };

        await _paymentRepository.AddAsync(payment);

        if (totalPayments + amount == contract.Price)
        {
            contract.IsSigned = true;
            await _contractRepository.UpdateAsync(contractId, contract);
        }

        return true;
    }

    public async Task<Contract> GetContractById(int id)
    {
        return await _contractRepository.GetByIdAsync(id);
    }
}