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
    public async Task<ActionResult<Contract>> CreateContract(ContractDto contractDto)
    {
        try
        {
            var contract = await _contractService.AddContract(contractDto);
            return CreatedAtAction(nameof(GetContractById), new { id = contract.ContractId }, contract);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/payments")]
    public async Task<ActionResult> MakePayment(int id, [FromBody] decimal amount)
    {
        try
        {
            await _contractService.MakePaymentAsync(id, amount);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contract>> GetContractById(int id)
    {
        var contract = await _contractService.GetContractById(id);
        if (contract == null)
        {
            return NotFound();
        }

        return Ok(contract);
    }
}