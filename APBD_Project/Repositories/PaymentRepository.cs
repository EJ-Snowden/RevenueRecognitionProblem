using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class PaymentRepository(APBDContext context) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment)
    {
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> UpdateAsync(int id, Payment payment)
    {
        context.Entry(payment).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        return await context.Payments.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await context.Payments.FindAsync(id);
        if (payment == null)
        {
            return false;
        }
        context.Payments.Remove(payment);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await context.Payments
            .Include(p => p.Contract)
            .ThenInclude(c => c.Software)
            .ToListAsync();
    }
}