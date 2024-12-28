namespace E_CommerceProject.Core.DTOs.PaymentDTOs
{
    public class CreatePaymentDTO
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
