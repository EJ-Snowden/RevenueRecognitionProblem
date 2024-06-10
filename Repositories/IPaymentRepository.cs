using APBD_Project.Models;

namespace APBD_Project.Repositories;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
    Task<Payment> UpdateAsync(int id, Payment payment);
    Task<Payment> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Payment>> GetAllAsync();
}