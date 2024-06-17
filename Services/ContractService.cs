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

    public ContractService(IContractRepository contractRepository, IClientRepository clientRepository, ISoftwareRepository softwareRepository)
    {
        _contractRepository = contractRepository;
        _clientRepository = clientRepository;
        _softwareRepository = softwareRepository;
    }

    public async Task<Contract> AddContract(ContractDto contractDto)
    {
        var existingClient = await _clientRepository.GetByIdAsync(contractDto.ClientId);
        if (existingClient == null)
        {
            throw new ArgumentException("The specified client does not exist.");
        }
        
        var existingSoftware = await _softwareRepository.GetByIdAsync(contractDto.SoftwareId);
        if (existingSoftware == null)
        {
            throw new ArgumentException("The specified software does not exist.");
        }
        
        var existingContract = await _contractRepository.GetByClientAndSoftwareAsync(contractDto.ClientId, contractDto.SoftwareId);
        if (existingContract != null)
        {
            throw new ArgumentException("A contract for this client and software already exists.");
        }
        
        if ((contractDto.EndDate - contractDto.StartDate).Days < 3 || (contractDto.EndDate - contractDto.StartDate).Days > 30)
        {
            throw new ArgumentException("The contract duration must be between 3 and 30 days.");
        }
        
        var contract = new Contract
        {
            ClientId = contractDto.ClientId,
            SoftwareId = contractDto.SoftwareId,
            StartDate = contractDto.StartDate,
            EndDate = contractDto.EndDate,
            Price = contractDto.Price,
            IsSigned = contractDto.IsSigned,
            SupportYears = contractDto.SupportYears
        };

        return await _contractRepository.AddAsync(contract);
    }

    public async Task<Contract> UpdateContract(int id, ContractDto contractDto)
    {
        var existingContract = await _contractRepository.GetByIdAsync(id);
        if (existingContract == null)
        {
            throw new ArgumentException("The specified contract does not exist.");
        }

        var existingClient = await _clientRepository.GetByIdAsync(contractDto.ClientId);
        if (existingClient == null)
        {
            throw new ArgumentException("The specified client does not exist.");
        }

        var existingSoftware = await _softwareRepository.GetByIdAsync(contractDto.SoftwareId);
        if (existingSoftware == null)
        {
            throw new ArgumentException("The specified software does not exist.");
        }

        existingContract.ClientId = contractDto.ClientId;
        existingContract.SoftwareId = contractDto.SoftwareId;
        existingContract.StartDate = contractDto.StartDate;
        existingContract.EndDate = contractDto.EndDate;
        existingContract.Price = contractDto.Price;
        existingContract.IsSigned = contractDto.IsSigned;
        existingContract.SupportYears = contractDto.SupportYears;

        try
        {
            return await _contractRepository.UpdateAsync(id, existingContract);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ArgumentException("The contract was updated or deleted by another process.");
        }
    }

    public async Task<Contract> GetContractById(int id)
    {
        return await _contractRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteContract(int id)
    {
        return await _contractRepository.DeleteAsync(id);
    }
}