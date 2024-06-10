using APBD_Project.Data;
using APBD_Project.Models;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment(Payment payment)
    {
        var createdPayment = await _paymentService.AddPayment(payment);
        return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.PaymentId }, createdPayment);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment(int id)
    {
        var payment = await _paymentService.GetPaymentById(id);
        if (payment == null) return NotFound();
        return Ok(payment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, Payment updatedPayment)
    {
        var payment = await _paymentService.UpdatePayment(id, updatedPayment);
        if (payment == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var result = await _paymentService.DeletePayment(id);
        if (!result) return NotFound();
        return NoContent();
    }
}