using APBD_Project.Models;

namespace APBD_Project.Repositories;

public interface ISoftwareRepository
{
    Task<Software> AddAsync(Software software);
    Task<Software> UpdateAsync(int id, Software software);
    Task<Software> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Software>> GetAllAsync();
}