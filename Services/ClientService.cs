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

    public async Task<Client> AddClient(Client client)
    {
        if (!string.IsNullOrEmpty(client.PESEL))
        {
            var existingClientByPESEL = await _clientRepository.GetByPESELAsync(client.PESEL);
            if (existingClientByPESEL != null)
            {
                throw new ArgumentException("A client with the same PESEL already exists.");
            }
        }

        // Check for existing KRS if it's a company
        if (!string.IsNullOrEmpty(client.KRS))
        {
            var existingClientByKRS = await _clientRepository.GetByKRSAsync(client.KRS);
            if (existingClientByKRS != null)
            {
                throw new ArgumentException("A client with the same KRS already exists.");
            }
        }
        return await _clientRepository.AddAsync(client);
    }

    public async Task<Client> UpdateClient(int id, Client client)
    {
        if (!string.IsNullOrEmpty(client.PESEL))
        {
            var existingClientByPESEL = await _clientRepository.GetByPESELAsync(client.PESEL);
            if (existingClientByPESEL != null)
            {
                throw new ArgumentException("A client with the same PESEL already exists.");
            }
        }

        // Check for existing KRS if it's a company
        if (!string.IsNullOrEmpty(client.KRS))
        {
            var existingClientByKRS = await _clientRepository.GetByKRSAsync(client.KRS);
            if (existingClientByKRS != null)
            {
                throw new ArgumentException("A client with the same KRS already exists.");
            }
        }
        return await _clientRepository.UpdateAsync(id, client);
    }

    public async Task<Client> GetClientById(int id)
    {
        return await _clientRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteClient(int id)
    {
        return await _clientRepository.DeleteAsync(id);
    }
}