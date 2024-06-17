using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController(RevenueService revenueService) : ControllerBase
{
    [HttpGet("current")]
    public async Task<ActionResult<decimal>> GetCurrentRevenue([FromQuery]string currency = "PLN")
    {
        var revenue = await revenueService.CalculateCurrentRevenueAsync(currency);
        return Ok(revenue);
    }

    [HttpGet("predicted")]
    public async Task<ActionResult<decimal>> GetPredictedRevenue([FromQuery]string currency = "PLN")
    {
        var revenue = await revenueService.CalculatePredictedRevenueAsync(currency);
        return Ok(revenue);
    }

    [HttpGet("current/product/{softwareId}")]
    public async Task<ActionResult<decimal>> GetCurrentRevenueForProduct(int softwareId, [FromQuery]string currency = "PLN")
    {
        var revenue = await revenueService.CalculateCurrentRevenueForProductAsync(softwareId, currency);
        return Ok(revenue);
    }

    [HttpGet("predicted/product/{softwareId}")]
    public async Task<ActionResult<decimal>> GetPredictedRevenueForProduct(int softwareId, [FromQuery] string currency = "PLN")
    {
        var revenue = await revenueService.CalculatePredictedRevenueForProductAsync(softwareId, currency);
        return Ok(revenue);
    }
}