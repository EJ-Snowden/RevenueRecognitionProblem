using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class ClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<Client> AddIndividualClientAsync(IndividualClientDto individualClientDto)
    {
        var client = new Client
        {
            FirstName = individualClientDto.FirstName,
            LastName = individualClientDto.LastName,
            Address = individualClientDto.Address,
            Email = individualClientDto.Email,
            PhoneNumber = individualClientDto.PhoneNumber,
            PESEL = individualClientDto.PESEL,
            IsDeleted = false
        };

        return await _clientRepository.AddAsync(client);
    }

    public async Task<Client> AddCompanyClientAsync(CompanyClientDto companyClientDto)
    {
        var client = new Client
        {
            CompanyName = companyClientDto.CompanyName,
            Address = companyClientDto.Address,
            Email = companyClientDto.Email,
            PhoneNumber = companyClientDto.PhoneNumber,
            KRS = companyClientDto.KRS,
            IsDeleted = false
        };

        return await _clientRepository.AddAsync(client);
    }
    
    public async Task<Client> UpdateIndividualClientAsync(int id, IndividualClientUpdateDto updateDto)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("Client not found.");
        }
        
        if (!string.IsNullOrEmpty(client.KRS))
        {
            throw new InvalidOperationException("Cannot update an individual client with company data.");
        }

        client.Address = updateDto.Address;
        client.Email = updateDto.Email;
        client.PhoneNumber = updateDto.PhoneNumber;
        if (!string.IsNullOrEmpty(updateDto.FirstName)) client.FirstName = updateDto.FirstName;
        if (!string.IsNullOrEmpty(updateDto.LastName)) client.LastName = updateDto.LastName;

        return await _clientRepository.UpdateAsync(id, client);
    }

    public async Task<Client> UpdateCompanyClientAsync(int id, CompanyClientUpdateDto updateDto)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("Client not found.");
        }
        if (!string.IsNullOrEmpty(client.PESEL))
        {
            throw new InvalidOperationException("Cannot update a company client with individual data.");
        }
        
        client.Address = updateDto.Address;
        client.Email = updateDto.Email;
        client.PhoneNumber = updateDto.PhoneNumber;
        if (!string.IsNullOrEmpty(updateDto.CompanyName)) client.CompanyName = updateDto.CompanyName;

        return await _clientRepository.UpdateAsync(id, client);
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("Client not found.");
        }

        return client;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("Client not found.");
        }

        client.IsDeleted = true;
        await _clientRepository.UpdateAsync(id, client);

        return true;
    }
}