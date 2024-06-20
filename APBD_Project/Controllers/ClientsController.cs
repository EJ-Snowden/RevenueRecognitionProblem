using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(ClientService clientService) : ControllerBase
{
    [Authorize(Policy = "UserPolicy")]
    [HttpPost("individual")]
    public async Task<ActionResult<Client>> AddIndividualClient(IndividualClientDto individualClientDto)
    {
        try
        {
            var client = await clientService.AddIndividualClientAsync(individualClientDto);
            return client;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPost("company")]
    public async Task<ActionResult<Client>> AddCompanyClient(CompanyClientDto companyClientDto)
    {
        try
        {
            var client = await clientService.AddCompanyClientAsync(companyClientDto);
            return client;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Policy = "UserPolicy")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        try
        {
            var client = await clientService.GetClientByIdAsync(id);
            return Ok(client);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [Authorize(Policy = "UserPolicy")]
    [HttpPut("individual/{id}")]
    public async Task<ActionResult<Client>> UpdateIndividualClient(int id, IndividualClientUpdateDto updateDto)
    {
        try
        {
            var client = await clientService.UpdateIndividualClientAsync(id, updateDto);
            return Ok(client);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPut("company/{id}")]
    public async Task<ActionResult<Client>> UpdateCompanyClient(int id, CompanyClientUpdateDto updateDto)
    {
        try
        {
            var client = await clientService.UpdateCompanyClientAsync(id, updateDto);
            return Ok(client);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await clientService.DeleteClientAsync(id);
        if (!result) return NotFound();
        return Ok("Client was deleted successfully");
    }
}
