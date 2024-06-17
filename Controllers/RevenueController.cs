using APBD_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController : ControllerBase
{
    private readonly RevenueService _revenueService;

    public RevenueController(RevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<decimal>> GetCurrentRevenue([FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.CalculateCurrentRevenueAsync(currency);
        return Ok(revenue);
    }

    [HttpGet("predicted")]
    public async Task<ActionResult<decimal>> GetPredictedRevenue([FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.CalculatePredictedRevenueAsync(currency);
        return Ok(revenue);
    }

    [HttpGet("current/product/{softwareId}")]
    public async Task<ActionResult<decimal>> GetCurrentRevenueForProduct(int softwareId, [FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.CalculateCurrentRevenueForProductAsync(softwareId, currency);
        return Ok(revenue);
    }

    [HttpGet("predicted/product/{softwareId}")]
    public async Task<ActionResult<decimal>> GetPredictedRevenueForProduct(int softwareId, [FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.CalculatePredictedRevenueForProductAsync(softwareId, currency);
        return Ok(revenue);
    }
}