using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class ClientRepository(APBDContext context) : IClientRepository
{
    public async Task<Client> AddAsync(Client client)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateAsync(int id, Client client)
    {
        context.Entry(client).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        return await context.Clients.FindAsync(id);
    }

    public async Task<bool> HasPreviousContractsAsync(int clientId)
    {
        return await context.Contracts.AnyAsync(c => c.ClientId == clientId && c.IsSigned);
    }
}