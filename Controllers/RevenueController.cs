using APBD_Project.Data;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> GetCurrentRevenue()
    {
        var currentRevenue = await _revenueService.GetCurrentRevenue();
        return Ok(currentRevenue);
    }

    [HttpGet("predicted")]
    public async Task<IActionResult> GetPredictedRevenue()
    {
        var predictedRevenue = await _revenueService.GetPredictedRevenue();
        return Ok(predictedRevenue);
    }

    [HttpGet("currency")]
    public async Task<IActionResult> GetRevenueInCurrency(string currency)
    {
        var revenueInCurrency = await _revenueService.GetRevenueInCurrency(currency);
        return Ok(revenueInCurrency);
    }
}