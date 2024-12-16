using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Enums;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> GetPaymentByIntentIdAsync(string paymentIntentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.PaymentIntentId == paymentIntentId);
        }

        public async Task UpdatePaymentStatusAsync(string paymentIntentId, PaymentStatus status)
        {
            var payment = await GetPaymentByIntentIdAsync(paymentIntentId);
            if (payment != null)
            {
                payment.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
