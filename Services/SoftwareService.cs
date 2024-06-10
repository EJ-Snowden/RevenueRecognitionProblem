using APBD_Project.Models;
using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class SoftwareService
{
    private readonly ISoftwareRepository _softwareRepository;

    public SoftwareService(ISoftwareRepository softwareRepository)
    {
        _softwareRepository = softwareRepository;
    }

    public async Task<Software> AddSoftware(Software software)
    {
        // Business logic, validation, etc.
        return await _softwareRepository.AddAsync(software);
    }

    public async Task<Software> UpdateSoftware(int id, Software software)
    {
        // Business logic, validation, etc.
        return await _softwareRepository.UpdateAsync(id, software);
    }

    public async Task<Software> GetSoftwareById(int id)
    {
        return await _softwareRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteSoftware(int id)
    {
        return await _softwareRepository.DeleteAsync(id);
    }
}