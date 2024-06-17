using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class SoftwareService(ISoftwareRepository softwareRepository)
{
    public async Task<Software> AddSoftwareAsync(SoftwareDto softwareDto)
    {
        var software = new Software
        {
            Name = softwareDto.Name,
            Description = softwareDto.Description,
            CurrentVersion = softwareDto.CurrentVersion,
            Category = softwareDto.Category
        };

        return await softwareRepository.AddAsync(software);
    }

    public async Task<Software> UpdateSoftwareAsync(int id, SoftwareDto softwareDto)
    {
        var software = await softwareRepository.GetByIdAsync(id);
        if (software == null) throw new ArgumentException("Software not found");

        software.Name = softwareDto.Name;
        software.Description = softwareDto.Description;
        software.CurrentVersion = softwareDto.CurrentVersion;
        software.Category = softwareDto.Category;

        return await softwareRepository.UpdateAsync(software);
    }

    public async Task<Software> GetSoftwareByIdAsync(int id)
    {
        return await softwareRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Software>> GetAllSoftwareAsync()
    {
        return await softwareRepository.GetAllAsync();
    }

    public async Task<Discount> AddDiscountAsync(int softwareId, DiscountDto discountDto)
    {
        var software = await softwareRepository.GetByIdAsync(softwareId);
        if (software == null)
        {
            throw new ArgumentException("Software not found");
        }

        var discount = new Discount
        {
            Name = discountDto.Name,
            OfferType = discountDto.OfferType,
            Value = discountDto.Value,
            StartDate = discountDto.StartDate,
            EndDate = discountDto.EndDate,
            SoftwareId = softwareId
        };
        software.Discounts.Add(discount);
        await softwareRepository.UpdateAsync(software);

        return discount;
    }

    public async Task<IEnumerable<Discount>> GetDiscountsForSoftwareAsync(int softwareId)
    {
        var software = await softwareRepository.GetByIdAsync(softwareId);
        return software.Discounts;
    }
}