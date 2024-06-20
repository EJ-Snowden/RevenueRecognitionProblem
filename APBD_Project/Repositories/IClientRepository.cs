using APBD_Project.Models;

namespace APBD_Project.Repositories;

public interface IClientRepository
{
    Task<Client> AddAsync(Client client);
    Task<Client> UpdateAsync(int id, Client client);
    Task<Client> GetByIdAsync(int id);
    Task<bool> HasPreviousContractsAsync(int clientId);
}