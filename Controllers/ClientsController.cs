using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpPost("individual")]
    public async Task<ActionResult<Client>> AddIndividualClient(IndividualClientDto individualClientDto)
    {
        try
        {
            var client = await _clientService.AddIndividualClientAsync(individualClientDto);
            return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("company")]
    public async Task<ActionResult<Client>> AddCompanyClient(CompanyClientDto companyClientDto)
    {
        try
        {
            var client = await _clientService.AddCompanyClientAsync(companyClientDto);
            return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null) return NotFound();
        return Ok(client);
    }
    
    [HttpPut("individual/{id}")]
    public async Task<ActionResult<Client>> UpdateIndividualClient(int id, IndividualClientUpdateDto updateDto)
    {
        try
        {
            var client = await _clientService.UpdateIndividualClientAsync(id, updateDto);
            return Ok(client);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("company/{id}")]
    public async Task<ActionResult<Client>> UpdateCompanyClient(int id, CompanyClientUpdateDto updateDto)
    {
        try
        {
            var client = await _clientService.UpdateCompanyClientAsync(id, updateDto);
            return Ok(client);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await _clientService.DeleteClientAsync(id);
        if (!result) return NotFound();
        return Ok("Client was deleted successfully");
    }
}
