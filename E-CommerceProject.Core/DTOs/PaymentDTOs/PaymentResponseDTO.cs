using E_CommerceProject.Core.Enums;

namespace E_CommerceProject.Core.DTOs.PaymentDTOs
{
    public class PaymentResponseDTO
    {
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
