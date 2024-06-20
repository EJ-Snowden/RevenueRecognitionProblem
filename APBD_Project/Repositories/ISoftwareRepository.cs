using APBD_Project.Models;

namespace APBD_Project.Repositories;

public interface ISoftwareRepository
{
    Task<Software> AddAsync(Software software);
    Task<Software> UpdateAsync(Software software);
    Task<Software> GetByIdAsync(int id);
    Task<IEnumerable<Software>> GetAllAsync();
}