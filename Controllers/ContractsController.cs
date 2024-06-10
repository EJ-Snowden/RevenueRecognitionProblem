using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly ContractService _contractService;

    public ContractsController(ContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> AddContract(ContractDto contractDto)
    {
        try
        {
            contractDto.ContractId = 0;
            
            var createdContract = await _contractService.AddContract(contractDto);
            return CreatedAtAction(nameof(GetContract), new { id = createdContract.ContractId }, createdContract);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContract(int id)
    {
        var contract = await _contractService.GetContractById(id);
        if (contract == null) return NotFound();
        return Ok(contract);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContract(int id, ContractDto updatedContract)
    {
        try
        {
            var contract = await _contractService.UpdateContract(id, updatedContract);
            if (contract == null) return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContract(int id)
    {
        var result = await _contractService.DeleteContract(id);
        if (!result) return NotFound();
        return NoContent();
    }
}