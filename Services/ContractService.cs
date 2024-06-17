using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class ContractService(
    IContractRepository contractRepository,
    IClientRepository clientRepository,
    ISoftwareRepository softwareRepository,
    IPaymentRepository paymentRepository)
{
    public async Task<Contract> AddContract(ContractDto contractDto)
    {
        var client = await clientRepository.GetByIdAsync(contractDto.ClientId);
        if (client == null) throw new ArgumentException("No such client was found");

        var software = await softwareRepository.GetByIdAsync(contractDto.SoftwareId);
        if (software == null) throw new ArgumentException("No such software was found");
        
        var activeContract = await contractRepository.GetByClientAndSoftwareAsync(contractDto.ClientId, contractDto.SoftwareId);
        if (activeContract != null) throw new InvalidOperationException("Client already has an active contract for this software");

        
        if ((contractDto.EndDate - contractDto.StartDate).Days < 3 || (contractDto.EndDate - contractDto.StartDate).Days > 30)
        {
            throw new ArgumentException("The contract duration must be between 3 and 30 days");
        }
        
        var price = contractDto.Price;
        var isReturningClient = await clientRepository.HasPreviousContractsAsync(contractDto.ClientId);
        if (isReturningClient)
        {
            price *= 0.95m;
        }
        price += contractDto.SupportYears * 1000m;
        
        var contract = new Contract
        {
            ClientId = contractDto.ClientId,
            Client = await clientRepository.GetByIdAsync(contractDto.ClientId),
            SoftwareId = contractDto.SoftwareId,
            Software = await softwareRepository.GetByIdAsync(contractDto.SoftwareId),
            StartDate = contractDto.StartDate,
            EndDate = contractDto.EndDate,
            Price = price,
            IsSigned = contractDto.IsSigned,
            SupportYears = contractDto.SupportYears
        };

        return await contractRepository.AddAsync(contract);
    }

    public async Task MakePaymentAsync(int contractId, decimal amount)
    {
        var contract = await contractRepository.GetByIdAsync(contractId);
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
            Contract = await contractRepository.GetByIdAsync(contractId),
            Amount = amount,
            PaymentDate = DateTime.UtcNow
        };

        await paymentRepository.AddAsync(payment);

        if (totalPayments + amount == contract.Price)
        {
            contract.IsSigned = true;
            await contractRepository.UpdateAsync(contractId, contract);
        }
    }

    public async Task<Contract> GetContractById(int id)
    {
        return await contractRepository.GetByIdAsync(id);
    }
}