using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class SoftwareRepository : ISoftwareRepository
{
    private readonly APBDContext _context;

    public SoftwareRepository(APBDContext context)
    {
        _context = context;
    }

    public async Task<Software> AddAsync(Software software)
    {
        _context.Software.Add(software);
        await _context.SaveChangesAsync();
        return software;
    }

    public async Task<Software> UpdateAsync(Software software)
    {
        _context.Entry(software).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return software;
    }

    public async Task<Software> GetByIdAsync(int SoftwareId)
    {
        return await _context.Software.Include(s => s.Discounts).FirstOrDefaultAsync(s => s.SoftwareId == SoftwareId) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<Software>> GetAllAsync()
    {
        return await _context.Software.Include(s => s.Discounts).ToListAsync();
    }
}