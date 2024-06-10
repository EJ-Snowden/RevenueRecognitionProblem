using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly APBDContext _context;

    public DiscountRepository(APBDContext context)
    {
        _context = context;
    }

    public async Task<Discount> AddAsync(Discount discount)
    {
        _context.Discounts.Add(discount);
        await _context.SaveChangesAsync();
        return discount;
    }

    public async Task<Discount> UpdateAsync(int id, Discount discount)
    {
        _context.Entry(discount).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return discount;
    }

    public async Task<Discount> GetByIdAsync(int id)
    {
        return await _context.Discounts.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var discount = await _context.Discounts.FindAsync(id);
        if (discount == null)
        {
            return false;
        }
        _context.Discounts.Remove(discount);
        await _context.SaveChangesAsync();
        return true;
    }
}