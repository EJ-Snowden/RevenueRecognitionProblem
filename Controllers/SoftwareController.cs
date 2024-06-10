using APBD_Project.Data;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> AddSoftware(Software software)
    {
        var createdSoftware = await _softwareService.AddSoftware(software);
        return CreatedAtAction(nameof(GetSoftware), new { id = createdSoftware.SoftwareId }, createdSoftware);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSoftware(int id)
    {
        var software = await _softwareService.GetSoftwareById(id);
        if (software == null) return NotFound();
        return Ok(software);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSoftware(int id, Software updatedSoftware)
    {
        var software = await _softwareService.UpdateSoftware(id, updatedSoftware);
        if (software == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSoftware(int id)
    {
        var result = await _softwareService.DeleteSoftware(id);
        if (!result) return NotFound();
        return NoContent();
    }
}