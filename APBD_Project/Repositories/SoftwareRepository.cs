using APBD_Project.Data;
using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Repositories;

public class SoftwareRepository(APBDContext context) : ISoftwareRepository
{
    public async Task<Software> AddAsync(Software software)
    {
        context.Software.Add(software);
        await context.SaveChangesAsync();
        return software;
    }

    public async Task<Software> UpdateAsync(Software software)
    {
        context.Entry(software).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return software;
    }

    public async Task<Software> GetByIdAsync(int SoftwareId)
    {
        return await context.Software.Include(s => s.Discounts).FirstOrDefaultAsync(s => s.SoftwareId == SoftwareId) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<Software>> GetAllAsync()
    {
        return await context.Software.Include(s => s.Discounts).ToListAsync();
    }
}