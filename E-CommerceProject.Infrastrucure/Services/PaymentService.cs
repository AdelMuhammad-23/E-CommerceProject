using E_CommerceProject.Core.DTOs.PaymentDTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Enums;
using E_CommerceProject.Core.Interfaces;
using Stripe;

namespace E_CommerceProject.Infrastructure.Services
{
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponseDTO> CreatePaymentIntent(CreatePaymentDTO dto)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(dto.Amount * 100), // Stripe works in cents
                Currency = dto.Currency,
                Metadata = new Dictionary<string, string> { { "OrderId", dto.OrderId.ToString() } }
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                Currency = dto.Currency,
                PaymentIntentId = intent.Id,
                PaymentDate = DateTime.UtcNow,
                Status = PaymentStatus.Pending
            };

            await _paymentRepository.AddPaymentAsync(payment);

            return new PaymentResponseDTO
            {
                PaymentIntentId = intent.Id,
                ClientSecret = intent.ClientSecret,
                Status = PaymentStatus.Pending
            };
        }

        public async Task HandlePaymentSucceeded(string paymentIntentId)
        {
            await _paymentRepository.UpdatePaymentStatusAsync(paymentIntentId, PaymentStatus.Completed);
        }

        public async Task HandlePaymentFailed(string paymentIntentId)
        {
            await _paymentRepository.UpdatePaymentStatusAsync(paymentIntentId, PaymentStatus.Failed);
        }
    }
}