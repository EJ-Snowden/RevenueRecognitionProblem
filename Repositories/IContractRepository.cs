using APBD_Project.Models;

namespace APBD_Project.Repositories;

public interface IContractRepository
{
    Task<Contract> AddAsync(Contract contract);
    Task<Contract> UpdateAsync(int id, Contract contract);
    Task<Contract> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Contract>> GetAllPendingContractsAsync();
    Task<Contract> GetByClientAndSoftwareAsync(int clientId, int softwareId);
}