using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly APBDContext _context;

    public ClientRepository(APBDContext context)
    {
        _context = context;
    }

    public async Task<Client> AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateAsync(int id, Client client)
    {
        _context.Entry(client).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<bool> HasPreviousContractsAsync(int clientId)
    {
        return await _context.Contracts.AnyAsync(c => c.ClientId == clientId && c.IsSigned);
    }
}