using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Enums;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIntentIdAsync(string paymentIntentId);
        Task UpdatePaymentStatusAsync(string paymentIntentId, PaymentStatus status);
    }
}