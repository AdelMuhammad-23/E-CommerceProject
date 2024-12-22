using E_CommerceProject.Core.DTOs.PaymentDTOs;
using E_CommerceProject.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_CommerceProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentsController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePaymentIntent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _paymentService.CreatePaymentIntent(dto, userId);

            return Ok(result);
        }


        [HttpPost("HandlePaymentSucceeded")]
        public async Task<IActionResult> HandlePaymentSucceeded([FromQuery] string paymentIntentId)
        {
            await _paymentService.HandlePaymentSucceeded(paymentIntentId);
            return Ok("Payment succeeded and status updated.");
        }

        [HttpPost("HandlePaymentFailed")]
        public async Task<IActionResult> HandlePaymentFailed([FromQuery] string paymentIntentId)
        {
            await _paymentService.HandlePaymentFailed(paymentIntentId);
            return Ok("Payment failed and status updated.");
        }
    }
}
