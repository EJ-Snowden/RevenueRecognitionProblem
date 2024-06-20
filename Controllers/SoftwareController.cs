using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[Authorize(Policy = "UserPolicy")]
[ApiController]
[Route("api/[controller]")]
public class SoftwareController(SoftwareService softwareService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Software>> AddSoftware(SoftwareDto softwareDto)
    {
        var addedSoftware = await softwareService.AddSoftwareAsync(softwareDto);
        return addedSoftware;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Software>> UpdateSoftware(int id, SoftwareDto softwareDto)
    {
        var updatedSoftware = await softwareService.UpdateSoftwareAsync(id, softwareDto);
        return Ok(updatedSoftware);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSoftwareById(int id)
    {
        try
        {
            var software = await softwareService.GetSoftwareByIdAsync(id);
            return Ok(software);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Software>>> GetAllSoftwares()
    {
        var softwareList = await softwareService.GetAllSoftwareAsync();
        return Ok(softwareList);
    }

    [HttpPost("{softwareId}/discounts")]
    public async Task<ActionResult<Discount>> AddDiscount(int softwareId, DiscountDto discountDto)
    {
        var addedDiscount = await softwareService.AddDiscountAsync(softwareId, discountDto);
        return addedDiscount;
    }

    [HttpGet("{softwareId}/discounts")]
    public async Task<ActionResult<IEnumerable<Discount>>> GetDiscountsForSoftware(int softwareId)
    {
        var discounts = await softwareService.GetDiscountsForSoftwareAsync(softwareId);
        return Ok(discounts);
    }
}