using APBD_Project.Data;
using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoftwareController : ControllerBase
{
    private readonly SoftwareService _softwareService;

    public SoftwareController(SoftwareService softwareService)
    {
        _softwareService = softwareService;
    }

    [HttpPost]
    public async Task<ActionResult<Software>> AddSoftware(SoftwareDto softwareDto)
    {
        var addedSoftware = await _softwareService.AddSoftwareAsync(softwareDto);
        return CreatedAtAction(nameof(GetSoftwareById), new { id = addedSoftware.SoftwareId }, addedSoftware);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Software>> UpdateSoftware(int id, SoftwareDto softwareDto)
    {
        var updatedSoftware = await _softwareService.UpdateSoftwareAsync(id, softwareDto);
        return Ok(updatedSoftware);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Software>> GetSoftwareById(int id)
    {
        var software = await _softwareService.GetSoftwareByIdAsync(id);
        if (software == null)
        {
            return NotFound();
        }

        return Ok(software);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Software>>> GetAllSoftware()
    {
        var softwareList = await _softwareService.GetAllSoftwareAsync();
        return Ok(softwareList);
    }

    [HttpPost("{softwareId}/discounts")]
    public async Task<ActionResult<Discount>> AddDiscount(int softwareId, DiscountDto discountDto)
    {
        var addedDiscount = await _softwareService.AddDiscountAsync(softwareId, discountDto);
        return CreatedAtAction(nameof(GetDiscountsForSoftware), new { softwareId = softwareId }, addedDiscount);
    }

    [HttpGet("{softwareId}/discounts")]
    public async Task<ActionResult<IEnumerable<Discount>>> GetDiscountsForSoftware(int softwareId)
    {
        var discounts = await _softwareService.GetDiscountsForSoftwareAsync(softwareId);
        return Ok(discounts);
    }
}