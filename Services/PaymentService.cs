using APBD_Project.Models;
using APBD_Project.Repositories;

namespace APBD_Project.Services;

public class PaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment> AddPayment(Payment payment)
    {
        // Business logic, validation, etc.
        return await _paymentRepository.AddAsync(payment);
    }

    public async Task<Payment> UpdatePayment(int id, Payment payment)
    {
        // Business logic, validation, etc.
        return await _paymentRepository.UpdateAsync(id, payment);
    }

    public async Task<Payment> GetPaymentById(int id)
    {
        return await _paymentRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeletePayment(int id)
    {
        return await _paymentRepository.DeleteAsync(id);
    }
}