using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly APBDContext _context;

    public ContractRepository(APBDContext context)
    {
        _context = context;
    }

    public async Task<Contract> AddAsync(Contract contract)
    {
        _context.Entry(contract.Client).State = EntityState.Unchanged;
        _context.Entry(contract.Software).State = EntityState.Unchanged;
        
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();
        return contract;
    }

    public async Task<Contract> UpdateAsync(int id, Contract contract)
    {
        _context.Entry(contract).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return contract;
    }

    public async Task<Contract> GetByIdAsync(int id)
    {
        return await _context.Contracts.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null)
        {
            return false;
        }
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<IEnumerable<Contract>> GetAllPendingContractsAsync()  // Implement this method
    {
        return await _context.Contracts.Where(c => !c.IsSigned).ToListAsync();
    }
    public async Task<Contract> GetByClientAndSoftwareAsync(int clientId, int softwareId) // Implement this method
    {
        return await _context.Contracts.FirstOrDefaultAsync(c => c.ClientId == clientId && c.SoftwareId == softwareId);
    }
}