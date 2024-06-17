using APBD_Project.DTOs;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController(ContractService contractService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Contract>> CreateContract(ContractDto contractDto)
    {
        try
        {
            var contract = await contractService.AddContract(contractDto);
            return contract;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/payments")]
    public async Task<ActionResult> MakePayment(int id, [FromBody]decimal amount)
    {
        try
        {
            await contractService.MakePaymentAsync(id, amount);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contract>> GetContractById(int id)
    {
        var contract = await contractService.GetContractById(id);
        if (contract == null)
        {
            return NotFound("No such contract found");
        }
        return Ok(contract);
    }
}