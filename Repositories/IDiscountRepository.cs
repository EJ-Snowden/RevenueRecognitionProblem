using APBD_Project.Models;

namespace APBD_Project.Repositories;

public interface IDiscountRepository
{
    Task<Discount> AddAsync(Discount discount);
    Task<Discount> UpdateAsync(int id, Discount discount);
    Task<Discount> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}