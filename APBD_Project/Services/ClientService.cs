using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class ClientService(IClientRepository clientRepository)
{
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
        return await clientRepository.AddAsync(client);
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
        return await clientRepository.AddAsync(client);
    }
    
    public async Task<Client> UpdateIndividualClientAsync(int id, IndividualClientUpdateDto updateDto)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("No such client was found");
        }
        
        if (!string.IsNullOrEmpty(client.KRS))
        {
            throw new InvalidOperationException("Cant update individual client with company\'s data");
        }

        client.Address = updateDto.Address;
        client.Email = updateDto.Email;
        client.PhoneNumber = updateDto.PhoneNumber;
        if (!string.IsNullOrEmpty(updateDto.FirstName)) client.FirstName = updateDto.FirstName;
        if (!string.IsNullOrEmpty(updateDto.LastName)) client.LastName = updateDto.LastName;

        return await clientRepository.UpdateAsync(id, client);
    }

    public async Task<Client> UpdateCompanyClientAsync(int id, CompanyClientUpdateDto updateDto)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("No such client was found");
        }
        if (!string.IsNullOrEmpty(client.PESEL))
        {
            throw new InvalidOperationException("Cant update company client with individual client\'s data");
        }
        
        client.Address = updateDto.Address;
        client.Email = updateDto.Email;
        client.PhoneNumber = updateDto.PhoneNumber;
        if (!string.IsNullOrEmpty(updateDto.CompanyName)) client.CompanyName = updateDto.CompanyName;

        return await clientRepository.UpdateAsync(id, client);
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("No such client was found");
        }
        return client;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client == null || client.IsDeleted)
        {
            throw new KeyNotFoundException("No such client was found");
        }

        client.IsDeleted = true;
        await clientRepository.UpdateAsync(id, client);

        return true;
    }
}