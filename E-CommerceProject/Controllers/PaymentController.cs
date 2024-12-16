using E_CommerceProject.Core.DTOs.PaymentDTOs;
using E_CommerceProject.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_CommerceProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO dto)
        {
            var paymentResponse = await _paymentService.CreatePaymentIntent(dto);
            return Ok(paymentResponse);
        }

        [HttpPost("Webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Secretkey"], "your_webhook_secret");
            }
            catch (Exception ex)
            {
                // في حالة كان الحدث غير صالح أو في خطأ في الفحص
                return BadRequest($"Webhook signature verification failed: {ex.Message}");
            }

            // التحقق من النوع بشكل مباشر
            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                {
                    await _paymentService.HandlePaymentSucceeded(paymentIntent.Id);
                }
                else
                {
                    // التعامل مع حالة لو الكائن مش من نوع PaymentIntent
                    return BadRequest("Invalid event object for payment_intent.succeeded");
                }
            }
            else if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                {
                    await _paymentService.HandlePaymentFailed(paymentIntent.Id);
                }
                else
                {
                    // التعامل مع حالة لو الكائن مش من نوع PaymentIntent
                    return BadRequest("Invalid event object for payment_intent.payment_failed");
                }
            }

            return Ok();
        }

    }
}
