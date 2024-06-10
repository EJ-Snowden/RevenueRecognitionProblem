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

    public async Task<Software> UpdateAsync(int id, Software software)
    {
        _context.Entry(software).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return software;
    }

    public async Task<Software> GetByIdAsync(int id)
    {
        return await _context.Software.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var software = await _context.Software.FindAsync(id);
        if (software == null)
        {
            return false;
        }
        _context.Software.Remove(software);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<IEnumerable<Software>> GetAllAsync()
    {
        return await _context.Software.ToListAsync();
    }
}