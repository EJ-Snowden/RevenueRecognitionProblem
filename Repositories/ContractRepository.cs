using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class ContractRepository(APBDContext context) : IContractRepository
{
    public async Task<Contract> AddAsync(Contract contract)
    {
        context.Entry(contract.Client).State = EntityState.Unchanged;
        context.Entry(contract.Software).State = EntityState.Unchanged;
        
        context.Contracts.Add(contract);
        await context.SaveChangesAsync();
        return contract;
    }

    public async Task<Contract> UpdateAsync(int id, Contract contract)
    {
        context.Entry(contract).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return contract;
    }

    public async Task<Contract> GetByIdAsync(int id)
    {
        return await context.Contracts.FindAsync(id);
    }
    
    public async Task<IEnumerable<Contract>> GetAllAsync()
    {
        return await context.Contracts.Include(c => c.Client).Include(c => c.Software).ToListAsync();
    }
    public async Task<Contract> GetByClientAndSoftwareAsync(int clientId, int softwareId)
    {
        return await context.Contracts.FirstOrDefaultAsync(c => c.ClientId == clientId && c.SoftwareId == softwareId);
    }
}