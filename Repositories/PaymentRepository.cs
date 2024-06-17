using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly APBDContext _context;

    public PaymentRepository(APBDContext context)
    {
        _context = context;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> UpdateAsync(int id, Payment payment)
    {
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return false;
        }
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .Include(p => p.Contract)
            .ThenInclude(c => c.Software)
            .ToListAsync();
    }
}